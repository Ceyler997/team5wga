using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CombatSystem))]

public class Unit : BaseObject {

    #region private fields

    Movement movementAgent;
    Health health;
    CombatSystem combatSystem;
    AIBehaviour behaviour;
    Suprime master;
    bool isUnderAttack;
    #endregion

    #region getters and setters

    public Suprime Master {
        get {
            return master;
        }

        set {
            master = value;
        }
    }

    AIBehaviour Behaviour {
        get {
            return behaviour;
        }

        set {
            behaviour = value;
        }
    }

    public bool IsUnderAttack {
        get {
            return isUnderAttack;
        }

        set {
            isUnderAttack = value;
        }
    }
    #endregion

    #region MonoBehaviour methods

    void Start () {
        movementAgent = GetComponent<Movement>();
        health = GetComponent<Health>();
        combatSystem = GetComponent<CombatSystem>();
        behaviour = new ProtectBehaviour();
        behaviour.Subject = this;
	}

    private void Update() {
        behaviour.UpdateState();
    }
    #endregion

}
