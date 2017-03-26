using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
public class CombatUnitLogic : BaseObject {

    #region private fields

    Movement movementAgent;
    Health health;
    private Suprime master; // TODO TEMP public

    public Suprime Master {
        get {
            return master;
        }

        set {
            master = value;
        }
    }
    #endregion

    #region getters and setters

    public void setMaster(Suprime newMaster) {
        master = newMaster;
    }
    #endregion

    #region MonoBehaviour methods

    void Start () {
        movementAgent = GetComponent<Movement>();
        health = GetComponent<Health>();
	}

    private void Update() {
        movementAgent.moveTo(master.transform.position); // TODO TEMP
    }
    #endregion

}
