using UnityEngine;

public interface ILeveable {
    void LevelUp();
}

//Скрипт реализует прокачку уровня для ВС, Магии и Кристалла
public class Level : MonoBehaviour, IPunObservable {

    #region getters and setters

    public int CurrentLevel { get; private set; } // Текущий уровень

    public int MaxLevel { get; private set; } // Максимальный уровень

    private bool IsSettedUp { get; set; }
    #endregion

    #region public methods

    public void LevelUp() {
        if (CurrentLevel < MaxLevel)
            CurrentLevel += 1;
    }

    public void SetupSystem(int startLevel, int maxLevel) {
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
