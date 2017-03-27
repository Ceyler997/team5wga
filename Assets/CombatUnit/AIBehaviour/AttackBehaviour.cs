using System;

public class AttackBehaviour : AIBehaviour {

    new CombatUnit subject;

    new public CombatUnit Subject {
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
