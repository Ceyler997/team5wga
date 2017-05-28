
using UnityEngine;

public class CrystalInteractUI : SpellUI {
    public Crystal crystal;
    public Sprite captureSprite;
    public Sprite lvlUpSprite;

    protected override void Update() {
        ControllableUnit unit = CharactersController.Instance.SelectedUnit;

        if (unit != null) {
            Suprime suprime = unit.Subject;
            if (suprime.CurrentCrystal == crystal) {
                Magic newSpell;

                if (crystal.photonView.isMine) {
                    newSpell = suprime.Magic.LvlUpCrystal;
                    activeSprite = lvlUpSprite;
                } else {
                    newSpell = suprime.Magic.CaptureCrystal;
                    activeSprite = captureSprite;
                }

                if (Spell != newSpell) { // обновляем изображение
                    SpellImage.sprite = activeSprite;
                    Spell = newSpell;
                }
            }

        } else {
            Spell = null;
        }

        base.Update();
    }
}
