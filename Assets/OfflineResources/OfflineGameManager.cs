using System;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
// Singletone
public class OfflineGameManager : MonoBehaviour {

    #region public fields

    public GameObject suprimePrefab;
    public GameObject unitPrefab;
    #endregion

    #region private fields

    private static OfflineGameManager instance;
    #endregion

    #region properties

    public static OfflineGameManager Instance {
        get { return instance; }
        set {
            if (instance == null) {
                instance = value;
            } else {
                throw new AttemptToManagerReassignmentException();
            }
        }
    }

    #endregion

    #region public fields

    [Tooltip("Все игроки подключенные к игре")]
    public List<Player> players; //Все игроки подключенные к игре
    [Tooltip("Все кристаллы на карте")]
    public Crystal [] crystals; //Все кристаллы на карте (при добавлении нового кристалла, обязательно добавить его сюда)
    #endregion

    #region public methods

    public Player GetPlayer(int id) {
        if (id >= 0 && id < players.Count) {
            return players [id];
        } else {
            throw new PlayerNotExistingException();
        }
    }
    #endregion

    #region MonoBehaviour methods

    public void Start() {
        Instance = this; // singletone

        if (GameManager.Instance != null) {
            Debug.LogError("Both online and offline managers is turned on!");
        }

        foreach (Crystal crystal in crystals) {
            crystal.SetupCrystal(null);
        }

        foreach (Player player in players) {
            player.setupPlayer(player.name);
            player.addSuprime(player.transform.position);
        }
    }

    //Изменяет владельцев кристалла (пока временная реализация, изменить на что-то прекрасное :D )
    // - отказаться от хранения списков кристаллов у каждого игрока, перенести список в менеджер
    public void SwapCrystalls(Player player, Crystal crystall) {
        // Добавляем игроку кристалл
        player.addCrystall(crystall);

        // Если не нейтрал, то удаляем у игрока противоположной комманты кристалл 
        if (crystall.owner != null) {
            crystall.owner.Crystals.Remove(crystall);
            crystall.owner = player;
        }
        //Захватываем кристалл
        crystall.owner = player;
    }
    #endregion
}

