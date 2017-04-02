using System;
using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour {

    #region private fields

    private SphereCollider radiusCollider;
    private List<BaseObject> enemyList;
    private bool isSettedUp;
    #endregion

    #region getters and setters

    public SphereCollider RadiusCollider {
        get { return radiusCollider; }

        set { radiusCollider = value; }
    }

    public List<BaseObject> EnemyList {
        get { return enemyList; }

        set { enemyList = value; }
    }

    public bool IsSettedUp {
        get { return isSettedUp; }

        set { isSettedUp = value; }
    }
    #endregion

    #region MonoBehaviour methods

    public void Start() {
        EnemyList = new List<BaseObject>();
        RadiusCollider = gameObject.AddComponent<SphereCollider>();
        RadiusCollider.isTrigger = true;
    }

    public void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Enter " + other.name);
    }

    void OnTriggerExit(Collider other) {
        Debug.Log("Exit " + other.name);
    }
    #endregion

    #region public methods

    public void setupSystem(float radiusSize) {
        IsSettedUp = true;
        RadiusCollider.radius = radiusSize;
    }

    internal bool isEnemyInside() {
        throw new NotImplementedException();
    }

    internal IFightable getClosestUnit() {
        throw new NotImplementedException();
    }
    #endregion
}
