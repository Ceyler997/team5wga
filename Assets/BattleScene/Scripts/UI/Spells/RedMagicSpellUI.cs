using UnityEngine.UI;

public class RedMagicSpellUI : SpellUI {

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
            Spell = suprime.Magic.Red;
            LevelText.text = suprime.Magic.Red.LevelSystem.CurrentLevel.ToString();
        } else {
            Spell = null;
            LevelText.text = "";
        }

        base.Update();
    }
}
