using System;

public class UnitDefensiveBehaviour : AIBehaviour {

    new Unit subject;

    public UnitDefensiveBehaviour(Unit subject) : base(subject) {
        this.subject = subject;
        
    }

    new public Unit Subject {
        get {
            return subject;
        }
    }

    public override void UpdateState() {
        throw new NotImplementedException();
    }
}
