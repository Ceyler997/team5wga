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

    public override void OnJoinedLobby() {//called after connection to Photon
        throw new System.NotImplementedException();
    }

    public override void OnLeftLobby() {
        throw new System.NotImplementedException();
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause) {
        base.OnFailedToConnectToPhoton(cause);
    }

    public override void OnDisconnectedFromPhoton() {
        throw new System.NotImplementedException();
    }

    public override void OnJoinedRoom() {
        throw new System.NotImplementedException();
    }

    public override void OnPhotonCreateRoomFailed(object [] codeAndMsg) {
        throw new System.NotImplementedException();
    }

    public override void OnPhotonRandomJoinFailed(object [] codeAndMsg) {
        throw new System.NotImplementedException();
    }

    public override void OnLeftRoom() {
        throw new System.NotImplementedException();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {//new player entered the room
        throw new System.NotImplementedException();
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {//player left the room
        throw new System.NotImplementedException();
    }

    public override void OnPhotonMaxCccuReached() {//too many peoples connected to server for this subscribe plan
        throw new System.NotImplementedException();
    }

    /* USEFULL FUNCTIONS OF PhotonNetwork
     * ReconnectAndRejoin()
     * Disconnect()
     * CreateRoom(name, options, typedLobby)
     * JoinRandomRoom()
     * LoadLevel(levelName)
     */
    #endregion

    #region Public methods

    public void Connect() {
        isConnecting = true;
        controlPanel.SetActive(false);
        progressPanel.SetActive(true);

        if (PhotonNetwork.connected) {
            Debug.Log("Already connected to PUN, start joining random room");
            PhotonNetwork.JoinRandomRoom();
        } else {
            Debug.Log("Not connected, starting connection with settings (game version)");
            PhotonNetwork.ConnectUsingSettings(gameVersion);
            //somewhere between Awake and this string Photon changing his loglevel to set in properties, we need to change it back
            PhotonNetwork.logLevel = logLevel;
        }
    }

    public void CancelConnection() {
        isConnecting = false;
        PhotonNetwork.Disconnect();
    }

    public void Exit() {
        CancelConnection();
        Debug.Log("Quitting");
        Application.Quit();
    }
    #endregion
}
