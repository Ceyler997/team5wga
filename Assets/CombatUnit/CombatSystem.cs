using UnityEngine;

public class CombatSystem : MonoBehaviour {

    #region private variables

    public float unitDamage; // TODO make private
    public float unitCritDamage;

    public float attackRadius; // TODO make private

    public float attackSpeed; // TODO make private
    float nextAttackTime;

    private IFightable target;
    private bool isUnderAttack;
    private BattleMagicColor currentColor;
    float critChance; // from 0.0 to 1.0
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

    public BattleMagicColor CurrentColor {
        get { return currentColor; }

        set { currentColor = value; }
    }

    public float NextAttackTime {
        get { return nextAttackTime; }

        set { nextAttackTime = value; }
    }

    public float AttackSpeed {
        get { return attackSpeed; }

        set { attackSpeed = value; }
    }

    public float CritChance {
        get {return critChance;}

        set {critChance = value;}
    }
    #endregion

    #region MonoBehaviour methods
    private void Start() {
        CurrentColor = BattleMagicColor.NO_COLOR;
        NextAttackTime = Time.time;
    }
    #endregion

    #region public methods

    public void attacked(IFightable attacker) {
        IsUnderAttack = true;
        Target = attacker;
    }

    public bool attack() { // return true if unit CAN attack
        if(Vector3.Distance(transform.position, Target.getPosition()) < attackRadius) {
            if(Time.time >= NextAttackTime) {
                float damageToTarger = unitDamage;
                damageToTarger += getCrit();
                Target.getHealthSystem().getDamage(damageToTarger);
                NextAttackTime = Time.time + AttackSpeed;
            }

            return true;
        }
        
        return false;
    }

    private float getCrit() {
        float curCritChance = CritChance;

        if (CurrentColor == BattleMagicColor.NO_COLOR) {
            return 0;
        }

        if(CurrentColor.CounterMagic == Target.getCombatSystem().CurrentColor) {
            curCritChance -= Target.getCombatSystem().CritChance;
        }

        float dice = Random.Range(0.0f, 1.0f);

        if(dice < curCritChance) {
            return unitCritDamage;
        } else {
            return 0;
        }
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
    Health getHealthSystem();
}
