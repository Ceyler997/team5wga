
public class UnitAgressiveBehaviour : UnitAIBehaviour {

    #region properties
    CombatRadius UnitRadius { get; set; } // для быстрого доступа к радиусу цели защиты
    #endregion

    #region constructors

    public UnitAgressiveBehaviour(Unit subject) : base(subject) {

        if (Subject.DetectRadius is CombatRadius) { // чтобы не заворачивать радиус несколько раз при смене состояния
            UnitRadius = (CombatRadius) Subject.DetectRadius;
        } else {
            UnitRadius = new CombatRadius(Subject.DetectRadius);
            Subject.DetectRadius = UnitRadius;
        }
    }
    #endregion

    #region public methods

    public override void OnUpdate() {
        CombatSystem cs = Subject.CombatSys;
        bool isTargetClosest = cs.IsUnderAttack; // определяем, нужно ли проверять на расстояние до цели

        // если юнит никого не видит, он следует за мастером
        if (cs.Target == null) {
            if (!UnitRadius.isEnemyInside()) {
                Subject.MovementAgent.follow(Subject.Master);
                return;
            }

            // если нет цели, но юнит кого-то видит, то он берёт ближайшую цель и уведомляет о ней остальных
            cs.Target = UnitRadius.getClosestEnemy();
            notifyUnitsAboutTarget();
            isTargetClosest = true;
        }

        // если юнит кого-то видит и не проверял расстояние - взять ближайшую
        if (!isTargetClosest && UnitRadius.isEnemyInside()) {
            cs.Target = UnitRadius.getClosestEnemy();
        }

        attack();
    }

    #endregion

    #region private methods

    // уведомляет всех юнитов у мастера о цели
    private void notifyUnitsAboutTarget() {
        foreach (Unit unit in Subject.Master.Units) {
            unit.CombatSys.getTargetNotification(Subject.CombatSys.Target);
        }
    }
    #endregion
}
