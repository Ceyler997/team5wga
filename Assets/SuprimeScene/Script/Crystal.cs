using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Energy))]
[RequireComponent (typeof (Level))]
[RequireComponent (typeof (Player))]
[RequireComponent (typeof (Radius))]
public class Crystal : BaseObject, ILeveable {
    private float regenSpeed = 0; //Скорость восстановления энергии
    private Energy energy; //Текущее кол-во энергии
    private Level level; //Уровень кристалла

    public void Setup(Player ownerPlayer) {
        energy = GetComponent<Energy>();
        level = GetComponent<Level>();
        energy.setEnergy(GameConf.crysMaxEnergy, 0f);
        setupLevel();
        setPlayer(ownerPlayer);
        Radius.Initialization(GameConf.crysDetectRadius, this);
    }
	
	void Update () {
        //Если кто-нибудь владеет кристалом то вырабатываем энергию
        if(ControllPlayer != null && energy != null)
            energy.changeEnergy(regenSpeed * Time.deltaTime);
    }

    void setupLevel() {
        level.setup(GameConf.crysMaxLevel, GameConf.crysStartLevel);
        regenSpeed = GameConf.getCrysRegenSpeed(level.curentLevel);
    }

    void ILeveable.levelUp() {
        level.levelUp();
        regenSpeed = GameConf.getCrysRegenSpeed(level.curentLevel);
    }
    
    public override void EnemyCheck(BaseObject enemy) {
        Debug.Log("Crystall has saw an enemy!");
    }
    
}
