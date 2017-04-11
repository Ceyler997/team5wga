using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region private fields

    public string playerName; //Имя игрока
    private Suprime[] suprimes; //ВС, которыми владеет игрок
    private int suprimeCount; //Текущее кол-во ВС
    private List<Crystal> crystals; //Кристалы, которыми владеет игрок
    // Use this for initialization
    public GameObject SuprimePrefab;
    #endregion

    #region getters and setters

    //возвращает имя игрока
    public string PlayerName {
        get {return playerName; }
        set { playerName = value; }
    }

    //Возвращает массив кристаллов принадлежавших игроку
    public Suprime [] Suprimes {
        get { return suprimes; }
        set { suprimes = value; }
    }

    //Возвращает кол-во ВС под контролем игрока
    public int SuprimeCount {
        get { return suprimeCount; }
        set { suprimeCount = value; }
    }

    //Возвращает массив кристаллов принадлежавших игроку
    public List<Crystal> Crystals {
        get { return crystals; }
        set { crystals = value; }
    }
    #endregion

    #region public methods

    //Добавляет ВС в массив suprimes
    public void addSuprime(Vector3 position) {
        if(SuprimeCount < GameConf.maxSuprimeAmount) {
            Suprime suprime = Instantiate(SuprimePrefab, position, Quaternion.identity).GetComponent<Suprime>();
            suprime.setupSuprime(this);
            suprimes[SuprimeCount] = suprime;
            ++SuprimeCount;
        } else {
            throw new TooMuchSuprimesException();
        }
    }

    public void addCrystall(Crystal crystal) {
        Crystals.Add(crystal);
        crystal.ControllingPlayer = this;
    }
    #endregion

    #region private methods

    private void setupPlayer() {
        suprimes = new Suprime [GameConf.maxSuprimeAmount];
        crystals = new List<Crystal>();
        addSuprime(transform.position);
    }
    #endregion

    #region DEBUG
    public void spawnUnit() {
        Suprimes [0].spawnUnit();
    }
    #endregion
}
