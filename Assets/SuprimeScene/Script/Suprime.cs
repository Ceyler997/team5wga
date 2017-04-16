using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(Level))]
[RequireComponent(typeof(Teleport))]
[RequireComponent(typeof(CaptureCrystall))]
public class Suprime : BaseObject, IFightable, IEnemyChecker {
    [HeaderAttribute("Suprime Property")]
    public Crystal curentCrystall = null; // текущий кристалл, в радиусе которого находится ВС
    private Health health; // здоровье ВС
    private Energy energyComponent;// энергия ВС
    private Level level; // Уровень ВС
    private SuprimeMagic teleport; //Телепорт к ближайшему кристаллу
    private SuprimeMagic captureCrystall; //захват кристалла

    public Energy EnergyComponent { get { return energyComponent; } set { energyComponent = value; } }

    void Start() {
        health = GetComponent<Health>();
        energyComponent = GetComponent<Energy>();
        level = GetComponent<Level>();
        energyComponent.setEnergy(GameConf.suprimeMaxEnergy, GameConf.suprimeStartEnergy);
        health.setHealth(GameConf.suprimeMaxHealth, GameConf.suprimeStartHealth,
                         GameConf.suprimeBasicRegenSpeed, this);
        level.setup(GameConf.suprimeMaxLevel, GameConf.suprimeStartLevel);
        Radius.Initialization(GameConf.suprimeDetectRadius, this);

        //Инициализация магии
        teleport = GetComponent<Teleport>();
        teleport.setup(this, GameConf.TeleportCostEnergy, GameConf.TeleportCastTime);
        captureCrystall = GetComponent<CaptureCrystall>();
        captureCrystall.setup(this, GameConf.CrystallCaptureCostEnergy, GameConf.TeleportCastTime);
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            captureCrystall.cast();
            // transform.position = new Vector3(0, 0, 0);
        }
    }

    public Crystal NearCrystall { get { return curentCrystall; } set { curentCrystall = value; } }

    void IFightable.die() {
        Destroy(gameObject);
    }

    public override void ObjectEnter(BaseObject baseObject) {
        //Если кристалл, то добавляем в текущий
        if (baseObject is Crystal) {
            Crystal crystall = (Crystal)baseObject;
            if (crystall != null)
                curentCrystall = crystall;
        }

        if (baseObject is Suprime) {
            Debug.Log("i saw an Suprime");
        }
    }

    public override void ObjectExit(BaseObject baseObject) {
        //Если кристалл, то добавляем в текущий
        if (baseObject is Crystal) {
            Crystal crystall = (Crystal)baseObject;
            if (crystall == curentCrystall)
                curentCrystall = null;
        }

        if (baseObject is Suprime) {
            Debug.Log("i saw an Suprime");
        }
    }
}
