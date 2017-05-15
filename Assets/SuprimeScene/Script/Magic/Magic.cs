﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic : MonoBehaviour {

    private bool isCasting; // локальный флаг каста

    public Suprime Caster { get; private set; }
    public float EnergyCost { get; private set; }
    public float CastTime { get; private set; }
    public float CurrentCastTime { get; private set; }
    private bool IsCasting {
        get { return isCasting; }
        set {
            isCasting = value;
            Caster.Magic.IsCasting = value;
        }
    }

    private bool IsSettedUp { get; set; }

    // условия, которые должны выполнятся при произнесении заклинания
    // true если залинание может быть применено
    abstract protected bool IsAbleToCast();

    // Инициализация
    virtual protected void Setup(Suprime caster, float energyCost, float castTime) {
        Caster = caster;
        EnergyCost = energyCost;
        CastTime = castTime;
        IsSettedUp = true;
    }

    private void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }

        if (IsCasting) {
            CastDelay();
        }
    }

    //подготовка перед кастом, проверка всех условий, расширяется в потомках, общие действия в базе
    virtual protected void TryCast() {
        //Остановка мага
        Caster.MoveSystem.Stop();
    }

    virtual protected void StartCasting() {
        //Установка начальныйх значений времени каста
        CurrentCastTime = CastTime;
        //Запуск таймера
        IsCasting = true;
    }

    // Задержка перед выполнением заклинания
    private void CastDelay() {
        if (!IsAbleToCast())
            CancelCast();

        CurrentCastTime -= Time.deltaTime;
        if (CurrentCastTime <= 0) {
            ApplyMagic();
        }

        Debug.Log(CurrentCastTime);
    }

    // итоговый результат выполнения магии, для каждой магии своя реализация, общие действия в базе
    virtual protected void ApplyMagic() {
        Caster.EnergySystem.changeEnergy(-EnergyCost);
        IsCasting = false;
    }

    virtual protected void CancelCast() {
        IsCasting = false;
    }
}
