using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour {

    #region private variables

    float damage;
    float attackSpeed;
    float attackRadius;
    BaseObject target;

    public BaseObject Target {
        get {
            return target;
        }

        set {
            target = value;
        }
    }
    #endregion

    #region public methods

    public bool attack() {
        throw new System.NotImplementedException();
    }

    public void attacked() {
        throw new System.NotImplementedException();
    }
    #endregion
}
