using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour {
    IFightable master; //Класс который реализует переменную Health
    public float health; //Текущее кол-во здоровья
    public float maxHealth; //Максимальное кол-во здоровья
    private float regenSpeed; //Скорость востановления здоровья
    public void setHealth(float health, float maxHealth, float regenSpeed, IFightable himself) {
        this.health = health;
        this.maxHealth = maxHealth;
        this.regenSpeed = regenSpeed;
        this.master = himself;
    }
    
    //Получение урона
    public void takeDamage(float damage) {
        health -= damage;
        if(health <= 0) die();
    }
    
    //Смерть юнита
    void die() {
        master.die();
    }
    
    //Востановление жизней (запускать в апдейте)
    void regen(float deltaTime) {
        health += regenSpeed * Time.deltaTime;
    }

    //Изменение значения скорости регенерации
    void setRegenSpeed(float regenSpeed) {
        this.regenSpeed = regenSpeed;
    }
}