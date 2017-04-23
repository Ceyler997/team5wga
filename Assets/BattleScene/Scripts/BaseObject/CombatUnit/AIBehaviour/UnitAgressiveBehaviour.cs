
public class UnitAgressiveBehaviour : UnitAIBehaviour, IRadiusObserver {
    
    #region constructors

    public UnitAgressiveBehaviour(Unit subject) : base(subject) { }
    #endregion

    #region public methods

    public override void Start() {
        Subject.DetectRadius.Attach(this);
    }

    public override void UpdateState() {
        CombatSystem cs = Subject.CombatSys;
        bool isTargetClosest = cs.IsUnderAttack; // определяем, нужно ли проверять на расстояние до цели

        if (cs.Target == null) {
            IFightable closestEnemy = getClosestEnemyInRadius();
            if (closestEnemy == null) {
                // если юнит никого не видит, он следует за мастером
                Subject.MovementAgent.Follow(Subject.Master);
                return;
            } else {
                // если нет цели, но юнит кого-то видит, то он берёт ближайшую цель и уведомляет о ней остальных
                cs.Target = closestEnemy;
                notifyUnitsAboutTarget();
                isTargetClosest = true;
            }
        }

        // если юнит кого-то видит и не проверял расстояние - взять ближайшую
        if (!isTargetClosest) {
            IFightable closestEnemy = getClosestEnemyInRadius();
            if(closestEnemy != null) {
                cs.Target = closestEnemy;
            }
        }

        Attack();
    }

    public override void End() {
        Subject.DetectRadius.Detach(this);
    }
    #endregion

    #region IRadius observer implementation

    public void OnObjectEnter(BaseObject enteredObject) {
        if (enteredObject.ControllingPlayer != Subject.ControllingPlayer 
            && enteredObject is IFightable) {
            Subject.CombatSys.GetTargetNotification((IFightable) enteredObject);
            notifyUnitsAboutTarget();
        }
    }

    public void OnObjectExit(BaseObject enteredObject) { }
    #endregion

    #region private methods

    // уведомляет всех юнитов у мастера о цели
    private void notifyUnitsAboutTarget() {
        foreach (Unit unit in Subject.Master.Units) {
            unit.CombatSys.GetTargetNotification(Subject.CombatSys.Target);
        }
    }
    #endregion
}
