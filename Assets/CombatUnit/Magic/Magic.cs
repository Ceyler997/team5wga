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
