using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic: MonoBehaviour {
    float castEnergy; // кол-во необходимой энергии для каста
    float durationTime; // кулдаун
    float currentDurationTime; //текущее значение времени кулдауна
    bool isAbleToCast = false; // может ли кастовать, проверяется с цикле

    public abstract void cast();
    public abstract void decast();

    // Инициализация
    public void setup(float castEnergy, float durationTime) {
        this.CastEnergy = castEnergy;
        this.durationTime = durationTime;
    }

    public float DurationTime { get { return durationTime; } set { durationTime = value; } }
    public float CastEnergy { get { return castEnergy; } set { castEnergy = value; } }
    public float CurrentDurationTime { get { return currentDurationTime; } set { currentDurationTime = value; } }
    public bool IsAbleToCast { get { return isAbleToCast; } set {isAbleToCast = value; } }
}
