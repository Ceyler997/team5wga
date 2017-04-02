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
    #endregion

    #region MonoBehaviour methods

    new public void Update() {
        base.Update();
        if(ControllingPlayer == null) {
            throw new SuprimeHaveNoPlayerException();
        }
    }
    #endregion

    #region public methods

    public void setupSuprime(Player controller) {
        setupBaseObject(controller,
            GameConf.suprimeAlarmRadius,
            GameConf.suprimeDetectRadius);

        HealthSystem = GetComponent<Health>();
        HealthSystem.setupSystem(GameConf.suprimeStartHealth,
            GameConf.suprimeMaxHealth,
            GameConf.suprimeBasicRegenSpeed);

        EnergySystem = GetComponent<Energy>();
        EnergySystem.setupSystem(GameConf.suprimeStartEnergy,
            GameConf.suprimeMaxEnergy);

        CombatSys = GetComponent<CombatSystem>();
        CombatSys.setupSystem(GameConf.suprimeBasicDmg,
            GameConf.suprimeCritDmg,
            0,
            GameConf.suprimeAttackRadius,
            GameConf.suprimeAttackSpeed);

        Level = GetComponent<Level>();
        Level.setupSystem(GameConf.suprimeStartLevel, GameConf.suprimeMaxLevel);

        MoveSystem = GetComponent<Movement>();
        MoveSystem.setupSystem(GameConf.suprimeMoveSpeed);

        units = new List<Unit>();
    }

    #endregion
}
