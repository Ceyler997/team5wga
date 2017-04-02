using System;
using UnityEngine;
public class Health : MonoBehaviour {

    #region private fields

    private float curHealth; //Текущее кол-во здоровья
    private float maxHealth; //Максимальное кол-во здоровья
    private float regenSpeed; //Скорость востановления здоровья
    private bool isDead;
    private bool isSettedUp;
    #endregion

    #region getters and setters

    private float CurrentHealth {
        get { return curHealth; }

        set { curHealth = value; }
    }

    private float MaxHealth {
        get { return maxHealth; }

        set { maxHealth = value; }
    }

    public float RegenSpeed {
        get { return regenSpeed; }

        set { regenSpeed = value; }
    }

    public bool IsDead {
        get { return isDead; }

        set { isDead = value; }
    }

    private bool IsSettedUp {
        get { return isSettedUp; }

        set { isSettedUp = value; }
    }
    #endregion

    #region MonoBehaviour methods

    public void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }

        regen();
    }
    #endregion

    #region public methods

    // Функция для настройки системы после инициализации
    public void setupSystem(float health, float maxHealth, float regenSpeed) {
        CurrentHealth = health;
        MaxHealth = maxHealth;
        RegenSpeed = regenSpeed;
        IsSettedUp = true;
    }

    //Получение урона
    public void getDamage(float damage) {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
            die();
    }
    #endregion

    #region private methods

    //Смерть юнита
    private void die() {
        IsDead = true;
        Destroy(gameObject);
    }

    //Востановление жизней (запускать в апдейте)
    private void regen() {
        CurrentHealth += RegenSpeed * Time.deltaTime;
        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
    }
    #endregion
}