﻿using UnityEngine;

public class UnitProtectiveBehaviour : UnitAIBehaviour {

    #region private fields
    // все поля завёрнуты в свойства, смотри регион геттеров и сеттеров

    private UnitState currentUnitState; // текущее состояние юнита, перечисление в конце файла
    private int agroRadius; // расстояние от ЦЕЛИ ЗАЩИТЫ, в котором враги будут атакованы //TODO может, стоит перенести в цель защиты
    private BaseObject protectTarget; // цель защиты
    #endregion

    #region constructors

    // Для задания защитного поведения необходим и юнит, и цель защиты
    public UnitProtectiveBehaviour(Unit subject, BaseObject protectTarget) : base(subject) {
        if (ProtectTarget == null) {
            throw new NoTargetToProtectException(); // Поведение падает если нет цели для защиты
        }

        ProtectTarget = protectTarget;
        CurrentUnitState = UnitState.CALM; // Изначально юнит находится в спокойном состоянии
    }
    #endregion

    #region getters and setters

    private UnitState CurrentUnitState {
        get { return currentUnitState; }

        set { currentUnitState = value; }
    }

    private BaseObject ProtectTarget {
        get { return protectTarget; }

        set { protectTarget = value; }
    }

    private int AgroRadius {
        get { return agroRadius; }

        set { agroRadius = value; }
    }
    #endregion

    public override void UpdateState() {
        CombatSystem cs = Subject.CombatSystem; // Используется для сокращения размера строк

        switch (CurrentUnitState) {
            #region CALM STATE
            case UnitState.CALM:

                if (cs.IsUnderAttack) {
                    if (cs.attack()) {
                        cs.Target.getCombatSystem().attacked(Subject);
                    }

                    // Для предотвращения зацикливания атак между двумя защищающими юнитами
                    cs.IsUnderAttack = false;
                    return;
                }

                if (ProtectTarget.RadiusStub.Length != 0) { // Если внутри радиуса цели кто-то есть
                    IFightable closestEnemy = ProtectTarget.getClosestUnitStub(); // берём ближайшего к цели юнита

                    // если этот юнит в агро радиусе, переходим в встревоженное состояние
                    if (Vector3.Distance(ProtectTarget.getPosition(), closestEnemy.getPosition()) < AgroRadius) {
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
                if (ProtectTarget.RadiusStub.Length == 0) {
                    CurrentUnitState = UnitState.CALM;
                    return;
                }

                // если юнита никто не атакует, взять ближайшего к цели защиты врага
                if (!cs.IsUnderAttack) {
                    cs.Target = protectTarget.getClosestUnitStub();
                }

                // подойти к цели, если не получилось атаковать
                if (cs.attack()) {
                    cs.Target.getCombatSystem().attacked(Subject);
                } else {
                    Subject.MovementAgent.moveTo(cs.Target.getPosition());
                }

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