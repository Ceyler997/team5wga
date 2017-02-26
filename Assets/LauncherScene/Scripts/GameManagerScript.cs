using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : Photon.PunBehaviour {

    #region Public Fields

    [Tooltip("Panel for input")]
    public GameObject controlPanel;

    [Tooltip("Panel for output")]
    public GameObject progressPanel;

    [Tooltip("Log level for Photon")]
    public PhotonLogLevel logLevel;

    [Tooltip("Game version")]
    public string gameVersion = "0";

    [Tooltip("Name of scene for load to battle")]
    public string battleSceneName = "BattlegroundScene";
    #endregion

    #region Private Fields
    private bool isConnecting;
    private Text messageText;
    #endregion

    #region MonoBehaviour Methods

    private void Awake() {
        PhotonNetwork.logLevel = logLevel;//Setting log level
        PhotonNetwork.autoJoinLobby = true;//joining to lobby on connecting
        PhotonNetwork.automaticallySyncScene = true;//All players in the room will be loading same scene as master
        //Joined lobby message called if we switched from another scene, so we need to check, is player pressed button
        isConnecting = false;

        messageText = progressPanel.GetComponentInChildren<Text>();//TODO: check, should I use it
        Debug.Log("GameManager is awoken");
    }

    private void Start() {
        controlPanel.SetActive(true);
        progressPanel.SetActive(false);
    }
    #endregion

    #region PunBehaviour methods

    #region Lobby messages

    public override void OnJoinedLobby() {//called after connection to Photon
        Debug.Log("Connected and joined to lobby");

        if (isConnecting) {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnLeftLobby() {
        Debug.Log("Left lobby");
    }
    #endregion

    #region Connection messages

    public override void OnFailedToConnectToPhoton(DisconnectCause cause) {//TODO: pop-up
        Debug.LogError("Failed to connect due to " + cause.ToString());

        messageText.text = "Failed to connect due to " + cause.ToString();//TODO: check localisation
    }

    public override void OnDisconnectedFromPhoton() {
        Debug.Log("Disconnected from PUN");

        messageText.text = "Diconnected";//TODO: check localisation
    }
    #endregion

    #region Room messages

    public override void OnJoinedRoom() {
        Debug.Log("Joined to room");

        if (PhotonNetwork.isMasterClient) {//if we are first in room
            messageText.text = "Waiting for sencond player";//TODO: check localisation
        } else {//if not, master will load level
            messageText.text = "Ready to play!";//TODO: check localisation
        }
    }
    
    public override void OnPhotonCreateRoomFailed(object [] codeAndMsg) {//TODO: pop-up
        Debug.LogError("Failed to create room:\n" +
            codeAndMsg [0].ToString() + " " + codeAndMsg [1].ToString());

        messageText.text = "Failed to create room";//TODO: check localisation
    }

    public override void OnPhotonRandomJoinFailed(object [] codeAndMsg) {
        Debug.Log("Failed to join room:\n" +
            codeAndMsg [0].ToString() + " " + codeAndMsg [1].ToString());

        messageText.text = "Failed to join room, creating new one";//TODO: check localisation

        PhotonNetwork.CreateRoom(
            "",//Name will be generated automatically
            new RoomOptions() { MaxPlayers = 2 },//TODO: ATTENTION: hardcoded value
            null);//lobby for room, null for current lobby
    }

    public override void OnLeftRoom() {
        Debug.Log("Room left");
    }
    #endregion

    #region Players messages

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {//new player entered the room
        Debug.Log("Player " + newPlayer.NickName + " connected");

        messageText.text = "Ready to play!";//TODO: check localisation
        PhotonNetwork.LoadLevel(battleSceneName);//TODO: create this scene
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {//player left the room
        Debug.Log("Player " + otherPlayer.NickName + " disconnected");
        CancelConnection();
        PhotonNetwork.LoadLevel(0);//restarting game in case of disconnection while loading level
    }
    #endregion

    #region Server messages
    //NOT TESTED
    public override void OnPhotonMaxCccuReached() {//too many peoples connected to server for this subscribe plan
        Debug.Log("Server is overloaded");

        messageText.text = "Please, wait. Server is overloaded.";//TODO: check localisation
    }
    #endregion
    #endregion

    #region Public methods

    public void Connect() {
        isConnecting = true;
        controlPanel.SetActive(false);
        progressPanel.SetActive(true);
        messageText.text = "Connecting...";

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
        Debug.Log("Canceling connection");
        isConnecting = false;
        if (PhotonNetwork.connected) {
            PhotonNetwork.Disconnect();
        }

        controlPanel.SetActive(true);
        progressPanel.SetActive(false);
    }

    public void Exit() {
        CancelConnection();
        Debug.Log("Quitting");
        Application.Quit();
    }
    #endregion
}
