using UnityEngine;

public class CombatSystem : MonoBehaviour {

    #region private variables
    // все поля в свойствах (см. геттеры и сеттеры)

    private float damage; // базовый урон юнита за удар
    private float critDamage; // дополнительный урон от крита

    private float critChance; // базовая вероятность крита, от 0.0 до 1.0
    private BattleMagicColor currentMagicColor; // текущий цвет магии, наложенной на юнита

    private float attackRadius; // радиус, в котором юнит атакует

    private float attackSpeed; // время отката атаки
    private float nextAttackTime; // минимальное время начала следующей атаки

    private IFightable target; // цель атаки
    private bool isUnderAttack; // атакуют ли цель

    private bool isSettedUp;
    #endregion

    #region getters and setters

    public float Damage {
        get { return damage; }

        set { damage = value; }
    }

    public float CritDamage {
        get { return critDamage; }

        set { critDamage = value; }
    }

    public float CritChance {
        get { return critChance; }

        set { critChance = Mathf.Clamp(value, 0.0f, 1.0f); }
    }

    public BattleMagicColor CurrentMagicColor {
        get { return currentMagicColor; }

        set { currentMagicColor = value; }
    }

    public float AttackRadius {
        get { return attackRadius; }

        set { attackRadius = value; }
    }

    public float AttackSpeed {
        get { return attackSpeed; }

        set { attackSpeed = value; }
    }

    private float NextAttackTime {
        get { return nextAttackTime; }

        set { nextAttackTime = value; }
    }

    public IFightable Target {
        get { return target; }

        set { target = value; }
    }

    public bool IsUnderAttack {
        get { return isUnderAttack; }

        set { isUnderAttack = value; }
    }

    public bool IsSettedUp {
        get { return isSettedUp; }

        set { isSettedUp = value; }
    }
    #endregion

    #region private methods

    // рассчитывает добавочный крит в текущий момент
    private float getCrit() {
        float curCritChance = CritChance;

        if (CurrentMagicColor == BattleMagicColor.NO_COLOR) {
            return 0;
        }

        if (CurrentMagicColor.CounterMagic == Target.UnitCombatSystem.CurrentMagicColor) {
            curCritChance -= Target.UnitCombatSystem.CritChance;
        }

        float dice = Random.Range(0.0f, 1.0f);

        if (dice < curCritChance) {
            return CritDamage;
        } else {
            return 0;
        }
    }
    #endregion

    #region MonoBehaviour methods

    public void Start() {
        CurrentMagicColor = BattleMagicColor.NO_COLOR;
        NextAttackTime = Time.time;
        IsUnderAttack = false;
    }

    private void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }
    #endregion

    #region public methods

    // Функция для настройки системы после инициализации
    public void setupSystem(float basicDmg, float critDmg, float critChance, float atkRadius, float atkSpeed) {
        isSettedUp = true;
        Damage = basicDmg;
        CritDamage = critDmg;
        CritChance = critChance;
        AttackRadius = atkRadius;
        AttackSpeed = atkSpeed;
    }

    // Проверяет текущую цель и обнуляет её, если она мертва. Вызывать в цикле перед атакой
    public void updateTarget() { 
        if(Target != null && Target.UnitHealthSystem.IsDead) {
            Target = null;
            isUnderAttack = false; // Если атаковала не цель, поле будет вскоре установленно обратно
        }
    }

    // Если юнита атакуют, то его цель меняется на атакующего
    public void attacked(IFightable attacker) {
        IsUnderAttack = true;
        Target = attacker;
    }

    // возвращает true, если цель МОЖЕТ атаковать текущую цель (т.е. не учитывая откат атаки)
    public bool attack() {
        if(Vector3.Distance(transform.position, Target.Position) < AttackRadius) { // проверка расстояния

            if(Time.time >= NextAttackTime) { // проверка отката
                float damageToTarger = Damage + getCrit(); // получение наносимого урона

                Target.UnitHealthSystem.getDamage(damageToTarger); // наносится урон цели

                NextAttackTime = Time.time + AttackSpeed; // устанавливается откат
            }
            return true;
        }        
        return false;
    }
    
    // получение уведомление о доступной цели. Если цели нет, то устанавливается полученная
    public void getTargetNotification(IFightable target) {
        if(Target == null) {
            Target = target;
        }
    }
    #endregion
}

public interface IFightable {
    CombatSystem UnitCombatSystem { get; }
    Vector3 Position { get; }
    Health UnitHealthSystem { get; }
}
