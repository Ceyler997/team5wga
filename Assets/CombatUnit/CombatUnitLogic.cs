using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CombatSystem))]
public class CombatUnitLogic : BaseObject {

    #region private fields

    Movement movementAgent;
    Health health;
    CombatSystem combatSystem;
    AIBehaviour behaviour;
    Suprime master;
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
    #endregion

    #region MonoBehaviour methods

    void Start () {
        movementAgent = GetComponent<Movement>();
        health = GetComponent<Health>();
        combatSystem = GetComponent<CombatSystem>();
	}

    private void Update() {
        movementAgent.moveTo(master.transform.position); // TODO TEMP
    }
    #endregion

}
