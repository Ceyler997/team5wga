using UnityEngine;

[RequireComponent (typeof (Energy))]
[RequireComponent (typeof (Level))]
[RequireComponent (typeof (Radius))]
public class Crystal : BaseObject, ILeveable, IPunObservable {

    #region private fields

    private float regenSpeed; //Скорость восстановления энергии
    private Energy energySystem; //Текущее кол-во энергии
    private Level levelSystem; //Уровень кристалла
    #endregion

    #region getters and setters

    private float RegenSpeed {
        get {return regenSpeed;}

        set {regenSpeed = value;}
    }

    public Energy EnergySystem {
        get {return energySystem;}

        set {energySystem = value;}
    }

    public Level LevelSystem {
        get {return levelSystem;}

        private set {levelSystem = value;}
    }

    private Material material;
    #endregion

    public void SetupCrystal(Player owner) {
        SetupBaseObject(owner,
            GameConf.crysAlarmRadius,
            GameConf.crysDetectRadius);

        EnergySystem = GetComponent<Energy>();
        EnergySystem.SetupSystem(GameConf.crysStartEnergy,
            GameConf.crysMaxEnergy);

        LevelSystem = GetComponent<Level>();
        LevelSystem.SetupSystem(GameConf.crysStartLevel,
            GameConf.crysMaxLevel, 
            0.0f);

        RegenSpeed = GameConf.GetCrysRegenSpeed(LevelSystem.CurrentLevel);

        material = GetComponent<Renderer>().material;
    }

    // Метод для смены владельца, вызывается на стороне нового владельца
    public void ChangeOwner(Player newOwner) {
        if (ControllingPlayer != newOwner) {
            ControllingPlayer = newOwner;

            if (PhotonNetwork.connected && newOwner.photonView.isMine) {
                photonView.RequestOwnership();
            }
        }
    }

    public bool TransferEnergyToSuprime(Suprime suprime) {
        float energyToTransfer;
        if(Vector3.Distance(Position, suprime.Position) < DetectRadius.RadiusValue) {
            energyToTransfer = GameConf.crysMaxTransferSpeed;
        } else {
            energyToTransfer = GameConf.crysMinTransferSpeed;
        }

        energyToTransfer *= Time.deltaTime;

        if(EnergySystem.CurrentEnergy < energyToTransfer) {
            return false;
        } else {
            suprime.EnergySystem.ChangeEnergy(energyToTransfer);
            EnergySystem.ChangeEnergy(-energyToTransfer);
            return true;
        }
    }

    #region MonoBehaviours methods

    new void Update() {
        base.Update();
        // Если кто-нибудь владеет кристалом то вырабатываем энергию
        if (ControllingPlayer != null)
            EnergySystem.ChangeEnergy(RegenSpeed * Time.deltaTime);

        if (material != null) {
            float energyLevel = 2.0f * EnergySystem.CurrentEnergy / EnergySystem.MaxEnergy;
            material.SetColor("_EmissionColor", new Color(energyLevel, energyLevel, energyLevel));
        }
    }
    #endregion

    #region ILevelable implementation

    public void LevelUp() {
        LevelSystem.LevelUp();
        RegenSpeed = GameConf.GetCrysRegenSpeed(LevelSystem.CurrentLevel);
    }
    #endregion

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            if (ControllingPlayer != null) {
                stream.SendNext(ControllingPlayer.ID);
            } else {
                stream.SendNext(-1);
            }
        } else {
            int receivedID = (int) stream.ReceiveNext();
            if (receivedID != -1 && (ControllingPlayer == null || receivedID != ControllingPlayer.ID)) {
                Player newOwner;
                GameManager.Instance.Players.TryGetValue(receivedID, out newOwner);
                ControllingPlayer = newOwner;
            }
        }
    }
    #endregion
}
