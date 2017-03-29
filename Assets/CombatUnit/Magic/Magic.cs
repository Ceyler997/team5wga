using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic : MonoBehaviour {

    #region private fields

    float energyToCast;
    float castingTime;
    float durationTime;
    float currentDurationTime;
    bool isAbleToCast;
    #endregion

    #region public methods

    public abstract void cast();
    public abstract void decast();
    #endregion
}

public class BattleMagicColor { // TODO move to suprime magic
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
