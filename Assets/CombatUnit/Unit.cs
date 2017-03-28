using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CombatSystem))]

public class Unit : BaseObject, IFightable {

    #region private fields

    Movement movementAgent;
    Health health;
    CombatSystem combatSystem;
    UnitAIBehaviour behaviour;
    public Suprime master; // TODO make private
    public BaseObject [] inside; // should be redone with radius object in BaseObject
    #endregion

    #region getters and setters

    public Movement MovementAgent {
        get {return movementAgent;}

        set {movementAgent = value;}
    }

    public Health Health {
        get {return health;}

        set {health = value;}
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
        Health = GetComponent<Health>();
        CombatSystem = GetComponent<CombatSystem>();
        Behaviour = new UnitAgressiveBehaviour(this); // TODO make defensive in the end
	}

    private void Update() {
        Behaviour.UpdateState();
    }
    #endregion

    internal void follow(Suprime master) {
        throw new NotImplementedException();
    }

}
