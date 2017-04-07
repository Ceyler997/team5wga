using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
public class GameManager : MonoBehaviour {
    #region public fields

    [Tooltip("Все игроки подключенные к игре")]
    public List<Player> players; //Все игроки подключенные к игре
    [Tooltip("Все кристаллы на карте")]
    public Crystal[] crystalls; //Все кристаллы на карте (при добавлении нового кристалла, обязательно добавить его сюда)
    #endregion
    
    public void Start() {
        foreach(Player player in players) {
            player.Manager(this);
        }
    }

    public Player GetPlayer(int id) {
        if(id >= 0 && id < players.Count) {
            return players[id];
        } else {
            throw new PlayerNotExistingException();
        }
    }
}
