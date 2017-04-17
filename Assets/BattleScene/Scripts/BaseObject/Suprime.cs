using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//systems
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(CombatSystem))]
[RequireComponent(typeof(Level))]
[RequireComponent(typeof(Movement))]
//spells
[RequireComponent(typeof(Teleport))]
[RequireComponent(typeof(CaptureCrystal))]
public class Suprime : BaseObject, IFightable, IDeathObserver, IRadiusObserver {

    #region private fields
    [HeaderAttribute("Suprime Property")]

    private Health healthSystem; //здоровье ВС
    private Energy energySystem; // энергия ВС
    private CombatSystem combatSys; // Система сражения ВС
    private Level level; //Уровень
    private Movement moveSystem; // Передвижение
    private List<Unit> units; //Юниты, прикреплённые к данному ВС
    private Crystal currentCrystal; //текущий кристалл, в радиусе которого находится ВС

    private SuprimeMagic teleport; //Телепорт к ближайшему кристаллу
    private SuprimeMagic captureCrystal; //захват кристалла

    private List<IDeathObserver> deathObservers; //список наблюдателей
    #endregion

    #region getters and setters
    
    public Crystal CurrentCrystal {
        get { return currentCrystal; }
        set { currentCrystal = value; }
    }

    public Health HealthSystem {
        get { return healthSystem; }
        set { healthSystem = value; }
    }

    public Energy EnergySystem {
        get { return energySystem; }
        set { energySystem = value; }
    }

    private Level Level {
        get { return level; }
        set { level = value; }
    }

    public List<Unit> Units {
        get { return units; }
    }

    public CombatSystem CombatSys {
        get { return combatSys; }
        set { combatSys = value; }
    }

    public Movement MoveSystem {
        get { return moveSystem; }
        set { moveSystem = value; }
    }

    Vector3 IFightable.Position {
        get { return transform.position; }
    }

    private List<IDeathObserver> DeathObservers {
        get { return deathObservers; }
        set { deathObservers = value; }
    }
    #endregion

    #region MonoBehaviour methods

    new public void Update() {
        base.Update();
        if (ControllingPlayer == null) {
            throw new SuprimeHaveNoPlayerException();
        }

        if (Input.GetKeyDown("space")) {
            captureCrystal.cast();
            // transform.position = new Vector3(0, 0, 0);
        }
    }
    #endregion

    #region PunBehaviour methods

    public override void OnPhotonInstantiate(PhotonMessageInfo info) {
        Player owner;
        GameManager.Instance.Players.TryGetValue(info.sender, out owner);
        setupSuprime(owner);

        owner.Suprimes.Add(this);
    }
    #endregion

    #region public methods

    public void setupSuprime(Player controller) {
        setupBaseObject(controller,
            GameConf.suprimeReactRadius,
            GameConf.suprimeDetectRadius);

        HealthSystem = GetComponent<Health>();
        HealthSystem.setupSystem(GameConf.suprimeStartHealth,
            GameConf.suprimeMaxHealth,
            GameConf.suprimeBasicRegenSpeed,
            this);

        EnergySystem = GetComponent<Energy>();
        EnergySystem.setupSystem(GameConf.suprimeStartEnergy,
            GameConf.suprimeMaxEnergy);

        CombatSys = GetComponent<CombatSystem>();
        CombatSys.setupSystem(GameConf.suprimeDamage,
            GameConf.suprimeCritDamage,
            0,
            GameConf.suprimeAttackRadius,
            GameConf.suprimeAttackSpeed);

        Level = GetComponent<Level>();
        Level.setupSystem(GameConf.suprimeStartLevel, GameConf.suprimeMaxLevel);

        MoveSystem = GetComponent<Movement>();
        MoveSystem.setupSystem(GameConf.suprimeMoveSpeed);

        units = new List<Unit>();
        DeathObservers = new List<IDeathObserver>();

        //Инициализация магии
        teleport = GetComponent<Teleport>();
        teleport.setup(this, GameConf.TeleportCostEnergy, GameConf.TeleportCastTime);

        captureCrystal = GetComponent<CaptureCrystal>();
        captureCrystal.setup(this, GameConf.CrystalCaptureCostEnergy, GameConf.TeleportCastTime);

    }

    #endregion

    #region DEBUG

    public GameObject UnitPrefab;

    public void spawnUnit() {
        Unit unit = Instantiate(UnitPrefab, transform.position + Vector3.left * 3, Quaternion.identity).GetComponent<Unit>();
        unit.setupUnit(this);
        //unit.Behaviour = new UnitAgressiveBehaviour(unit);
        unit.Attach(this);

        units.Add(unit);
    }
    #endregion

    #region IDeathSubject implementation

    public void Attach(IDeathObserver observer) {
        DeathObservers.Add(observer);
    }

    public void Detach(IDeathObserver observer) {
        DeathObservers.Remove(observer);
    }

    public void SubjectDeath() {
        while (DeathObservers.Count != 0) { // Используется такая конструкция, т.к. список динамически изменяется
            IDeathObserver observer = DeathObservers [0];
            observer.onSubjectDeath(this);
            Detach(observer); // При смерти объекта его подписчики от него отписываются
        }

        CombatSys.Target = null; // Убираем цель, оповещая, что мы больше не атакуем предыдущую цель

        while (Units.Count!=0) {
            Units [0].SubjectDeath(); // При смерти юнит уходит из списка прикреплённых юнитов
        }

        Destroy(gameObject);
    }

    public void onSubjectDeath(IDeathSubject subject) {
        if (subject is Unit) {
            Units.Remove((Unit) subject);
        } else {
            throw new WrongDeathSubsciptionException();
        }
    }
    #endregion

    #region IRadiusObserver implememntation

    public void onObjectEnter(BaseObject enteredObject) {
        //Если кристалл, то ставим как текущий
        if (enteredObject is Crystal) {
            Crystal crystal = (Crystal) enteredObject;
            if (crystal != null) // можно узнать зачем эта проверка?
                CurrentCrystal = crystal;
        }

        if (enteredObject is Suprime) {
            Debug.Log("i saw an Suprime");
        }
    }

    public void onObjectExit(BaseObject enteredObject) {
        //Если кристалл, то убираем из текущего
        if (enteredObject is Crystal) {
            Crystal crystal = (Crystal) enteredObject;
            if (crystal == CurrentCrystal)
                CurrentCrystal = null;
        }

        if (enteredObject is Suprime) {
            Debug.Log("i saw an Suprime");
        }
    }
    #endregion
}
