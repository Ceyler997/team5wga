using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Health))]
[RequireComponent (typeof (Energy))]
[RequireComponent (typeof (Level))]
public class Suprime : BaseObject, IFightable {
    [HeaderAttribute("Suprime Property")]
    public Crystall curentCrystall = null; //текущий кристалл, в радиусе которого находится ВС
    private Health health; //здоровье ВС
    private Energy energy;//энергия ВС
    private Player controllPlayer; //Игрок, который управляет ВС
    private Level level; //Уровень ВС
    public void Initialize() {
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

    public void setPlayer(Player player) {
        controllPlayer = player;
        if(controllPlayer!= null)
            Initialize();
    }

    void IFightable.die() {
        Destroy(gameObject);
    }
}
