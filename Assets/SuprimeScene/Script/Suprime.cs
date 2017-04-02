using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(Level))]
[RequireComponent(typeof(CombatSystem))]
public class Suprime : BaseObject, IFightable {
    #region private fields
    [HeaderAttribute("Suprime Property")]

    private Crystal curentCrystall; //текущий кристалл, в радиусе которого находится ВС
    private Health healthSystem; //здоровье ВС
    private Energy energy; // энергия ВС
    private CombatSystem combatSystem; // Система сражения ВС
    private Level level; //Уровень
    private List<Unit> units; //Юниты, прикреплённые к данному ВС
    #endregion

    #region getters and setters

    //Вызывается кристаллом, при пересечении ВС радиуса кристалла
    public Crystal CurentCrystall {
        get { return curentCrystall; }

        set { curentCrystall = value; }
    }

    public Health UnitHealthSystem {
        get { return healthSystem; }

        set { healthSystem = value; }
    }

    private Energy Energy {
        get { return energy; }

        set { energy = value; }
    }

    private Level Level {
        get { return level; }

        set { level = value; }
    }

    public List<Unit> Units {
        get { return units; }
    }

    public CombatSystem UnitCombatSystem {
        get { return combatSystem; }

        set { combatSystem = value; }
    }


    Vector3 IFightable.Position {
        get { return transform.position; }
    }

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        UnitHealthSystem = GetComponent<Health>();
        UnitHealthSystem.setupSystem(GameConf.suprimeStartHealth,
            GameConf.suprimeMaxHealth,
            GameConf.suprimeBasicRegenSpeed);

        Energy = GetComponent<Energy>();
        Energy.setupSystem(GameConf.suprimeStartEnergy, 
            GameConf.suprimeMaxEnergy);

        Level = GetComponent<Level>();
        Level.setupSystem(GameConf.suprimeStartLevel, GameConf.suprimeMaxLevel);

        units = new List<Unit>();
    }

    new public void Update() {
        base.Update();
        if(ControllingPlayer == null) {
            throw new SuprimeHaveNoPlayerException();
        }
    }
    #endregion
}
