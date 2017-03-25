using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suprime : BaseObject {
    [HeaderAttribute("Suprime Property")]
    public float distanceOfCapture = 10f; //Дистанция, при которой возможен захват
    public float energy = 0f; //Текущее кол-во энергии
    public float maxEnergy = 50f; //Максимальное кол-во энергии у ВС
    public Crystall curentCrystall = null; //текущий кристалл, в радиусе которого находится ВС
    public Health health; //здоровье ВС
    void Start() {
        //Жизя
        health = getHealt();
    }

    private Health getHealt() {
        return new Health(getPlayer.getManager.MaxSuprimeHealth,
                                     getPlayer.getManager.MaxSuprimeHealth,
                                     getPlayer.getManager.SuprimeRegenPerSecond);
    }

    //Вызывается кристаллом, при пересечении ВС радиуса кристалла
    public void setCurentCrystall(Crystall crystall) {
        curentCrystall = crystall;
    }

}
