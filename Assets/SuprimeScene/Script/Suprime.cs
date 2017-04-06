using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Health))]
[RequireComponent (typeof (Energy))]
[RequireComponent (typeof (Level))]
public class Suprime : BaseObject, IFightable, IEnemyChecker {
    [HeaderAttribute("Suprime Property")]
    public Crystall curentCrystall = null; //текущий кристалл, в радиусе которого находится ВС
    private Health health; //здоровье ВС
    private Energy energy;//энергия ВС
    private Level level; //Уровень ВС

    void Start() {
        health = GetComponent<Health>();
        energy = GetComponent<Energy>();
        level  = GetComponent<Level>();
        setEnergy();
        setHealth();
        setLevel();
        setRadius(10);
    }

    void Update() {

    }

    void setEnergy() {
        energy.setEnergy(ControllPlayer.getManager.MaxSuprimeEnergy,
                        ControllPlayer.getManager.MaxSuprimeEnergy);
    }
    void setHealth() {
        health.setHealth(ControllPlayer.getManager.MaxSuprimeHealth,
                         ControllPlayer.getManager.MaxSuprimeHealth,
                         ControllPlayer.getManager.SuprimeRegenPerSecond, this);
    }
    void setLevel() {
        level.setup(ControllPlayer.getManager.SuprimeMaxLevel,ControllPlayer.getManager.SuprimeStartLevel);
    }

    //Вызывается кристаллом, при пересечении ВС радиуса кристалла
    public void setCurentCrystall(Crystall crystall) {
        curentCrystall = crystall;
    }
     public void setSuprime(Player player) {
        setPlayer(player);
    }
    public override void EnemyCheck(BaseObject enemy) {
        Debug.Log("Suprime has saw an enemy!");
    }
    void IFightable.die() {
        Destroy(gameObject);
    }

    public override void setRadius(float size) {
        Radius.Initialization(size, this);
    }
}
