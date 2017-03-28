using System;

public class UnitAgressiveBehaviour : AIBehaviour {

    #region private fields

    new Unit subject;
    IFightable target;
    bool isUnderAttack = false;
    #endregion

    #region getters and setters

    new public Unit Subject {
        get {
            return subject;
        }
    }

    public IFightable Target {
        get {
            return target;
        }

        set {
            target = value;
        }
    }
    #endregion

    #region constructors

    public UnitAgressiveBehaviour(Unit subject) : base(subject) {
        this.subject = subject;
    }
    #endregion

    public override void UpdateState() {
        if(target == null) {

        }
    }
}
