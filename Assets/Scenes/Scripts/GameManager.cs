using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
public class GameManager : MonoBehaviour {
    public List<Player> players; //Все игроки подключенные к игре
    public Crystal[] crystalls; //Все кристаллы на карте (при добавлении нового кристалла, обязательно добавить его сюда)
        
    void Start() {
        foreach(Player player in players) {
            player.setManager(this); 
        }
        foreach(Crystal crystall in crystalls) {
            crystall.Setup(null);
        }
        players[0].addCrystall(crystalls[0]);
        players[0].addCrystall(crystalls[1]);
        players[0].addCrystall(crystalls[2]);
    }
    public Player GetPlayer(int id) {
        if(id >= 0 && id < players.Count) {
            return players[id];
        }
        return null;
    }
}
