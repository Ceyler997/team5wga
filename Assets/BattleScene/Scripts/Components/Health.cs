using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IPunObservable {

    #region private fields

    private float curHealth; //Текущее кол-во здоровья
    private float maxHealth; //Максимальное кол-во здоровья
    private float regenSpeed; //Скорость востановления здоровья
    private IDeathSubject subject; // Объект, за здоровье которого компоненто отвечает
    private bool isSettedUp;
    private bool isDead;
    #endregion

    #region getters and setters

    public float CurrentHealth {
        get { return curHealth; }
        private set { curHealth = value; }
    }

    public float MaxHealth {
        get { return maxHealth; }
        private set { maxHealth = value; }
    }

    public float RegenSpeed {
        get { return regenSpeed; }
        set { regenSpeed = value; }
    }

    private IDeathSubject Subject {
        get { return subject; }
        set { subject = value; }
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

        Regen();

        if (CurrentHealth <= 0 && !isDead)
            Die();
    }
    #endregion

    #region public methods

    // Функция для настройки системы после инициализации
    public void SetupSystem(float health, float maxHealth, float regenSpeed, IDeathSubject deathSubject) {
        CurrentHealth = health;
        MaxHealth = maxHealth;
        RegenSpeed = regenSpeed;
        Subject = deathSubject;
        isDead = false;
        IsSettedUp = true;
    }

    //Получение урона
    public void GetDamage(float damage) {
        if (CurrentHealth > 0) {
            CurrentHealth -= damage;
        }
    }
    #endregion

    #region private methods

    //Смерть юнита
    private void Die() {
        Subject.SubjectDeath();
        isDead = true;
    }

    //Востановление жизней (запускать в апдейте)
    private void Regen() {
        CurrentHealth += RegenSpeed * Time.deltaTime;
        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
    }
    #endregion

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(CurrentHealth);
        } else {
            CurrentHealth = (float) stream.ReceiveNext();
        }
    }
    #endregion
}