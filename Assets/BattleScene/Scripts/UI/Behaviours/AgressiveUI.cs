
public class AgressiveUI : BehaviourUI {
    public override void ApplyBehaviour() {
        if (IsEnable()) {
            Suprime suprime = CharactersController.Instance.SelectedUnit.Subject;
            foreach(Unit unit in suprime.Units) {
                unit.Behaviour = new UnitAgressiveBehaviour(unit);
            }
        }

        CharactersController.Instance.SelectedUnit.UnitsState = BehaviourStates.ATTACK;
    }

    protected override bool IsEnable() {
        return CharactersController.Instance.SelectedUnit != null
            && CharactersController.Instance.SelectedUnit.Subject.Units.Count > 0
            && CharactersController.Instance.SelectedUnit.UnitsState != BehaviourStates.ATTACK;
    }
}
