using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CombatSystem))]
public class Unit : BaseObject, IFightable {

    #region private fields

    private Movement movementAgent;
    private Health healthSystem;
    private CombatSystem combatSystem;
    private UnitAIBehaviour behaviour;
    private Suprime master;
    #endregion

    #region getters and setters

    public Movement MovementAgent {
        get {return movementAgent;}

        set {movementAgent = value;}
    }

    public Health getHealthSystem() {
        return HealthSystem;
    }

    public Health HealthSystem {
        get {return healthSystem;}

        set {healthSystem = value;}
    }

    public CombatSystem getCombatSystem() {
        return CombatSystem;
    }

    public CombatSystem CombatSystem {
        get { return combatSystem; }

        set { combatSystem = value; }
    }

    UnitAIBehaviour Behaviour {
        get {return behaviour;}

        set {behaviour = value;}
    }

    public Suprime Master {
        get {return master;}

        set {master = value;}
    }
    #endregion

    #region MonoBehaviour methods

    void Start () {
        MovementAgent = GetComponent<Movement>();
        HealthSystem = GetComponent<Health>();
        CombatSystem = GetComponent<CombatSystem>();
        // protective behaviour with this unit as behaviour subject and master as protect target
        Behaviour = new UnitProtectiveBehaviour(this, master);
	}

    private void Update() {
        // Проверяем, есть ли мастер у юнита
        if(Master == null) {
            throw new UnitHaveNoMasterException();
        }
        
        Behaviour.UpdateState(); // Получаем команды от ИИ
    }
    #endregion
}
