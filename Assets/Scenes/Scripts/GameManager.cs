using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
public class GameManager : MonoBehaviour {
    public List<Player> players; //Все игроки подключенные к игре
    private const float minEnergySpeed = 1f; //Минимальное скорость передачи энергии одному ВС от кристалла с энергией == 0 
    private const float maxEnergySpeed = 5f; //Максимальная скорость передачи энергии одному ВС от кристалла с энергией > 0

    [HeaderAttribute("Suprime Property")]
    private const int maxSuprimeAmount = 9; //Максимальнео кол-во ВС у одного игрока
    private const float maxSuprimeHealth = 100f; //Максимальное здоровье у ВС
    private const float maxSuprimeEnergy = 100f; //Максимальное кол-во энергии у ВС
    private const float suprimeRegenPerSecond = 1f; //Скорость восстановления жизни ВС
    private const int suprimeStartLevel = 1; //Начальный уровень ВС
    private const int suprimeMaxLevel = 5; //Максимальный уровень ВС
    public const float distanceOfCapture = 10f; //Дистанция, при которой возможен захват
    void Start() {
       foreach(Player player in players) {
            player.setManager(this);
        } 
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
    public int SuprimeMaxLevel {get { return suprimeMaxLevel; } }
    public int SuprimeStartLevel {get { return suprimeStartLevel; } }
    public float DistanceOfCapture { get { return distanceOfCapture; } }


}
