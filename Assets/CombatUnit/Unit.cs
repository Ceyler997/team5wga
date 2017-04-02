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

    public CombatSystem CombatSys {
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

    Vector3 IFightable.Position {
        get { return transform.position; }
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

        CombatSys = GetComponent<CombatSystem>();
        CombatSys.setupSystem(GameConf.unitDamage,
            GameConf.unitCritDamage,
            GameConf.unitBasicCritChance,
            GameConf.unitAttackRadius,
            GameConf.unitAttackSpeed);
	}

    new public void Update() { // Проверяем, есть ли мастер у юнита
        base.Update();
        if (Master == null) {
            throw new UnitHaveNoMasterException();
        }

        // Если поведения нет, устанавливаем защитное с защитой мастера
        if(Behaviour == null) {
            Behaviour = new UnitProtectiveBehaviour(this, master);
        }

        CombatSys.updateTarget(); // Обновляем цель

        Behaviour.UpdateState(); // Получаем команды от ИИ
    }
    #endregion
}
