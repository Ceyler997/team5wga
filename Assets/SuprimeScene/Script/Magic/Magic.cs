using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic : MonoBehaviour {

    public Suprime Caster { get; private set; }
    public float CastEnergy { get; private set; }
    public float CastTime { get; private set; }
    public float CurrentCastTime { get; private set; }
    public bool IsCasting { get; private set; }

    private bool IsSettedUp { get; set; }

    // условия, которые должны выполнятся при произнесении заклинания
    // true если залинание может быть применено
    protected abstract bool IsAbleToCast();

    // Инициализация
    virtual protected void Setup(Suprime caster, float castEnergy, float castTime) {
        Caster = caster;
        CastEnergy = castEnergy;
        CastTime = castTime;
        IsSettedUp = true;
    }

    void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }

        if (IsCasting) {
            CastDelay();
        }
    }

    //подготовка перед кастом, проверка всех условий, расширяется в потомках, общие действия в базе
    public virtual void TryCast() {
        //Остановка мага
        Caster.MoveSystem.Stop();
        //Установка начальныйх значений времени каста
        CurrentCastTime = CastTime;
        //Запуск таймера
        IsCasting = true;
    }

    // Задержка перед выполнением заклинания
    void CastDelay() {
        if (!IsAbleToCast())
            CancelCast();

        CurrentCastTime -= Time.deltaTime;
        if (CurrentCastTime <= 0) {
            ApplyMagic();
        }

        Debug.Log(CurrentCastTime);
    }

    // итоговый результат выполнения магии, для каждой магии своя реализация, общие действия в базе
    protected virtual void ApplyMagic() {
        IsCasting = false;
        Caster.EnergySystem.changeEnergy(-CastEnergy);
    }

    virtual protected void CancelCast() {
        IsCasting = false;
    }
}