using System.Collections.Generic;
using UnityEngine;

//systems
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(CombatSystem))]
[RequireComponent(typeof(Level))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(MagicManager))]
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
    private MagicManager magicManager; // Менеджер магии

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

    public Level LevelSystem {
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

    public MagicManager Magic {
        get { return magicManager; }
        set { magicManager = value; }
    }
    #endregion

    #region MonoBehaviour methods

    new public void Update() {
        base.Update();
        if (ControllingPlayer == null) {
            throw new SuprimeHaveNoPlayerException();
        }

        if (Input.GetKeyDown("space")) {
            Magic.CaptureCrystal.TryCast();
            Magic.SmnUnits.TryCast();
        }
    }

    public void OnDestroy() {

        while (DeathObservers.Count != 0) { // Используется такая конструкция, т.к. список динамически изменяется
            IDeathObserver observer = DeathObservers [0];
            observer.OnSubjectDeath(this);
            DeathDetach(observer); // При смерти объекта его подписчики от него отписываются
        }

        CombatSys.Target = null; // Убираем цель, оповещая, что мы больше не атакуем предыдущую цель

        ControllingPlayer.Suprimes.Remove(this);
    }
    #endregion

    #region PunBehaviour methods

    public override void OnPhotonInstantiate(PhotonMessageInfo info) {
        Player owner;
        GameManager.Instance.Players.TryGetValue(info.sender.ID, out owner);
        SetupSuprime(owner);
    }
    #endregion

    #region public methods

    public void SetupSuprime(Player controller) {
        SetupBaseObject(controller,
            GameConf.suprimeReactRadius,
            GameConf.suprimeDetectRadius);
        controller.Suprimes.Add(this);

        HealthSystem = GetComponent<Health>();
        HealthSystem.setupSystem(GameConf.suprimeStartHealth,
            GameConf.suprimeMaxHealth,
            GameConf.suprimeBasicRegenSpeed,
            this);

        EnergySystem = GetComponent<Energy>();
        EnergySystem.setupSystem(GameConf.suprimeStartEnergy,
            GameConf.suprimeMaxEnergy);

        CombatSys = GetComponent<CombatSystem>();
        CombatSys.SetupSystem(this,
            GameConf.suprimeDamage,
            GameConf.suprimeCritDamage,
            0,
            GameConf.suprimeAttackRadius,
            GameConf.suprimeAttackSpeed);

        LevelSystem = GetComponent<Level>();
        LevelSystem.SetupSystem(GameConf.suprimeStartLevel, 
            GameConf.suprimeMaxLevel,
            GameConf.suprimeLevelUpExp);

        MoveSystem = GetComponent<Movement>();
        MoveSystem.SetupSystem(GameConf.suprimeMoveSpeed);

        units = new List<Unit>();
        DeathObservers = new List<IDeathObserver>();

        DetectRadius.RadiusAttach(this);

        //Инициализация магии
        Magic = GetComponent<MagicManager>();
        Magic.Setup(this);
    }

    public void AddUnit(Vector3 position) {
        if (PhotonNetwork.connected) {
            PhotonNetwork.Instantiate("CombatUnitPrefab",
            position,
            Quaternion.identity,
            0,
            new object [] { photonView.viewID });
        } else {
            Unit newUnit = Instantiate(((OfflineGameManager) GameManager.Instance).unitPrefab,
                position,
                Quaternion.identity).GetComponent<Unit>();
            newUnit.SetupUnit(this);
        }
    }

    #endregion

    #region IDeathSubject implementation

    public void DeathAttach(IDeathObserver observer) {
        DeathObservers.Add(observer);
    }

    public void DeathDetach(IDeathObserver observer) {
        DeathObservers.Remove(observer);
    }

    // Если у игрока есть кристаллы, то юниты отправляются защищать ближайший к умершему суприму
    // Если кристаллов нет - юниты умирают
    public void SubjectDeath() {
        if (Units.Count > 0) {
            if (ControllingPlayer.Crystals.Count > 0) {
                List<Crystal>.Enumerator enumerator = ControllingPlayer.Crystals.GetEnumerator();
                enumerator.MoveNext();

                Crystal closestCrystal = enumerator.Current;
                float distanceToClosest = Vector3.Distance(Position, closestCrystal.Position);

                while (enumerator.MoveNext()) {
                    float distanceToCurrent = Vector3.Distance(Position, enumerator.Current.Position);

                    if (distanceToCurrent < distanceToClosest) {
                        closestCrystal = enumerator.Current;
                        distanceToClosest = distanceToCurrent;
                    }
                }

                foreach (Unit unit in Units) {
                    unit.Behaviour = new UnitProtectiveBehaviour(unit, closestCrystal);
                    unit.Master = null;
                }
            } else {
                Debug.LogWarning("Player have no crystals!");
                foreach (Unit unit in Units) {
                    unit.SubjectDeath(); // Список не модифицируется из-за особенностей работы сетевой части
                }
            }
        }

        PhotonNetwork.Destroy(gameObject);
    }

    public void OnSubjectDeath(IDeathSubject subject) {
        if (subject is Unit) {
            Units.Remove((Unit) subject);
        } else {
            throw new WrongDeathSubsciptionException();
        }
    }
    #endregion

    #region IRadiusObserver implememntation

    public void OnObjectEnter(BaseObject enteredObject) {
        //Если кристалл, то ставим как текущий
        if (enteredObject is Crystal) {
            Crystal crystal = (Crystal) enteredObject;
            CurrentCrystal = crystal;
        }
    }

    public void OnObjectExit(BaseObject enteredObject) {
        //Если кристалл, то убираем из текущего
        if (enteredObject is Crystal) {
            Crystal crystal = (Crystal) enteredObject;
            if (crystal == CurrentCrystal)
                CurrentCrystal = null;
        }
    }
    #endregion
}
