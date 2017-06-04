using UnityEngine.UI;

public class BlackMagicSpellUI : SpellUI {

    public Text LevelText;

    protected override void Start() {
        base.Start();
        if (LevelText == null) {
            throw new SystemIsNotSettedUpException();
        }
    }
    protected override void Update() {
        ControllableUnit unit = CharactersController.Instance.SelectedUnit;

        if (unit != null) {
            Suprime suprime = unit.Subject;
            Spell = suprime.Magic.Black;
            LevelText.text = suprime.Magic.Black.LevelSystem.CurrentLevel.ToString();
        } else {
            Spell = null;
            LevelText.text = "";
        }

        base.Update();
    }
}
