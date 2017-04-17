﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

    #region private fields

    public string playerName; //Имя игрока
    private List<Suprime> suprimes; //ВС, которыми владеет игрок
    private List<Crystal> crystals; //Кристалы, которыми владеет игрок
    #endregion

    #region getters and setters

    //возвращает имя игрока
    public string PlayerName {
        get {return playerName; }
        set { playerName = value; }
    }

    //Возвращает массив кристаллов принадлежавших игроку
    public List<Suprime> Suprimes {
        get { return suprimes; }
        set { suprimes = value; }
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
        if(Suprimes.Count < GameConf.maxSuprimeAmount) {
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
        suprimes = new List<Suprime>();
        crystals = new List<Crystal>();
        PlayerName = ownerName;

        if (photonView.isMine) {
            addSuprime(transform.position);
        }
    }
    #endregion
}
