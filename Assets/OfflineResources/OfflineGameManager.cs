using System;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
// Singletone
public class OfflineGameManager : GameManager {

    #region public fields

    public GameObject suprimePrefab;
    public GameObject unitPrefab;
    #endregion

    #region public fields

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
            player.setupPlayer(player.name);
            player.addSuprime(player.transform.position);
        }
    }
    #endregion
}

