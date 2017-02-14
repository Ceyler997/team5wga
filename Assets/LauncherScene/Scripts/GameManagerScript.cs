using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : Photon.PunBehaviour {

    #region Public Fields

    [Tooltip("Panel for input")]
    public GameObject controlPanel;

    [Tooltip("Panel for output")]
    public GameObject progressPanel;

    [Tooltip("Log level for Photon")]
    public PhotonLogLevel logLevel;

    [Tooltip("Game version")]
    public string gameVersion = "1";
    #endregion

    #region Private Fields
    private bool isConnecting = false; //TODO: check, will it be working in Awake()
    #endregion

    #region MonoBehaviour Methods


    #endregion

    #region PunBehaviour methods


    #endregion

    #region Public methods

    public void Connect() {
        isConnecting = true;
        controlPanel.SetActive(false);
        progressPanel.SetActive(true);

        if (PhotonNetwork.connected) {
            Debug.Log("Connected to PUN, start joining random room");
            PhotonNetwork.JoinRandomRoom();
        } else {
            Debug.Log("Not connected, starting connection with settings (game version)");
            PhotonNetwork.ConnectUsingSettings(gameVersion);
            //somewhere between Awake and this string Photon changing his loglevel to set in properties, we need to change it back
            PhotonNetwork.logLevel = logLevel;
        }
    }

    public void CancelConnection() {
        throw new System.NotImplementedException();
    }

    public void Exit() {
        CancelConnection();
        Debug.Log("Quitting");
        Application.Quit();
    }
    #endregion
}
