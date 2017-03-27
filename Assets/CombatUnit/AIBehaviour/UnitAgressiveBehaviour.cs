using System;

public class UnitAgressiveBehaviour : AIBehaviour {

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
