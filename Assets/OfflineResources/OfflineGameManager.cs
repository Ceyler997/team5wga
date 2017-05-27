using System;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
// Singletone
public class OfflineGameManager : GameManager {

    #region public fields
    [Tooltip("Префаб супрайма для создания")]
    public GameObject suprimePrefab;

    [Tooltip("Префаб юнита для создания")]
    public GameObject unitPrefab;

    [Tooltip("Все игроки подключенные к игре")]
    public List<Player> players; //Все игроки подключенные к игре
    #endregion

    #region MonoBehaviour methods

    new public void Start() {
        Instance = this; // singletone

        foreach (Crystal crystal in crystals) {
            crystal.SetupCrystal(null);
        }

        foreach (Player player in players) {
            player.SetupPlayer(player.name);
            player.AddSuprime(player.transform.position);
        }
    }
    #endregion

    #region PhotonMessages overriding

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) { }
    public override void OnDisconnectedFromPhoton() { }
    public override void OnLeftRoom() { }
    #endregion
}

