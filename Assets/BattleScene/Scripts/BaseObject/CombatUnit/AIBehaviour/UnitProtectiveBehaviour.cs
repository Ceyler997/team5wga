﻿using UnityEngine;

public class UnitProtectiveBehaviour : UnitAIBehaviour {

    #region constructors

    // Для задания защитного поведения необходим и юнит, и цель защиты
    public UnitProtectiveBehaviour(Unit subject, BaseObject protectTarget) : base(subject) {
        if (protectTarget == null) {
            throw new NoTargetToProtectException(); // Поведение падает если нет цели для защиты
        }

        ProtectTarget = protectTarget;
        CurrentUnitState = UnitState.CALM; // Изначально юнит находится в спокойном состоянии
        
    }
    #endregion

    #region properties

    private UnitState CurrentUnitState { get; set; } // текущее состояние юнита, перечисление в конце файла
    private BaseObject ProtectTarget { get; set; } // цель защиты
    #endregion

    public override void UpdateState() {

        CombatSystem cs = Subject.CombatSys; // Используется для сокращения размера строк

        switch (CurrentUnitState) {
            #region CALM STATE
            case UnitState.CALM:

                if (cs.IsUnderAttack) {
                    attack();
                    // Для предотвращения зацикливания атак между двумя защищающими юнитами
                    cs.IsUnderAttack = false;
                    return;
                }

                if (TargetRadius.isEnemyInside()) { // Если внутри радиуса цели кто-то есть
                    IFightable closestEnemy = TargetRadius.getClosestEnemy(); // берём ближайшего к цели юнита

                    // если этот юнит в агро радиусе, переходим в встревоженное состояние
                    if (Vector3.Distance(ProtectTarget.Position, closestEnemy.Position) < ProtectTarget.ReactDistance) {
                        cs.Target = closestEnemy;
                        CurrentUnitState = UnitState.ALARMED;
                        return;
                    }
                }

                // Если в агро радиусе никого нет и юнита никто не атакует - следуем за целью
                Subject.MovementAgent.follow(ProtectTarget);
                break;
            #endregion

            #region ALARMED STATE
            case UnitState.ALARMED:

                // если внутри радиуса цели никого нет, переходим в спокойное состояние
                if (!TargetRadius.isEnemyInside()) {
                    CurrentUnitState = UnitState.CALM;
                    return;
                }

                // если юнита никто не атакует, взять ближайшего к цели защиты врага
                if (!cs.IsUnderAttack) {
                    cs.Target = TargetRadius.getClosestEnemy();
                }

                attack();

                break;
            #endregion

            default:
                throw new UndefinedDefensiveUnitStateException();
        }
    }
}

public enum UnitState {
    CALM, // спокойное состояние
    ALARMED // встревоженное состояние
}
