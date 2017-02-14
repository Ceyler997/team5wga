using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class NicknameFieldManager : MonoBehaviour {

    #region Public Fields
    public string defaultName = "Too lazy person";
    #endregion

    #region Private Fields
    private static string playerNamePrefKey = "PlayerName";
    #endregion

    #region MonoBehaviour Methods

    private void Start() {
        InputField nicknameField = GetComponent<InputField>();
        if(nicknameField != null) {
            if (PlayerPrefs.HasKey(playerNamePrefKey)) {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                nicknameField.text = defaultName;
            }
        } else {
            Debug.LogError("No input field for nickname");
        }
    }
    #endregion

    #region Public methods

    public void setNickname(string newNickname) {
        if(newNickname.Length > 0) {
            PlayerPrefs.SetString(playerNamePrefKey, newNickname);
        } else {
            newNickname = defaultName;
        }

        PhotonNetwork.playerName = newNickname;
    }
    #endregion
}

