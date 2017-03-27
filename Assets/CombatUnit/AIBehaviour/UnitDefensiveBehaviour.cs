using System;

public class UnitDefensiveBehaviour : AIBehaviour {

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
