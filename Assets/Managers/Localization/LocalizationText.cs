using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour {

    #region Public Fields

    [Tooltip("Text field for localization")]
    public Text textField;

    [Tooltip("Key value for localization")]
    public string key;
    #endregion

    #region MonoBehaviour Methods
    
    void Start() {
        UpdateText();
	}
    #endregion

    #region Public Methods

    public void UpdateText() {
        textField.text = LocalizationManager.getTextByKey(key);
    }
    #endregion
}
