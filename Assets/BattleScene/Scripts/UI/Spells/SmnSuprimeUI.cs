
public class SmnSuprimeUI : SpellUI {
    protected override void Update() {
        ControllableUnit unit = CharactersController.Instance.SelectedUnit;

        if (unit != null) {
            Suprime suprime = unit.Subject;
            Spell = suprime.Magic.SmnSuprime;
        } else {
            Spell = null;
        }

        base.Update();
    }
}

