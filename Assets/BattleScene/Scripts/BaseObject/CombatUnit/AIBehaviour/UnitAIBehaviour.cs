
abstract public class UnitAIBehaviour : IUpdateObserver {
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

    protected void attack() {
        CombatSystem cs = Subject.CombatSys;
        
        // если получилось атаковать, остановиться и сообщить об атаке, иначе подойти к атакующему
        if (cs.attack()) {
            Subject.MovementAgent.stop();
            if (cs.Target != null)
                cs.Target.CombatSys.attacked(Subject);
        } else {
            Subject.MovementAgent.moveTo(cs.Target.Position);
        }
    }

    abstract public void OnUpdate(); // Реализовать и использовать в Update для управления субъектом

    virtual public void OnLateUpdate() { } // Реализовать и использовать в LateUpdate для служебных функций
}
