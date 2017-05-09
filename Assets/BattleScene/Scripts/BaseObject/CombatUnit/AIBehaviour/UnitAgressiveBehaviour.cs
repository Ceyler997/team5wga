
public class UnitAgressiveBehaviour : UnitAIBehaviour, IRadiusObserver {
    
    #region constructors

    public UnitAgressiveBehaviour(Unit subject) : base(subject) { }
    #endregion

    #region public methods

    public override void Start() {
        Subject.DetectRadius.RadiusAttach(this);
    }

    public override void UpdateState() {
        CombatSystem cs = Subject.CombatSys;
        bool isTargetClosest = cs.IsUnderAttack; // определяем, нужно ли проверять на расстояние до цели

        if (cs.Target == null) {
            IFightable closestEnemy = GetClosestEnemyInRadius();
            if (closestEnemy == null) {
                // если юнит никого не видит, он следует за мастером
                if(Subject.Master == null) {
                    throw new UnitHaveNoMasterException();
                }
                Follow(Subject.Master);
                return;
            } else {
                // если нет цели, но юнит кого-то видит, то он берёт ближайшую цель и уведомляет о ней остальных
                cs.Target = closestEnemy;
                NotifyUnitsAboutTarget();
                isTargetClosest = true;
            }
        }

        // если юнит кого-то видит и не проверял расстояние - взять ближайшую
        if (!isTargetClosest) {
            IFightable closestEnemy = GetClosestEnemyInRadius();
            if(closestEnemy != null) {
                cs.Target = closestEnemy;
            }
        }

        Attack();
    }

    public override void End() {
        Subject.DetectRadius.RadiusDetach(this);
    }
    #endregion

    #region IRadius observer implementation

    public void OnObjectEnter(BaseObject enteredObject) {
        if (enteredObject.ControllingPlayer != Subject.ControllingPlayer 
            && enteredObject is IFightable) {
            Subject.CombatSys.GetTargetNotification((IFightable) enteredObject);
            NotifyUnitsAboutTarget();
        }
    }

    public void OnObjectExit(BaseObject enteredObject) { }
    #endregion

    #region private methods

    // уведомляет всех юнитов у мастера о цели
    private void NotifyUnitsAboutTarget() {
        if(Subject.Master == null) {
            throw new UnitHaveNoMasterException();
        }

        foreach (Unit unit in Subject.Master.Units) {
            unit.CombatSys.GetTargetNotification(Subject.CombatSys.Target);
        }
    }
    #endregion
}
