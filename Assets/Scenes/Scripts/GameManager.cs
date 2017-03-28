using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
public class GameManager : MonoBehaviour {
    public List<Player> players; //Все игроки подключенные к игре
    public Crystal[] crystalls; //Все кристаллы на карте (при добавлении нового кристалла, обязательно добавить его сюда)

    [HeaderAttribute("Suprime Property")]
    private const int maxSuprimeAmount = 9; //Максимальнео кол-во ВС у одного игрока
    private const float maxSuprimeHealth = 100f; //Максимальное здоровье у ВС
    private const float maxSuprimeEnergy = 100f; //Максимальное кол-во энергии у ВС
    private const float suprimeRegenPerSecond = 1f; //Скорость восстановления жизни ВС
    private const int suprimeStartLevel = 1; //Начальный уровень ВС
    private const int suprimeMaxLevel = 5; //Максимальный уровень ВС
    public const float distanceOfCapture = 10f; //Дистанция, при которой возможен захват

    [HeaderAttribute("Crystall Property")]
    private const int crystallStartLevel = 1; //Начальный уровень кристалла
    private const int crystallMaxLevel = 3; //Максимальный уровень кристалла
    private const float crystallRegenEnergyLvl1 = 1f; //Скорость регенерации энергии кристаллом на 1 уровне
    private const float crystallRegenEnergyLvl2 = 2f; //Скорость регенерации энергии кристаллом на 2 уровне
    private const float crystallRegenEnergyLvl3 = 3f; //Скорость регенерации энергии кристаллом на 3 уровне
    private const float crystallMinEnergySpeed = 1f; //Минимальное скорость передачи энергии одному ВС от кристалла с энергией == 0 
    private const float crystallMaxEnergySpeed = 5f; //Максимальная скорость передачи энергии одному ВС от кристалла с энергией > 0
    private const float crystallRadiusFirst = 10f; //Радиус А (ближний), кристалла
    private const float crystallRadiusSecond = 20f; //Радиус Б (дальний), кристалла
    
    
    void Start() {
        foreach(Player player in players) {
            player.setManager(this);
        }

        foreach(Crystal crystall in crystalls) {
            crystall.Initialization(this);
        }
    }
    public Player GetPlayer(int id) {
        if(id >= 0 && id < players.Count) {
            return players[id];
        }
        return null;
    }
    public int MaxSuprimeAmount { get { return maxSuprimeAmount;} }
    public float MinEnergySpeed { get {return crystallMinEnergySpeed;} }
    public float MaxEnergySpeed { get{return crystallMaxEnergySpeed;} }
    public float MaxSuprimeHealth {get { return maxSuprimeHealth; } }
    public float MaxSuprimeEnergy {get { return maxSuprimeEnergy; } }
    public float SuprimeRegenPerSecond {get { return suprimeRegenPerSecond; } }
    public int SuprimeMaxLevel {get { return suprimeMaxLevel; } }
    public int SuprimeStartLevel {get { return suprimeStartLevel; } }
    public float DistanceOfCapture { get { return distanceOfCapture; } }
    public int CrytallMaxLevel {get { return crystallMaxLevel; } }
    public int CrytallStartLevel {get { return crystallStartLevel; } }
    public float CrystallGetFirstRadius{get { return crystallRadiusFirst; } }
    public float CrystallGetSecondRadius{get { return crystallRadiusSecond; } }
    
    
    public float GetCrystallRegenEnergySpeed(int lvl) {
        switch(lvl) {
            case 1 : return crystallRegenEnergyLvl1;
            case 2 : return crystallRegenEnergyLvl2;
            case 3 : return crystallRegenEnergyLvl3;
            default : return crystallRegenEnergyLvl1;
        }
    }

}
