using System;
using UnityEngine;

public interface ILeveable {
    void levelUp();
}

//Скрипт реализует прокачку уровня для ВС, Магии и Кристалла
public class Level : MonoBehaviour, IPunObservable {

    #region private fields

    private int currentLevel; //Текущий уровень
    private int maxLevel; //Максимальный уровень
    private bool isSettedUp;
    #endregion

    #region getters and setters

    public int CurrentLevel {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    public int MaxLevel {
        get { return maxLevel; }
        set { maxLevel = value; }
    }

    public bool IsSettedUp {
        get { return isSettedUp; }
        set { isSettedUp = value; }
    }
    #endregion

    #region public methods

    public void levelUp(){
        if (CurrentLevel < MaxLevel)
            CurrentLevel += 1;
    }

    public void setupSystem(int startLevel, int maxLevel) {
        CurrentLevel = startLevel;
        MaxLevel = maxLevel;
        IsSettedUp = true;
    }
    #endregion

    #region MonoBehaviour methods
    public void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }
    #endregion

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(CurrentLevel);
        } else {
            CurrentLevel = (int) stream.ReceiveNext();
        }
    }
    #endregion
}
