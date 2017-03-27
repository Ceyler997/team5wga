using System;

public class GroupAttackBehaviour : AIBehaviour {

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
