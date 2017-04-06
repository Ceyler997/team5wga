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
    private GameManager manager; //Игровой менеджер
    
    public void Initialization (GameManager manager) {
        this.manager = manager;
        energy = GetComponent<Energy>();
        level = GetComponent<Level>();
        setEnergy();
        setupLevel();
        setPlayer(null);
        setRadius(manager.CrystallGetSecondRadius);
    }
	 void setEnergy() {
        energy.setEnergy(manager.MaxSuprimeEnergy,0f);
    }
	void Update () {
        //Если кто-нибудь владеет кристалом то вырабатываем энергию
        if(ControllPlayer != null && energy != null)
            energy.changeEnergy(regenSpeed * Time.deltaTime);
    }
    void setupLevel() {
        level.setup(manager.CrytallMaxLevel, manager.CrytallStartLevel);
        regenSpeed = manager.GetCrystallRegenEnergySpeed(level.curentLevel);
    }
    void ILeveable.levelUp() {
        level.levelUp();
        regenSpeed = manager.GetCrystallRegenEnergySpeed(level.curentLevel);
    }
    public override void setRadius(float size) {
        Radius.Initialization(size, this);
    }
    
    public override void EnemyCheck(BaseObject enemy) {
        Debug.Log("Crystall has saw an enemy!");
    }
    
}
