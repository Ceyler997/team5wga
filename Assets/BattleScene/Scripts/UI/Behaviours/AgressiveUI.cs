
public class AgressiveUI : BehaviourUI {
    public override void ApplyBehaviour() {
        if (IsEnable()) {
            Suprime suprime = CharactersController.Instance.SelectedUnit.Subject;
            foreach(Unit unit in suprime.Units) {
                unit.Behaviour = new UnitAgressiveBehaviour(unit);
            }
        }
    }

    protected override bool IsEnable() {
        return CharactersController.Instance.SelectedUnit != null
            && CharactersController.Instance.SelectedUnit.UnitsState != BehaviourStates.NO_UNITS
            && CharactersController.Instance.SelectedUnit.UnitsState != BehaviourStates.ATTACK;
    }
}
