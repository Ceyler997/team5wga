using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
public class GameManager : MonoBehaviour {
    private List<Player> players; //Все игроки подключенные к игре
    private const int maxSuprimeAmount = 9; //Максимальнео кол-во ВС у одного игрока
    private const int minEnergySpeed = 1; //Минимальное скорость передачи энергии одному ВС от кристалла с энергией == 0 
    private const int maxEnergySpeed = 5; //Максимальная скорость передачи энергии одному ВС от кристалла с энергией > 0

    GameManager() {
        players = new List<Player>();
    }
    public Player GetPlayer(int id) {
        if(id >= 0 && id < players.Count) {
            return players[id];
        }
        return null;
    }
    public int MaxSuprimeAmount { get { return MaxSuprimeAmount;} }
    public int MinEnergySpeed { get {return minEnergySpeed;} }
    public int MaxEnergySpeed { get{return maxEnergySpeed;} }
    

}
