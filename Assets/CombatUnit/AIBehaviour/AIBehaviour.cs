abstract public class AIBehaviour {
    protected BaseObject subject;

    public BaseObject Subject {get;set;}

    public AIBehaviour(BaseObject subject) { }

    abstract public void UpdateState();
}
