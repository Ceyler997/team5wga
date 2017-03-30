
abstract public class UnitAIBehaviour {
    private Unit subject; // Управляемый юнит

    public UnitAIBehaviour(Unit subject) { // Конструктор для создания поведения, запрещает конструктор по умолчанию
        if(subject == null) {
            throw new NoSubjectForControlException(); // Поведение падает, если некем управлять
        }

        this.subject = subject;
    } 

    public Unit Subject { // Свойство (property) с геттером для юнита
        get {return subject;}
    }

    abstract public void UpdateState(); // Реализовать и использовать в Update для управления субъектом
}
