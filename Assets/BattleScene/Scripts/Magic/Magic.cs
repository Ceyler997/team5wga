using UnityEngine;

public abstract class Magic: MonoBehaviour {
    float castEnergy; // кол-во необходимой энергии для каста
    float durationTime; // кулдаун
    float currentDurationTime; //текущее значение времени кулдауна
    bool isAbleToCast = false; // может ли кастовать, проверяется с цикле
                                // может, стоит изменять по какому-то коллбеку?

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

public class BattleMagicColor { // TODO move to group magic
    public static readonly BattleMagicColor NO_COLOR = new BattleMagicColor(NO_COLOR);
    public static readonly BattleMagicColor WHITE = new BattleMagicColor(BLACK);
    public static readonly BattleMagicColor RED = new BattleMagicColor(WHITE);
    public static readonly BattleMagicColor BLACK = new BattleMagicColor(RED);

    private BattleMagicColor counterMagic;

    public BattleMagicColor CounterMagic {
        get {
            return counterMagic;
        }
    }

    private BattleMagicColor(BattleMagicColor counterMagic) {
        this.counterMagic = counterMagic;
    }
}