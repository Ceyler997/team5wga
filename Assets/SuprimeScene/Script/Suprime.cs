using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(CombatSystem))]
[RequireComponent(typeof(Level))]
[RequireComponent(typeof(Movement))]
public class Suprime : BaseObject, IFightable {
    #region private fields
    [HeaderAttribute("Suprime Property")]

    private Health healthSystem; //здоровье ВС
    private Energy energySystem; // энергия ВС
    private CombatSystem combatSys; // Система сражения ВС
    private Level level; //Уровень
    private Movement moveSystem; // Передвижение
    private List<Unit> units; //Юниты, прикреплённые к данному ВС
    private Crystal curentCrystall; //текущий кристалл, в радиусе которого находится ВС

    private List<IDeathObserver> deathObservers; //список наблюдателей
    #endregion

    #region getters and setters

    //Вызывается кристаллом, при пересечении ВС радиуса кристалла
    public Crystal CurentCrystall {
        get { return curentCrystall; }
        set { curentCrystall = value; }
    }

    public Health HealthSystem {
        get { return healthSystem; }
        set { healthSystem = value; }
    }

    private Energy EnergySystem {
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
    }

    #endregion

    #region DEBUG

    public GameObject UnitPrefab;

    public void spawnUnit() {
        Unit unit = Instantiate(UnitPrefab, transform.position + Vector3.left * 3, Quaternion.identity).GetComponent<Unit>();
        unit.setupUnit(this);
        unit.Behaviour = new UnitAgressiveBehaviour(unit);
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
        while (DeathObservers.Count != 0) {
            DeathObservers [0].onSubjectDeath(this); // При смерти объекта его подписчики от него отписываются
        }

        CombatSys.Target = null; // Убираем цель, оповещая, что мы больше не атакуем предыдущую цель

        foreach (Unit unit in Units) {
            unit.SubjectDeath();
            Destroy(unit.gameObject);
        }

        Destroy(gameObject);
    }
    #endregion
}
