
using UnityEngine;

abstract public class UnitAIBehaviour {
    #region fields and properties

    private readonly Unit subject; // Управляемый юнит

    public Unit Subject { // Свойство (property) с геттером для юнита
        get {return subject;}
    }
    #endregion

    #region constructor

    public UnitAIBehaviour(Unit subject) { // Конструктор для создания поведения, запрещает конструктор по умолчанию
        if(subject == null) {
            throw new NoSubjectForControlException(); // Поведение падает, если некем управлять
        }

        this.subject = subject;
    }
    #endregion

    #region protected methods

    protected void Attack() {
        CombatSystem cs = Subject.CombatSys;
        
        // если получилось атаковать, остановиться и сообщить об атаке, иначе подойти к атакующему
        if (cs.Attack()) {
            Subject.MovementAgent.Stop();
        } else {
            Subject.MovementAgent.MoveTo(cs.Target.Position);
        }
    }

    protected IFightable GetClosestEnemyInRadius() {
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

    // Следование за целью
    protected void Follow(BaseObject target) { // TODO Can be improved with target movement interpolation
        // Если мы пришли к точке или цель далеко от точки следования, взять новую в окружности радиусом FollowRadius вокруг цели
        if (Subject.MovementAgent.IsFinishedMovement 
            || Vector3.Distance(target.Position, Subject.MovementAgent.Destination) > Subject.FollowDistance) {

            Vector2 shift = Random.insideUnitCircle * Subject.FollowDistance;
            Subject.MovementAgent.MoveTo(target.Position + new Vector3(shift.x, shift.y));
        }
    }
    #endregion

    #region public methods

    virtual public void Start() { } // Используется для настройки поведения перед началом

    abstract public void UpdateState(); // Реализовать и использовать в Update для управления субъектом

    virtual public void End() { } // Используется для настройки поведения после окончания работы
    #endregion
}
