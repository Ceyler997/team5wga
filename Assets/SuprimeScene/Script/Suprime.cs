using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Health))]
[RequireComponent (typeof (Energy))]
[RequireComponent (typeof (Level))]
[RequireComponent(typeof(Teleport))]
public class Suprime : BaseObject, IFightable, IEnemyChecker {
    [HeaderAttribute("Suprime Property")]
    public Crystall curentCrystall = null; // текущий кристалл, в радиусе которого находится ВС
    private Health health; // здоровье ВС
    private Energy energyComponent;// энергия ВС
    private Level level; // Уровень ВС
    private SuprimeMagic teleport; //Телепорт к ближайшему кристаллу

    public Energy EnergyComponent { get { return energyComponent; } set { energyComponent = value; } }

    void Start() {
        health = GetComponent<Health>();
        energyComponent = GetComponent<Energy>();
        level  = GetComponent<Level>();
        energyComponent.setEnergy(GameConf.suprimeMaxEnergy, GameConf.suprimeStartEnergy);
        health.setHealth(GameConf.suprimeMaxHealth, GameConf.suprimeStartHealth,
                         GameConf.suprimeBasicRegenSpeed, this);
        level.setup(GameConf.suprimeMaxLevel, GameConf.suprimeStartLevel);
        Radius.Initialization(GameConf.suprimeDetectRadius, this);

        //Инициализация магии
        teleport = GetComponent<Teleport>();
        teleport.setup(this,GameConf.TeleportCostEnergy,GameConf.TeleportCastTime);
    }

    void Update() {
        if(Input.GetKeyDown("space")) {
             teleport.cast();
           // transform.position = new Vector3(0, 0, 0);
        }
        
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
}
