using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class SuprimeUI : MonoBehaviour {

    public Sprite selectedSuprimeImage;
    public Sprite unselectedSuprimeImage;
    public Sprite noSuprimeImage;

    private Suprime subject;

    private Button SuprimeButton { get; set; }
    private Image SuprimeImage { get; set; }
    private Text SuprimeLevelText { get; set; }
    private bool IsSelected { get; set; }

    public Suprime Subject {
        get {
            return subject;
        }
        set {
            if (subject != value) {
                subject = value;
                if (subject == null) {
                    SuprimeImage.sprite = noSuprimeImage;
                    SuprimeLevelText.text = "";
                    SuprimeButton.interactable = false;
                } else {
                    SuprimeImage.sprite = unselectedSuprimeImage;
                    SuprimeButton.interactable = true;
                }
            }
        }
    }
    
	void Start () {
        SuprimeButton = GetComponent<Button>();
        SuprimeButton.onClick.AddListener(Select);
        SuprimeImage = GetComponent<Image>();
        SuprimeLevelText = GetComponentInChildren<Text>();

        SuprimeImage.sprite = noSuprimeImage;
        SuprimeLevelText.text = "";
        SuprimeButton.interactable = false;
    }

    private void Update() {
        if(subject != null) {
            bool selectCheck = false;
            ControllableUnit selectedUnit = CharactersController.Instance.SelectedUnit;

            if (selectedUnit != null && selectedUnit.Subject == Subject) {
                selectCheck = true;
            }

            if(IsSelected != selectCheck) {
                IsSelected = selectCheck;
                if (IsSelected) {
                    SuprimeImage.sprite = selectedSuprimeImage;
                } else {
                    SuprimeImage.sprite = unselectedSuprimeImage;
                }
            }

            SuprimeLevelText.text = subject.LevelSystem.CurrentLevel.ToString();
        }
    }

    public void Select() {
        if (Subject != null) {
            CharactersController.Instance.SelectUnit(Subject.GetComponent<ControllableUnit>());
        }
    }
}
