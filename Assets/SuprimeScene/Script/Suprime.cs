using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Player))]
[RequireComponent (typeof (Health))]
[RequireComponent (typeof (Energy))]
[RequireComponent (typeof (Level))]
public class Suprime : BaseObject, IFightable {
    [HeaderAttribute("Suprime Property")]
    public float distanceOfCapture = 10f; //Дистанция, при которой возможен захват
    public Crystall curentCrystall = null; //текущий кристалл, в радиусе которого находится ВС
    public Health health; //здоровье ВС
    public Energy energy;//энергия ВС
    public Player controllPlayer; //Игрок, который управляет ВС
    public Level level; //Уровень ВС
    void Start() {
        controllPlayer = GetComponent<Player>();
        health = GetComponent<Health>();
        energy = GetComponent<Energy>();
        level  = GetComponent<Level>();
        setEnergy();
        setHealth();
        setLevel();
    }

    void Update() {

    }

    void setEnergy() {
        energy.setEnergy(controllPlayer.getManager.MaxSuprimeEnergy,
                        controllPlayer.getManager.MaxSuprimeEnergy);
    }
    void setHealth() {
        health.setHealth(controllPlayer.getManager.MaxSuprimeHealth,
                         controllPlayer.getManager.MaxSuprimeHealth,
                         controllPlayer.getManager.SuprimeRegenPerSecond, this);
    }
    void setLevel() {
        level.setup(controllPlayer.getManager.SuprimeMaxLevel,controllPlayer.getManager.SuprimeStartLevel);
    }

    //Вызывается кристаллом, при пересечении ВС радиуса кристалла
    public void setCurentCrystall(Crystall crystall) {
        curentCrystall = crystall;
    }

    void IFightable.die() {
        Destroy(gameObject);
    }
}
