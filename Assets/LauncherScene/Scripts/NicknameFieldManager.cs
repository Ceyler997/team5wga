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
            if (PlayerPrefs.HasKey(playerNamePrefKey)) {//checking, if we have saved nickname
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);//setting it in field, if we have
                nicknameField.text = defaultName;
                PhotonNetwork.playerName = defaultName;
            }
        } else {
            Debug.LogError("No input field for nickname");
        }
    }
    #endregion

    #region Public methods

    public void setNickname(string newNickname) {
        if(newNickname.Length > 0) {//if we got something
            PlayerPrefs.SetString(playerNamePrefKey, newNickname);//saving new player nickname
        } else {
            newNickname = defaultName;//setting default name, if we got nothing
        }

        PhotonNetwork.playerName = newNickname;//setting this player name in PUN
    }
    #endregion
}

