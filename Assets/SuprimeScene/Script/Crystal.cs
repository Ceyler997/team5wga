using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Energy))]
[RequireComponent (typeof (Level))]
[RequireComponent (typeof (Player))]
public class Crystal : BaseObject, ILeveable {
    private float regenSpeed = 0; //Скорость восстановления энергии
    private Energy energy; //Текущее кол-во энергии
    private Level level; //Уровень кристалла

    public void Setup(Player ownerPlayer) {
        setPlayer(ownerPlayer);
        energy = GetComponent<Energy>();
        level = GetComponent<Level>();
        energy.setEnergy(GameConf.crysMaxEnergy, 0f);
        setupLevel();
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

    public override void ObjectEnter(BaseObject baseObject) {
        Debug.Log("Crystall has saw an baseObject!");
    }

    public override void ObjectExit(BaseObject baseObject) {
        Debug.Log("Crystall: object is exit from radius!");
    }
}
