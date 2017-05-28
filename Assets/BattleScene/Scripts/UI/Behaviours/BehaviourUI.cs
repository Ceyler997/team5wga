using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
abstract public class BehaviourUI : MonoBehaviour {
    protected Button BehaviourButton { get; private set; }

    // Use this for initialization
    void Start () {
        BehaviourButton = GetComponent<Button>();
        BehaviourButton.onClick.AddListener(ApplyBehaviour);
    }

    // Update is called once per frame
    void Update() {
        BehaviourButton.interactable = IsEnable();
    }

    abstract public void ApplyBehaviour();
    abstract protected bool IsEnable();
}

public enum BehaviourStates {
    SUPRIME_PROTECT,
    ATTACK, 
    CRYSTAL_PROTECT
}