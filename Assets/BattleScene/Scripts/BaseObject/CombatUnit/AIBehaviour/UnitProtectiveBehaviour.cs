using UnityEngine;

public class UnitProtectiveBehaviour : UnitAIBehaviour {

    #region constructors

    // Для задания защитного поведения необходим и юнит, и цель защиты
    public UnitProtectiveBehaviour(Unit subject, BaseObject protectTarget) : base(subject) {
        if (protectTarget == null) {
            throw new NoTargetToProtectException(); // Поведение падает если нет цели для защиты
        }

        ProtectTarget = protectTarget;   
    }
    #endregion

    #region properties

    private UnitState CurrentUnitState { get; set; } // текущее состояние юнита, перечисление в конце файла
    public BaseObject ProtectTarget { get; private set; } // цель защиты
    #endregion
    #region public methods

    public override void Start() {
        CurrentUnitState = UnitState.CALM; // Изначально юнит находится в спокойном 
        // т.к. на время защиты юнит следит только за целью защиты, но не за собой, мы меняем его радиус на радиус цели защиты
        Subject.DetectRadius = ProtectTarget.DetectRadius;
    }

    public override void UpdateState() {

        CombatSystem cs = Subject.CombatSys; // Используется для сокращения размера строк
        IFightable closestEnemy;

        switch (CurrentUnitState) {
            #region CALM STATE
            case UnitState.CALM:

                if (cs.IsUnderAttack) {
                    Attack();
                    // Для предотвращения зацикливания атак между двумя защищающими юнитами
                    cs.IsUnderAttack = false;
                    return;
                }

               closestEnemy  = GetClosestEnemyInRadius(); // берём ближайшего к цели юнита

                // если этот юнит есть и в агро радиусе, переходим в встревоженное состояние
                if (closestEnemy != null 
                    && Vector3.Distance(ProtectTarget.Position, closestEnemy.Position) < ProtectTarget.ReactDistance) {
                    cs.Target = closestEnemy;
                    CurrentUnitState = UnitState.ALARMED;
                    return;
                }

                // Если в агро радиусе никого нет и юнита никто не атакует - следуем за целью
                Follow(ProtectTarget);
                break;
            #endregion

            #region ALARMED STATE
            case UnitState.ALARMED:
                closestEnemy = GetClosestEnemyInRadius();

                if(closestEnemy == null) {
                    CurrentUnitState = UnitState.CALM;
                    return;
                }

                // если юнита никто не атакует, взять ближайшего к цели защиты врага
                if (!cs.IsUnderAttack) {
                    cs.Target = closestEnemy;
                }

                Attack();
                cs.IsUnderAttack = false; 

                break;
            #endregion

            default:
                throw new UndefinedDefensiveUnitStateException();
        }
    }

    public override void End() {
        // возвращаем радиус юнита на место
        Subject.DetectRadius = Subject.GetComponent<Radius>();
    }
    #endregion
}

public enum UnitState {
    CALM, // спокойное состояние
    ALARMED // встревоженное состояние
}
