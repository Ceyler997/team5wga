using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnergyUI : MonoBehaviour {

    public Energy target;

    private Slider slider;
    void Start() {
        if (target == null) {
            throw new SystemIsNotSettedUpException();
        }

        slider = GetComponent<Slider>();
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update() {
        slider.maxValue = target.MaxEnergy;
        slider.value = target.CurrentEnergy;
    }
}
