using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Energy))]
[RequireComponent (typeof (Level))]
[RequireComponent (typeof (Player))]
[RequireComponent (typeof (Radius))]
public class Crystal : BaseObject, ILeveable {
    public Player controllPlayer;//Игрок, который в данный момент контроллирует кристалл
    private float regenSpeed = 0; //Скорость восстановления энергии
    private Energy energy; //Текущее кол-во энергии
    private Level level; //Уровень кристалла
    private GameManager manager; //Игровой менеджер
    private Radius radius; //Дальний радису кристалла, отслеживает всех кто в него заходит

    public Radius Radius
    {
        get
        {
            return radius;
        }

        set
        {
            radius = value;
        }
    }

    public void Initialization (GameManager manager) {
        this.manager = manager;
        energy = GetComponent<Energy>();
        level = GetComponent<Level>();
        Radius = GetComponent<Radius>();
        setEnergy();
        setupLevel();
        setupRadius();
    }
	 void setEnergy() {
        energy.setEnergy(manager.MaxSuprimeEnergy,0f);
    }
	public void setPlayer(Player player) {
        controllPlayer = player;
    } 
	void Update () {
        //Если кто-нибудь владеет кристалом то вырабатываем энергию
        if(controllPlayer != null)
            energy.changeEnergy(regenSpeed * Time.deltaTime);
    }
    void setupLevel() {
        level.setup(manager.CrytallMaxLevel, manager.CrytallStartLevel);
        regenSpeed = manager.GetCrystallRegenEnergySpeed(level.curentLevel);
    }
    void setupRadius() {
        Radius.Initialization(manager.CrystallGetSecondRadius);
    }
    void ILeveable.levelUp() {
        level.levelUp();
        regenSpeed = manager.GetCrystallRegenEnergySpeed(level.curentLevel);
    }
}
