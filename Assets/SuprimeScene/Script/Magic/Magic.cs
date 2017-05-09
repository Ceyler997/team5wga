using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic : MonoBehaviour {
    float castEnergy; // кол-во необходимой энергии для каста
    float durationTime; // кулдаун
    float currentDurationTime; //текущее значение времени кулдауна
    bool isAbleToCast = false; // может ли кастовать, проверяется с цикле
    // Судя по действию, переменная говорит, когда идёт каст, а не когда каст возможен. Переименуй, если так (IsCasting, например)

    public float DurationTime { get { return durationTime; } set { durationTime = value; } }
    public float CastEnergy { get { return castEnergy; } set { castEnergy = value; } }
    public float CurrentDurationTime { get { return currentDurationTime; } set { currentDurationTime = value; } }
    public bool IsAbleToCast { get { return isAbleToCast; } set { isAbleToCast = value; } }
    
    public abstract void cast(); //подготовка перед кастом, проверка всех условий

    // итоговый результат выполнения магии, для каждой магии своя реализация
    protected abstract void CastMagic();

    // условия, которые должны выполнятся при произнесении заклинания
    protected abstract bool CastCondition();

    // Инициализация
    public void setup(float castEnergy, float durationTime) {
        CastEnergy = castEnergy;
        DurationTime = durationTime;
    }

    void Update() {
        if (IsAbleToCast) {
            castDelay();
        }
    }

    // Задержка перед выполнением заклинания
    void castDelay() {
        if (CastCondition())
            decast();

        CurrentDurationTime -= 1.0f * Time.deltaTime; // зачем умножать?
        if (CurrentDurationTime <= 0) {
            IsAbleToCast = false;
            CastMagic();
        }

        Debug.Log(CurrentDurationTime);
    }

    virtual protected void decast() {
        IsAbleToCast = false;
    }
}