using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

    #region private fields

    public string playerName; //Имя игрока
    private Suprime[] suprimes; //ВС, которыми владеет игрок
    private int suprimeCount; //Текущее кол-во ВС
    private List<Crystal> crystals; //Кристалы, которыми владеет игрок
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

    #region MonoBehaviour methods
    public override void OnPhotonInstantiate(PhotonMessageInfo info) {
        GameManager.Instance.Players.Add(info.sender, this);
        setupPlayer(info.sender.NickName);
    }
    #endregion

    #region public methods

    //Добавляет ВС в массив suprimes
    public void addSuprime(Vector3 position) {
        if(SuprimeCount < GameConf.maxSuprimeAmount) {
            PhotonNetwork.Instantiate("SuprimePrefab",
                position,
                Quaternion.identity,
                0).GetComponent<Suprime>();
        } else {
            throw new TooMuchSuprimesException();
        }
    }

    public void addCrystall(Crystal crystal) {
        Crystals.Add(crystal);
        crystal.ControllingPlayer = this;
    }

    public void setupPlayer(String ownerName) {
        suprimes = new Suprime [GameConf.maxSuprimeAmount];
        crystals = new List<Crystal>();
        PlayerName = ownerName;

        if (photonView.isMine) {
            addSuprime(transform.position);
        }
    }
    #endregion
}
