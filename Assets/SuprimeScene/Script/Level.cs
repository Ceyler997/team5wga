using UnityEngine;

public interface ILeveable {
    void levelUp();
}

//Скрипт реализует прокачку уровня для ВС, Магии и Кристалла
public class Level : MonoBehaviour {

    #region private fields

    private int curentLevel; //Текущий уровень
    private int maxLevel; //Максимальный уровень
    private bool isSettedUp;
    #endregion

    #region getters and setters

    public int CurentLevel {
        get { return curentLevel; }

        set { curentLevel = value; }
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
        if (CurentLevel < MaxLevel)
            CurentLevel += 1;
    }

    public void setupSystem(int startLevel, int maxLevel) {
        CurentLevel = startLevel;
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
}
