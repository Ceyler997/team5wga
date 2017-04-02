
public class UnitAgressiveBehaviour : UnitAIBehaviour {

    #region constructors

    public UnitAgressiveBehaviour(Unit subject) : base(subject) { }
    #endregion

    #region public methods

    public override void UpdateState() {
        CombatSystem cs = Subject.UnitCombatSystem;
        bool isTargetClosest = cs.IsUnderAttack; // определяем, нужно ли проверять на расстояние до цели

        // если юнит никого не видит, он следует за мастером
        if (cs.Target == null) {
            if (!Subject.DetectRadius.isEnemyInside()) {
                Subject.MovementAgent.follow(Subject.Master);
                return;
            }

            // если нет цели, но юнит кого-то видит, то он берёт ближайшую цель и уведомляет о ней остальных
            cs.Target = Subject.DetectRadius.getClosestUnit();
            notifyUnitsAboutTarget();
            isTargetClosest = true;
        }

        // если юнит кого-то видит и не проверял расстояние - взять ближайшую
        if (!isTargetClosest && Subject.DetectRadius.isEnemyInside()) {
            cs.Target = Subject.DetectRadius.getClosestUnit();
        }

        // если получилось атаковать, остановиться и сообщить об атаке, иначе подойти к цели
        if (cs.attack()) {
            Subject.MovementAgent.stop();
            cs.Target.UnitCombatSystem.attacked(Subject);
        } else {
            Subject.MovementAgent.moveTo(cs.Target.Position);
        }
    }
    #endregion

    #region private methods

    // уведомляет всех юнитов у мастера о цели
    private void notifyUnitsAboutTarget() {
        foreach (Unit unit in Subject.Master.Units) {
            unit.UnitCombatSystem.getTargetNotification(Subject.UnitCombatSystem.Target);
        }
    }
    #endregion
}
