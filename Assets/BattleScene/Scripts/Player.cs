using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

    #region private fields

    private string playerName; //Имя игрока
    private List<Suprime> suprimes; // ВС, которыми владеет игрок
    private List<Crystal> crystals; // Кристалы, которыми владеет игрок
    #endregion

    #region getters and setters

    //возвращает имя игрока
    public string PlayerName {
        get { return playerName; }
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

    #region PunBehaviour methods
    public override void OnPhotonInstantiate(PhotonMessageInfo info) {
        GameManager.Instance.Players.Add(info.sender.ID, this);
        SetupPlayer(info.sender.NickName);
        ID = info.sender.ID;
        Crystal startCrystal 
            = PhotonView.Find((int) photonView.instantiationData [0])
            .GetComponent<Crystal>();

        GameManager.Instance.SwapCrystals(this, startCrystal);
    }
    #endregion

    #region MonoBehaviour methods

    private void Update() {
        if (Crystals.Count > 0) {
            foreach (Suprime suprime in Suprimes) {
                if (suprime.EnergySystem.CurrentEnergy < suprime.EnergySystem.MaxEnergy) {

                    if (suprime.CurrentCrystal != null
                        && suprime.CurrentCrystal.ControllingPlayer == this) {
                        if (suprime.CurrentCrystal.TransferEnergyToSuprime(suprime)) {
                            continue; // переходим к следующему suprime
                        }
                    }

                    Crystals.Sort((fCrys, sCrys) => {
                        return Vector3.Distance(fCrys.Position, suprime.Position)
                        .CompareTo(Vector3.Distance(sCrys.Position, suprime.Position));
                    });

                    foreach (Crystal crystal in Crystals) {
                        if (crystal.TransferEnergyToSuprime(suprime)) {
                            break; // выходим из цикла кристаллов, передав энергию
                        }
                    }
                }
            }
        } else {
            //Debug.LogWarning(PlayerName + " lose cause have no crystals");
        }
    }
    #endregion

    #region public methods

    //Добавляет ВС в массив suprimes
    public void AddSuprime(Vector3 position) {
        if (!IsSuprimesCountMax()) {

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

    public bool IsSuprimesCountMax() {
        return Suprimes.Count >= GameConf.maxSuprimeAmount;
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
