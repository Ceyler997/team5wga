using System;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour, IPunObservable {

    #region private fields

    public float currentEnergy; //Текущее кол-во энергии
    private float maxEnergy; //Максимальное кол-во энергии
    private bool isSettedUp;
    #endregion

    #region UI fields

    public Text energyOut;
    #endregion

    #region getters and setters

    public float CurrentEnergy {
        get { return currentEnergy; }
        private set { currentEnergy = value; }
    }

    public float MaxEnergy {
        get { return maxEnergy; }
        private set { maxEnergy = value; }
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

        if(energyOut != null) {
            energyOut.text = Math.Round(CurrentEnergy, 2).ToString() + "/" + Math.Round(MaxEnergy, 2).ToString();
        }
    }
    #endregion

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(CurrentEnergy);
        } else {
            CurrentEnergy = (float) stream.ReceiveNext();
        }
    }
    #endregion
}
