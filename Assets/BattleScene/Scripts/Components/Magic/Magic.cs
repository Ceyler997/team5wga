using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic : MonoBehaviour {

    private bool isCasting; // локальный флаг каста

    public Suprime Caster { get; private set; }
    public float EnergyCost { get; set; }
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

    abstract public void Setup(Suprime caster);

    private void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }

        if (IsCasting) {
            CastDelay();
        }
    }

    //подготовка перед кастом, проверка всех условий, расширяется в потомках, общие действия в базе
    virtual public void TryCast() {
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
        if (!IsAbleToCast()) {
            CancelCast();
            return;
        }

        CurrentCastTime -= Time.deltaTime;
        if (CurrentCastTime <= 0) {
            ApplyMagic();
        }

        Debug.Log(Math.Round(CurrentCastTime, 2));
    }

    // итоговый результат выполнения магии, для каждой магии своя реализация, общие действия в базе
    virtual protected void ApplyMagic() {
        IsCasting = false;
    }

    virtual protected void CancelCast() {
        IsCasting = false;
    }
}
