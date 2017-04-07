using UnityEngine;

[RequireComponent (typeof (Energy))]
[RequireComponent (typeof (Level))]
[RequireComponent (typeof (Radius))]
public class Crystal : BaseObject, ILeveable {

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

    #region MonoBehaviours methods

    public void Start() {
        EnergySystem = GetComponent<Energy>();
        EnergySystem.setupSystem(GameConf.crysStartEnergy,
            GameConf.crysMaxEnergy);

        LevelSystem = GetComponent<Level>();
        LevelSystem.setupSystem(GameConf.crysStartLevel,
            GameConf.crysMaxLevel);

        setupBaseObject(null, // Кристалл инициализируется нейтральным
            GameConf.crysAlarmRadius,
            GameConf.crysDetectRadius);

        RegenSpeed = GameConf.getCrysRegenSpeed(LevelSystem.CurentLevel);
    }

    new void Update() {
        base.Update();
        //Если кто-нибудь владеет кристалом то вырабатываем энергию
        if (ControllingPlayer != null)
            EnergySystem.changeEnergy(RegenSpeed * Time.deltaTime);
    }
    #endregion

    #region ILevelable implementation

    public void levelUp() {
        LevelSystem.levelUp();
        RegenSpeed = GameConf.getCrysRegenSpeed(LevelSystem.CurentLevel);
    }

    #endregion
}
