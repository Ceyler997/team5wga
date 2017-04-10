using UnityEngine;

public class Energy : MonoBehaviour {

    #region private fields

    private float currentEnergy; //Текущее кол-во энергии
    private float maxEnergy; //Максимальное кол-во энергии
    private bool isSettedUp;
    #endregion

    #region getters and setters

    public float CurrentEnergy {
        get { return currentEnergy; }

        set { currentEnergy = value; }
    }

    private float MaxEnergy {
        get { return maxEnergy; }

        set { maxEnergy = value; }
    }

    private bool IsSettedUp {
        get { return isSettedUp; }

        set { isSettedUp = value; }
    }
    #endregion

    #region public methods

    public void changeEnergy(float deltaEnergy) {
        CurrentEnergy += deltaEnergy;
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
    }

    public void setupSystem(float curentEnergy, float maxEnergy) {
        IsSettedUp = true;
        CurrentEnergy = curentEnergy;
        MaxEnergy = maxEnergy;
    }
    #endregion

    #region MonoBehaviour methods
    private void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }
    #endregion
}
