using UnityEngine;

abstract public class UnitAIBehaviour {
    private Unit subject;

    public UnitAIBehaviour(Unit subject) {
        Subject = subject;
    }

    protected Unit Subject {
        get {return subject;}

        set {subject = value;}
    }

    abstract public void UpdateState();
}
