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

    #region MonoBehaviour Methods


    #endregion

    #region PunBehaviour methods


    #endregion

    #region Public methods

    public void Connect() {
        throw new System.NotImplementedException();
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
