using UnityEngine;

public class CombatSystem : MonoBehaviour {

    #region private variables

    float damage;
    float attackSpeed;
    float attackRadius;
    private IFightable target;
    private bool isUnderAttack;
    #endregion

    #region getters and setters

    public IFightable Target {
        get { return target; }

        set { target = value; }
    }

    public bool IsUnderAttack {
        get { return isUnderAttack; }

        set { isUnderAttack = value; }
    }
    #endregion

    #region public methods

    public void attacked(IFightable attacker) {
        IsUnderAttack = true;
        Target = attacker;
    }

    internal bool attack() {
        if(Vector3.Distance(transform.position, Target.getPosition()) < attackRadius) {
            // TODO implement
            return true;
        }
        
        return false;
    }

    internal void notifyAboutTarget(IFightable target) {
        if(Target == null) {
            Target = target;
        }
    }
    #endregion
}

public interface IFightable {
    CombatSystem getCombatSystem();
    Vector3 getPosition();
}
