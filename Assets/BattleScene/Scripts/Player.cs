using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

    #region private fields

    public string playerName; //Имя игрока
    private List<Suprime> suprimes; // ВС, которыми владеет игрок
    private List<Crystal> crystals; // Кристалы, которыми владеет игрок
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

    public int ID { get; private set; } // ID игрока, выставляется в соответствии с Photon ID
    #endregion

    #region MonoBehaviour methods
    public override void OnPhotonInstantiate(PhotonMessageInfo info) {
        GameManager.Instance.Players.Add(info.sender.ID, this);
        SetupPlayer(info.sender.NickName);
        ID = info.sender.ID;
    }
    #endregion

    #region public methods

    //Добавляет ВС в массив suprimes
    public void AddSuprime(Vector3 position) {
        if (Suprimes.Count < GameConf.maxSuprimeAmount) {
            if (PhotonNetwork.connected) {
                PhotonNetwork.Instantiate("SuprimePrefab",
                    position,
                    Quaternion.identity,
                    0);
            } else {
                Suprime newSuprime = Instantiate(((OfflineGameManager) GameManager.Instance).suprimePrefab, 
                    position, 
                    Quaternion.identity).GetComponent<Suprime>();
                newSuprime.SetupSuprime(this);
            }
        } else {
            throw new TooMuchSuprimesException();
        }
    }

    // Тут немного несостыковка получается. Мы юзаем AddSuprime и AddUnit для того, чтобы создать новый объект на позиции
    // А тут логики почти нет, но единообразие методов нарушается, т.к. он ничего не создаёт
    public void AddCrystal(Crystal crystal) {
        Crystals.Add(crystal);    
    }

    public void SetupPlayer(String ownerName) {
        suprimes = new List<Suprime>();
        crystals = new List<Crystal>();
        PlayerName = ownerName;

        if (photonView.isMine) {
            AddSuprime(transform.position);
        }
    }
    #endregion
}
