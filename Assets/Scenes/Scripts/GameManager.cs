using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
public class GameManager : MonoBehaviour {
    private List<Player> players; //Все игроки подключенные к игре
    private const float minEnergySpeed = 1f; //Минимальное скорость передачи энергии одному ВС от кристалла с энергией == 0 
    private const float maxEnergySpeed = 5f; //Максимальная скорость передачи энергии одному ВС от кристалла с энергией > 0

    [HeaderAttribute("Suprime Property")]
    private const int maxSuprimeAmount = 9; //Максимальнео кол-во ВС у одного игрока
    private const float maxSuprimeHealth = 100f; //Максимальное здоровье у ВС
    private const float maxSuprimeEnergy = 100f; //Максимальное кол-во энергии у ВС
    private const float suprimeRegenPerSecond = 1f; //Скорость восстановления жизни ВС


    public GameManager() {
        players = new List<Player>();
    }
    public Player GetPlayer(int id) {
        if(id >= 0 && id < players.Count) {
            return players[id];
        }
        return null;
    }
    public int MaxSuprimeAmount { get { return maxSuprimeAmount;} }
    public float MinEnergySpeed { get {return minEnergySpeed;} }
    public float MaxEnergySpeed { get{return maxEnergySpeed;} }

    public float MaxSuprimeHealth {get { return maxSuprimeHealth; } }
    public float MaxSuprimeEnergy {get { return maxSuprimeEnergy; } }
    public float SuprimeRegenPerSecond {get { return suprimeRegenPerSecond; } }
    

}
