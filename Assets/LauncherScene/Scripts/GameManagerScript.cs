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
    private bool isConnecting = false; //TODO: check, will it be working in Awake()
    private Text messageText;
    #endregion

    #region MonoBehaviour Methods

    private void Awake() {
        PhotonNetwork.logLevel = logLevel;//Setting log level
        PhotonNetwork.automaticallySyncScene = true;//All players in the room will be loading same scene as master

        messageText = progressPanel.GetComponentInChildren<Text>();//Should I use it?
        Debug.Log("GameManager is awoken");
    }

    private void Start() {
        controlPanel.SetActive(true);
        progressPanel.SetActive(false);
    }
    #endregion

    #region PunBehaviour methods

    public override void OnJoinedLobby() {//called after connection to Photon
        Debug.Log("Connected and joined to lobby");

        if (PhotonNetwork.isMasterClient) {//if we are first in room
            messageText.text = "Waiting for sencond player";//TODO: check localisation
        }
    }

    public override void OnLeftLobby() {
        Debug.Log("Left lobby");
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause) {
        Debug.LogError("Failed to connect due to " + cause.ToString());

        messageText.text = "Failed to connect due to " + cause.ToString();//TODO: check localisation
    }

    public override void OnDisconnectedFromPhoton() {
        Debug.Log("Disconnected from PUN");
    }

    public override void OnJoinedRoom() {
        Debug.Log("Joined to room");

        messageText.text = "Ready to play!";//TODO: check localisation
    }

    public override void OnPhotonCreateRoomFailed(object [] codeAndMsg) {
        Debug.LogError("Failed to create room:");
        for(int errorCount = 0; errorCount < codeAndMsg.Length; ++errorCount) {
            Debug.Log("Room fail: " + codeAndMsg [errorCount].ToString());
        }

        messageText.text = "Failed to create room";//TODO: check localisation
    }

    public override void OnPhotonRandomJoinFailed(object [] codeAndMsg) {
        Debug.LogError("Failed to join room:");
        for (int errorCount = 0; errorCount < codeAndMsg.Length; ++errorCount) {
            Debug.Log("Room fail: " + codeAndMsg [errorCount].ToString());
        }

        messageText.text = "Failed to join room, creating new one";//TODO: check localisation

        PhotonNetwork.CreateRoom(
            "",//Name will be generated automatically
            new RoomOptions() { MaxPlayers = 2 },//ATTENTION hardcoded value
            null);//lobby for room, null for current lobby
    }

    public override void OnLeftRoom() {
        Debug.Log("Room left");
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {//new player entered the room
        Debug.Log("Second player connected");
        PhotonNetwork.LoadLevel(battleSceneName);//TODO: create this scene
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {//player left the room
        Debug.Log("Second player disconnected");
        PhotonNetwork.LoadLevel(0);//restarting game in case of disconnection while loading level
    }

    public override void OnPhotonMaxCccuReached() {//too many peoples connected to server for this subscribe 
        Debug.Log("Server is overloaded");

        messageText.text = "Please, wait. Server is overloaded.";//TODO: check localisation
    }
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
