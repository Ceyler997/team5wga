using System;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
// Singletone
public class GameManager : Photon.PunBehaviour{

    #region private fields

    private static GameManager instance;
    private Dictionary<int, Player> players; // Все игроки подключенные к игре в паре ID - Player
    #endregion

    #region public fields

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

        StartPosition [] startPositions = GetComponentsInChildren<StartPosition>(); // получаем все стартовые позиции
        
        foreach (Crystal crystal in crystals) {
            crystal.SetupCrystal(null);
        }

        PhotonNetwork.Instantiate("PlayerPrefab",
            startPositions [PhotonNetwork.player.ID - 1].Position, // позиция, соответвующая игроку (отсчёт ID начинается с мастера - 1)
            Quaternion.identity,
            0,
            new object [] {
                startPositions [PhotonNetwork.player.ID - 1].startCrystal.photonView.viewID
            });

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

    //Изменяет владельцев кристалла (пока временная реализация, изменить на что-то прекрасное :D )
    // - отказаться от хранения списков кристаллов у каждого игрока, перенести список в менеджер
    public void SwapCrystals(Player player, Crystal crystal) {
        // Добавляем игроку кристалл
        player.AddCrystal(crystal);

        // Если не нейтрал, то удаляем у игрока противоположной комманты кристалл 
        if (crystal.ControllingPlayer != null) {
            crystal.ControllingPlayer.Crystals.Remove(crystal);
        }

        //Захватываем кристалл
        crystal.ChangeOwner(player);
    }

    private void LoadLoginScreen() {
        PhotonNetwork.LoadLevel(0); // go to load screen
    }
}

