using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CrystalUI : MonoBehaviour {

    public Sprite enemyCrystalImage;
    public Sprite neutralCrystalImage;
    public Sprite frienlyCrystalImage;
    public Crystal observedCrystal;

    private Image CrystalImage { get; set; }
    private Player CrystalOwner { get; set; }

    private void Start() {
        CrystalImage = GetComponent<Image>();
        CrystalOwner = null;
        CrystalImage.sprite = neutralCrystalImage;
    }

    private void Update() {
        if (CrystalOwner != observedCrystal.ControllingPlayer) {
            CrystalOwner = observedCrystal.ControllingPlayer;

            if (CrystalOwner == null) {
                CrystalImage.sprite = neutralCrystalImage;
            } else if (CrystalOwner.ID == PhotonNetwork.player.ID) {
                CrystalImage.sprite = frienlyCrystalImage;
            } else {
                CrystalImage.sprite = enemyCrystalImage;
            }
        }
    }
}
