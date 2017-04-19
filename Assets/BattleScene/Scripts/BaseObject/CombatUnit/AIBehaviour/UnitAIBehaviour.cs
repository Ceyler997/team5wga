
using UnityEngine;

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

    protected void attack() {
        CombatSystem cs = Subject.CombatSys;
        
        // если получилось атаковать, остановиться и сообщить об атаке, иначе подойти к атакующему
        if (cs.Attack()) {
            Subject.MovementAgent.stop();
            if (cs.Target != null)
                cs.Target.CombatSys.Attacked(Subject);
        } else {
            Subject.MovementAgent.moveTo(cs.Target.Position);
        }
    }

    protected IFightable getClosestEnemyInRadius() {
        Vector3 curCenter = Subject.Position;
        IFightable closestEnemy = null;
        float distToClosestEnemy = 0;

        foreach (BaseObject objectInside in Subject.DetectRadius.ObjectsInside) {
            if (objectInside is IFightable 
                && objectInside.ControllingPlayer != Subject.ControllingPlayer) {
                float distToEnemy = Vector3.Distance(curCenter, objectInside.Position);

                if (distToEnemy < distToClosestEnemy || distToClosestEnemy == 0) {
                    closestEnemy = (IFightable) objectInside;
                    distToClosestEnemy = distToEnemy;
                }
            }
        }

        return closestEnemy;
    }

    virtual public void Start() { } // Используется для настройки поведения перед началом

    abstract public void UpdateState(); // Реализовать и использовать в Update для управления субъектом

    virtual public void End() { } // Используется для настройки поведения после окончания работы
}
