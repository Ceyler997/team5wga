using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
abstract public class SpellUI : MonoBehaviour {
    public Sprite activeSprite;
    public Sprite unactiveSprite;

    protected Magic Spell { get; set; }
    protected Button SpellButton { get; private set; }
    protected Image SpellImage { get; private set; }

    private void Start() {
        SpellButton = GetComponent<Button>();
        SpellButton.onClick.AddListener(Cast);

        SpellImage = GetComponent<Image>();

        if (activeSprite != null) {
            SpellImage.sprite = activeSprite;
        } else {
            throw new SystemIsNotSettedUpException();
        }
    }

    protected virtual void Update() {
        bool spellState = false;

        if (Spell != null) {
            spellState = Spell.IsAbleToStartCast;
        }


        if(spellState != SpellButton.interactable) {
            SpellButton.interactable = spellState;

            if (unactiveSprite != null) {
                if (spellState) {
                    SpellImage.sprite = activeSprite;
                } else {
                    SpellImage.sprite = unactiveSprite;
                }

            }
        }
    }

    public void Cast() {
        if(Spell != null) {
            Spell.TryCast();
        }
    }
}
