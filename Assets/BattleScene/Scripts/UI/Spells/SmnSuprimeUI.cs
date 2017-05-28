
public class SmnSuprimeUI : SpellUI {
    public Crystal crystal;

    protected override void Update() {
        ControllableUnit unit = CharactersController.Instance.SelectedUnit;
        Spell = null;

        if (unit != null) {
            Suprime suprime = unit.Subject;
            if (suprime.CurrentCrystal == crystal) {
                Spell = suprime.Magic.SmnSuprime;
            }
        }

        base.Update();
    }
}

