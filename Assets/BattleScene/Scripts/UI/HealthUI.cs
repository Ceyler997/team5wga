using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthUI : MonoBehaviour {

    public Health target;

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
        slider.maxValue = target.MaxHealth;
        slider.value = target.CurrentHealth;
    }
}
