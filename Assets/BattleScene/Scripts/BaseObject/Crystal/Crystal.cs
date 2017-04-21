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

    private Level LevelSystem {
        get {return levelSystem;}

        set {levelSystem = value;}
    }
    #endregion

    public void SetupCrystal(Player owner) {
        SetupBaseObject(owner,
            GameConf.crysAlarmRadius,
            GameConf.crysDetectRadius);

        EnergySystem = GetComponent<Energy>();
        EnergySystem.setupSystem(GameConf.crysStartEnergy,
            GameConf.crysMaxEnergy);

        LevelSystem = GetComponent<Level>();
        LevelSystem.setupSystem(GameConf.crysStartLevel,
            GameConf.crysMaxLevel);

        RegenSpeed = GameConf.getCrysRegenSpeed(LevelSystem.CurrentLevel);
    }

    // Метод для смены владельца, вызывается на стороне нового владельца
    public void ChangeOwner(Player newOwner) {
        ControllingPlayer = newOwner;
        if (PhotonNetwork.connected) {
            photonView.RequestOwnership();
        }
    }

    #region MonoBehaviours methods
    
    new void Update() {
        base.Update();
        // Если кто-нибудь владеет кристалом то вырабатываем энергию
        if (ControllingPlayer != null)
            EnergySystem.changeEnergy(RegenSpeed * Time.deltaTime);
    }
    #endregion

    #region ILevelable implementation

    public void levelUp() {
        LevelSystem.levelUp();
        RegenSpeed = GameConf.getCrysRegenSpeed(LevelSystem.CurrentLevel);
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

    #region DEBUG
    public Player owner;

    public void SetOwner() {
        ChangeOwner(owner);
    }
    #endregion
}
