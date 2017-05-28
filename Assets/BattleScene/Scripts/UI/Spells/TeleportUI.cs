
public class TeleportUI : SpellUI {
    protected override void Update() {
        ControllableUnit unit = CharactersController.Instance.SelectedUnit;

        if (unit != null) {
            Suprime suprime = unit.Subject;
            Spell = suprime.Magic.Teleport;
        } else {
            Spell = null;
        }

        base.Update();
    }
}
