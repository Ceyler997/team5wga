using UnityEngine;

public class UnitAgressiveBehaviour : UnitAIBehaviour {

    #region constructors

    public UnitAgressiveBehaviour(Unit subject) : base(subject) { }
    #endregion

    #region public methods

    public override void UpdateState() {
        CombatSystem cs = Subject.getCombatSystem();
        bool isTargetClosest = cs.IsUnderAttack; // for defining, are we get the closest unit in this iteration

        if (cs.Target == null) {
            if(Subject.Inside.Length == 0) {
                Subject.follow(Subject.Master);
                return;
            }

            cs.Target = getClosestUnit();
            notifyAboutTarget();
            isTargetClosest = true;
        }

        if(!isTargetClosest) {
            cs.Target = getClosestUnit();
        }

        if (cs.attack()) {
            Subject.MovementAgent.stop();
            cs.Target.getCombatSystem().attacked(Subject);
        } else {
            Subject.MovementAgent.moveTo(cs.Target.getPosition());
        }
    }
    #endregion

    #region private methods

    private void notifyAboutTarget() {
        foreach (Unit unit in Subject.Master.units) {
            unit.getCombatSystem().notifyAboutTarget(Subject.getCombatSystem().Target);
        }
    }
    #endregion
}
