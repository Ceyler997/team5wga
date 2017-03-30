using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suprime : BaseObject {
    [HeaderAttribute("Suprime Property")]
    public float distanceOfCapture = 10f; //Дистанция, при которой возможен захват
    public float energy = 0f; //Текущее кол-во энергии
    public float maxEnergy = 50f; //Максимальное кол-во энергии у ВС
    public Crystall curentCrystall = null; //текущий кристалл, в радиусе которого находится ВС
    private Health health; //здоровье ВС
    public Player controllPlayer;
    public Unit [] units;

    private Health Health {
        get { return health; }

        set { health = value; }
    }

    void Start() {
        controllPlayer = GetComponent<Player>();

        Health = GetComponent<Health>();
        Health.setupSystem(GameConf.suprimeStartHealth,
            GameConf.suprimeMaxHealth, 
            GameConf.suprimeBasicRegenSpeed);
    }

    //Вызывается кристаллом, при пересечении ВС радиуса кристалла
    public void setCurentCrystall(Crystall crystall) {
        curentCrystall = crystall;
    }

}
