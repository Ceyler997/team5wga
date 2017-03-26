using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
public class CombatUnitLogic : MonoBehaviour {

    #region Private fields

    Movement movementAgent;
    Health health;
    public Suprime master; // TODO TEMP public
    #endregion

    #region Getters and setters

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
