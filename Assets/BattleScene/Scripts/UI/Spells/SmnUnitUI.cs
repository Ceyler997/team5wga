
public class SmnUnitUI : SpellUI {
    protected override void Update() {
        ControllableUnit unit = CharactersController.Instance.SelectedUnit;

        if (unit != null) {
            Suprime suprime = unit.Subject;
            Spell = suprime.Magic.SmnUnits;
        } else {
            Spell = null;
        }

        base.Update();
    }
}

