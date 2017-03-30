using UnityEngine;

public class UnitProtectiveBehaviour : UnitAIBehaviour {

    #region private fields

    private UnitState currentUnitState;
    private int agroRadius; // radius from PROTECT TARGET, where unit will attack the enemy
    private BaseObject protectTarget;
    #endregion

    #region constructors

    public UnitProtectiveBehaviour(Unit subject, BaseObject protectTarget) : base(subject) {
        ProtectTarget = protectTarget;
        CurrentUnitState = UnitState.CALM;
    }
    #endregion

    #region getters and setters

    private UnitState CurrentUnitState {
        get { return currentUnitState; }

        set { currentUnitState = value; }
    }

    public BaseObject ProtectTarget {
        get { return protectTarget; }

        set { protectTarget = value; }
    }
    #endregion

    public override void UpdateState() {
        if (ProtectTarget == null) {
            throw new NoTargetToProtectException();
        }

        CombatSystem cs = Subject.CombatSystem;

        switch (CurrentUnitState) {
            case UnitState.CALM:

                if (cs.IsUnderAttack) {
                    if (cs.attack()) {
                        cs.Target.getCombatSystem().attacked(Subject);
                    }
                    cs.IsUnderAttack = false; // To not go to loop for two defensive units
                    return;
                }

                if (ProtectTarget.RadiusStub.Length != 0) {
                    IFightable closestEnemy = ProtectTarget.getClosestUnitStub();
                    if (Vector3.Distance(ProtectTarget.getPosition(), closestEnemy.getPosition()) < agroRadius) {
                        cs.Target = closestEnemy;
                        CurrentUnitState = UnitState.ALARMED;
                        return;
                    }
                }

                Subject.MovementAgent.follow(ProtectTarget);
                break;

            case UnitState.ALARMED:
                
                if(ProtectTarget.RadiusStub.Length == 0) {
                    CurrentUnitState = UnitState.CALM;
                    return;
                }

                if (!cs.IsUnderAttack) {
                    cs.Target = protectTarget.getClosestUnitStub();
                }

                if (cs.attack()) {
                    cs.Target.getCombatSystem().attacked(Subject);
                } else {
                    Subject.MovementAgent.moveTo(cs.Target.getPosition());
                }

                break;

            default:
                throw new UndefinedDefensiveUnitStateException();
        }
    }
}

public enum UnitState {
    CALM,
    ALARMED
}
