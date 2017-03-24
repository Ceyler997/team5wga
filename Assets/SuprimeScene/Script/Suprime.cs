using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suprime : BaseObject {
    [HeaderAttribute("Suprime Property")]
    public float distanceOfCapture = 10f; //Дистанция, при которой возможен захват
    public float energy = 0f; //Текущее кол-во энергии
    public float maxEnergy = 50f; //Максимальное кол-во энергии у ВС
    public List<Crystall> crystalls; //Кристаллы под контролем игрока
    public Crystall curentCrystall = null; //текущий кристалл, в радиусе которого находится ВС
    public Health health; //здоровье ВС
    void Start() {
        health = new Health( getPlayer.getManager.MaxSuprimeHealth, 
                             getPlayer.getManager.MaxSuprimeEnergy, 
                             getPlayer.getManager.SuprimeRegenPerSecond);
    }
}
