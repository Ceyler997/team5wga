
abstract class AIBehaviour {
    BaseObject subject;

    public BaseObject Subject {
        get {
            return subject;
        }

        set {
            subject = value;
        }
    }

    abstract public void UpdateState();
}