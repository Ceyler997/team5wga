using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public Text infoText;
    
	public void SetText(string text) {
        infoText.text = text;
    }
}
