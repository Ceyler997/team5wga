using System;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
// Singletone
public class GameManager : Photon.PunBehaviour{

    #region private fields

    private static GameManager instance;
    #endregion

    #region public fields

    [Tooltip("Все игроки подключенные к игре")]
    private Dictionary<int, Player> players; //Все игроки подключенные к игре
    [Tooltip("Все кристаллы на карте")]
    public Crystal[] crystals; //Все кристаллы на карте (при добавлении нового кристалла, обязательно добавить его сюда)
    #endregion

    #region properties

    public static GameManager Instance {
        get {return instance;}
        set {
            if(instance == null) {
                instance = value;
            } else {
                throw new AttemptToManagerReassignmentException();
            }
        }
    }

    public Dictionary<int, Player> Players {
        get {
            if (players == null) {
                players = new Dictionary<int, Player>();
            }
            return players;
        }
    }

    #endregion

    #region MonoBehaviour methods

    public void Start() {
        Instance = this; // singletone
        if(OfflineGameManager.Instance != null) {
            Debug.LogError("Both online and offline managers is turned on!");
        }

        StartPosition [] startPositions = GetComponentsInChildren<StartPosition>(); // получаем все стартовые позиции

        PhotonNetwork.Instantiate("PlayerPrefab",
            startPositions [PhotonNetwork.player.ID - 1].Position, // позиция, соответвующая игроку (отсчёт ID начинается с мастера - 1)
            Quaternion.identity,
            0);

        foreach (Crystal crystal in crystals) {
            crystal.SetupCrystal(null);
        }
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            print("Esc pressed");
            PhotonNetwork.LeaveRoom();
        }
    }
    #endregion

    #region PunBehaviour methods

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {
        print(otherPlayer.NickName + " disconnected, loading login screen");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnDisconnectedFromPhoton() {
        print("Diconnection, loading login sceen");
        LoadLoginScreen();
    }

    public override void OnLeftRoom() {
        print("Room left, loading login screen");
        LoadLoginScreen();
    }
    #endregion

    private void LoadLoginScreen() {
        PhotonNetwork.LoadLevel(0); // go to load screen
    }
}

