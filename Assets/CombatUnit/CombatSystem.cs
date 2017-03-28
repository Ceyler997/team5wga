using UnityEngine;

public class CombatSystem : MonoBehaviour {

    #region private variables

    float damage;
    float attackSpeed;
    float attackRadius;
    #endregion

    #region public methods

    public bool attack(IFightable target) {
        throw new System.NotImplementedException();
    }

    public void attacked(IFightable attacker) {
        throw new System.NotImplementedException();
    }
    #endregion
}

public interface IFightable {}