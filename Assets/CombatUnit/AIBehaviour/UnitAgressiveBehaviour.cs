
public class UnitAgressiveBehaviour : UnitAIBehaviour {

    #region constructors

    public UnitAgressiveBehaviour(Unit subject) : base(subject) { }
    #endregion

    #region public methods

    public override void UpdateState() {
        CombatSystem cs = Subject.CombatSystem;
        bool isTargetClosest = cs.IsUnderAttack; // определяем, нужно ли проверять на расстояние до цели

        // если юнит никого не видит, он следует за мастером
        if (cs.Target == null) {
            if (Subject.RadiusStub.Length == 0) {
                Subject.MovementAgent.follow(Subject.Master);
                return;
            }

            // если нет цели, но юнит кого-то видит, то он берёт ближайшую цель и уведомляет о ней остальных
            cs.Target = Subject.getClosestUnitStub();
            notifyUnitsAboutTarget();
            isTargetClosest = true;
        }

        // если юнит кого-то видит и не проверял расстояние - взять ближайшую
        if (!isTargetClosest && Subject.RadiusStub.Length != 0) {
            cs.Target = Subject.getClosestUnitStub();
        }

        // если получилось атаковать, остановиться и сообщить об атаке, иначе подойти к цели
        if (cs.attack()) {
            Subject.MovementAgent.stop();
            cs.Target.getCombatSystem().attacked(Subject);
        } else {
            Subject.MovementAgent.moveTo(cs.Target.getPosition());
        }
    }
    #endregion

    #region private methods

    // уведомляет всех юнитов у мастера о цели
    private void notifyUnitsAboutTarget() {
        foreach (Unit unit in Subject.Master.units) {
            unit.getCombatSystem().getTargetNotification(Subject.getCombatSystem().Target);
        }
    }
    #endregion
}
