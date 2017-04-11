using System;
using System.Collections.Generic;
using UnityEngine;

//Класс с глобальными переменными и основными настройками игры
// Singletone
public class GameManager : MonoBehaviour, IUpdateObject {

    #region private fields

    private static GameManager instance;
    #endregion

    #region properties

    public static GameManager Instance {
        get {return instance;}
        set {
            if(instance == null) {
                instance = value;
            } else {
                throw new AttemptToManagerReassignmentException();
            }
        }
    }

    private List<IUpdateObserver> UpdateObsevers { get; set; }
    #endregion

    #region public fields

    [Tooltip("Все игроки подключенные к игре")]
    public List<Player> players; //Все игроки подключенные к игре
    [Tooltip("Все кристаллы на карте")]
    public Crystal[] crystals; //Все кристаллы на карте (при добавлении нового кристалла, обязательно добавить его сюда)
    #endregion

    #region public methods

    public Player GetPlayer(int id) {
        if(id >= 0 && id < players.Count) {
            return players[id];
        } else {
            throw new PlayerNotExistingException();
        }
    }
    #endregion

    #region MonoBehaviour methods

    public void Start() {
        Instance = this; // singletone
        UpdateObsevers = new List<IUpdateObserver>();

        foreach(Crystal crystal in crystals) {
            crystal.setupCrystal(null);
        }

        //DEBUG
        players[0].addCrystall(crystals[0]);
        //players[0].addCrystall(crystalls[1]);
        //players[0].addCrystall(crystalls[2]);
        //DEBUG
    }

    public void Update() {
        foreach(IUpdateObserver observer in UpdateObsevers) {
            observer.OnUpdate();
        }
    }

    public void LateUpdate() {
        foreach (IUpdateObserver observer in UpdateObsevers) {
            observer.OnLateUpdate();
        }
    }
    #endregion

    #region IUpdateObject implementation

    public void Attach(IUpdateObserver observer) {
        UpdateObsevers.Add(observer);
    }

    public void Detach(IUpdateObserver observer) {
        UpdateObsevers.Remove(observer);
    }
    #endregion
}

#region Update observer

public interface IUpdateObject {
    void Attach(IUpdateObserver observer);
    void Detach(IUpdateObserver observer);
    void Update();
    void LateUpdate();
}

public interface IUpdateObserver {
    void OnUpdate();
    void OnLateUpdate();
}
#endregion
