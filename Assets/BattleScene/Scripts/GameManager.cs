using System;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
// Singletone
public class GameManager : MonoBehaviour{

    #region private fields

    private static GameManager instance;
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
    
    #endregion

    #region public fields

    [Tooltip("Все игроки подключенные к игре")]
    public List<Player> players; //Все игроки подключенные к игре
    [Tooltip("Все кристаллы на карте")]
    public Crystal[] crystals; //Все кристаллы на карте (при добавлении нового кристалла, обязательно добавить его сюда)
    #endregion

    #region public methods

    public Player GetPlayer(int id) {
        if(id >= 0 && id < players.Count) {
            return players[id];
        } else {
            throw new PlayerNotExistingException();
        }
    }
    #endregion

    #region MonoBehaviour methods

    public void Start() {
        Instance = this; // singletone

        foreach(Crystal crystal in crystals) {
            crystal.setupCrystal(null);
        }
        
        foreach (Player player in players) {
            player.setupPlayer();
        }
    }
    #endregion
}

