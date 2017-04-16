using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic: MonoBehaviour {
    float castEnergy; // кол-во необходимой энергии для каста
    float durationTime; // кулдаун
    float currentDurationTime; //текущее значение времени кулдауна
    bool isAbleToCast = false; // может ли кастовать, проверяется с цикле

    public abstract void cast(); //подготовка перед кастом, проверка всех условий

    // Инициализация
    public void setup(float castEnergy, float durationTime) {
        this.CastEnergy = castEnergy;
        this.durationTime = durationTime;
    }

    void Update() {
        if (IsAbleToCast) {
            castDelay();
        }
    }

    // итоговый результат выполнения магии, для каждой магии своя реализация
    protected abstract void CastMagic();

    //Задержка перед кастом
    void castDelay() {
        CurrentDurationTime -= 1.0f * Time.deltaTime;
        Debug.Log(CurrentDurationTime);
        if (CurrentDurationTime <= 0) {
            IsAbleToCast = false;
            CastMagic();
        }
    }

    protected void decast() {
        IsAbleToCast = false;
    }


    public float DurationTime { get { return durationTime; } set { durationTime = value; } }
    public float CastEnergy { get { return castEnergy; } set { castEnergy = value; } }
    public float CurrentDurationTime { get { return currentDurationTime; } set { currentDurationTime = value; } }
    public bool IsAbleToCast { get { return isAbleToCast; } set {isAbleToCast = value; } }
}
