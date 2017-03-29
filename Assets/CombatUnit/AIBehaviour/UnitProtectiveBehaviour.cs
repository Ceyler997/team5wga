using System;

public class UnitProtectiveBehaviour : UnitAIBehaviour {

    #region private fields

    private UnitState currentUnitState;
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

        switch (CurrentUnitState) {
            case UnitState.CALM:
                // TODO Implement
                break;
            case UnitState.ALARMED:
                // TODO Implement
                break;
            default:
                throw new UndefinedDefensiveUnitStateException();
        }
    }
}

enum UnitState {
    CALM,
    ALARMED
}
