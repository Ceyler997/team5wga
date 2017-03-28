using System;
using UnityEngine;

public class CombatSystem : MonoBehaviour {

    #region private variables

    float damage;
    float attackSpeed;
    float attackRadius;
    private IFightable target;
    #endregion

    #region getters and setters

    public IFightable Target {
        get { return target; }

        set { target = value; }
    }
    #endregion

    #region public methods

    public void attacked(IFightable attacker) {
        throw new System.NotImplementedException();
    }

    internal bool attack() {
        throw new NotImplementedException();
    }

    internal void notifyAboutTarget(IFightable target) {
        throw new NotImplementedException();
    }
    #endregion
}

public interface IFightable {
    CombatSystem getCombatSystem();
    Vector3 getPosition();
}