using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic: MonoBehaviour {
    float castEnergy; // кол-во необходимой энергии для каста
    float durationTime; // кулдаун
    float currentDurationTime; //текущее значение времени кулдауна
    bool isAbleToCast = false; // может ли кастовать, проверяется с цикле

    public float DurationTime { get { return durationTime; } set { durationTime = value; } }
    public float CastEnergy { get { return castEnergy; } set { castEnergy = value; } }
    public float CurrentDurationTime { get { return currentDurationTime; } set { currentDurationTime = value; } }
    public bool IsAbleToCast { get { return isAbleToCast; } set { isAbleToCast = value; } }

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

}

public class BattleMagicColor { // TODO move to group magic
    public static readonly BattleMagicColor NO_COLOR = new BattleMagicColor(NO_COLOR, 0);
    public static readonly BattleMagicColor WHITE = new BattleMagicColor(BLACK, 1);
    public static readonly BattleMagicColor RED = new BattleMagicColor(WHITE, 2);
    public static readonly BattleMagicColor BLACK = new BattleMagicColor(RED, 3);
    
    public BattleMagicColor CounterMagic {
        get;
        private set;
    }

    public int MagicID {
        get;
        private set;
    }

    private BattleMagicColor(BattleMagicColor counterMagic, int ID) {
        CounterMagic = counterMagic;
        MagicID = ID;
    }

    public static BattleMagicColor getMagicByID(int ID) {
        if(ID == NO_COLOR.MagicID) {
            return NO_COLOR;
        } else if (ID == WHITE.MagicID) {
            return WHITE;
        } else if(ID== RED.MagicID) {
            return RED;
        } else if (ID == BLACK.MagicID) {
            return BLACK;
        } else {
            throw new UnknownMagicException();
        }
    }
}
