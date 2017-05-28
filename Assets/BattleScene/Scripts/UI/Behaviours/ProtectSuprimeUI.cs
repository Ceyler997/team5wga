
public class ProtectSuprimeUI : BehaviourUI {
    public override void ApplyBehaviour() {
        if (IsEnable()) {
            Suprime suprime = CharactersController.Instance.SelectedUnit.Subject;
            foreach (Unit unit in suprime.Units) {
                unit.Behaviour = new UnitProtectiveBehaviour(unit, suprime);
            }
        }

        CharactersController.Instance.SelectedUnit.UnitsState = BehaviourStates.SUPRIME_PROTECT;
    }

    protected override bool IsEnable() {
        return CharactersController.Instance.SelectedUnit != null
            && CharactersController.Instance.SelectedUnit.Subject.Units.Count > 0
            && CharactersController.Instance.SelectedUnit.UnitsState != BehaviourStates.SUPRIME_PROTECT;
    }
}
