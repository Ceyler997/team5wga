using System;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CombatSystem))]
public class Unit : BaseObject, IFightable {

    #region private fields

    private Movement movementAgent; // компонент передвижения
    private Health healthSystem; // компонент здоровья
    private CombatSystem combatSystem; // компонент системы боёвки
    private UnitAIBehaviour behaviour; // Компонент поведения
    private Suprime master; // Призвавший юнита ВС
    #endregion

    #region getters and setters

    public Movement MovementAgent {
        get {return movementAgent;}

        set {movementAgent = value;}
    }

    public Health HealthSystem {
        get {return healthSystem;}

        set {healthSystem = value;}
    }

    public CombatSystem CombatSystem {
        get { return combatSystem; }

        set { combatSystem = value; }
    }

    public UnitAIBehaviour Behaviour {
        get {return behaviour;}

        set {behaviour = value;}
    }

    public Suprime Master {
        get {return master;}

        set {master = value;}
    }
    #endregion

    #region MonoBehaviour methods

    // ВНИМАНИЕ мастер должен быть установлен сразу после воплощения
    void Start () {
        MovementAgent = GetComponent<Movement>();

        HealthSystem = GetComponent<Health>();
        HealthSystem.setupSystem(GameConf.unitStartHealth,
            GameConf.unitMaxHealth,
            GameConf.unitBasicRegenSpeed);

        CombatSystem = GetComponent<CombatSystem>();
        CombatSystem.setupSystem(GameConf.unitDamage,
            GameConf.unitCritDamage,
            GameConf.unitBasicCritChance,
            GameConf.unitAttackRadius,
            GameConf.unitAttackSpeed);
	}

    public void Update() {
        // Проверяем, есть ли мастер у юнита
        if (Master == null) {
            throw new UnitHaveNoMasterException();
        }

        // Если поведения нет, устанавливаем защитное с защитой мастера
        if(Behaviour == null) {
            Behaviour = new UnitProtectiveBehaviour(this, master);
        }

        CombatSystem.updateTarget(); // Обновляем цель

        Behaviour.UpdateState(); // Получаем команды от ИИ
    }
    #endregion

    #region IFightable implementation

    public CombatSystem getCombatSystem() {
        return CombatSystem;
    }

    public Health getHealthSystem() {
        return HealthSystem;
    }
    #endregion
}
