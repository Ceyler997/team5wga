using System;

public class GroupProtectBehaviour : AIBehaviour {

    new Unit subject;

    new public Unit Subject {
        get {
            return subject;
        }

        set {
            subject = value;
        }
    }

    public override void UpdateState() {
        throw new NotImplementedException();
    }
}
