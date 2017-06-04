
public class ProtectSuprimeUI : BehaviourUI {
    public override void ApplyBehaviour() {
        if (IsEnable()) {
            Suprime suprime = CharactersController.Instance.SelectedUnit.Subject;
            foreach (Unit unit in suprime.Units) {
                unit.Behaviour = new UnitProtectiveBehaviour(unit, suprime);
            }
        }
    }

    protected override bool IsEnable() {
        return CharactersController.Instance.SelectedUnit != null
            && CharactersController.Instance.SelectedUnit.UnitsState != BehaviourStates.NO_UNITS
            && CharactersController.Instance.SelectedUnit.UnitsState != BehaviourStates.SUPRIME_PROTECT;
    }
}
