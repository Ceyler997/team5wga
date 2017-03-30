using UnityEngine;
public class Health : MonoBehaviour {

    public float health; //Текущее кол-во здоровья
    public float maxHealth; //Максимальное кол-во здоровья
    private float regenSpeed; //Скорость востановления здоровья
    private bool isDead = false;

    public bool IsDead {
        get { return isDead; }

        set { isDead = value; }
    }

    public void setHealth(float health, float maxHealth, float regenSpeed) {
        this.health = health;
        this.maxHealth = maxHealth;
        this.regenSpeed = regenSpeed;
    }

    //Получение урона
    public void getDamage(float damage) {
        health -= damage;
        if (health <= 0)
            die();
    }
    //Смерть юнита
    void die() {
        IsDead = true;
        Destroy(gameObject);
    }
    //Востановление жизней (запускать в апдейте)
    void regen() {
        health += regenSpeed * Time.deltaTime;
        health = Mathf.Min(health, maxHealth);
    }
    //Изменение значения скорости регенерации
    void setRegenSpeed(float regenSpeed) {
        this.regenSpeed = regenSpeed;
    }

    public void Update() {
        regen();
    }
}