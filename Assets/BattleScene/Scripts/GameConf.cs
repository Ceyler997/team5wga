using System;

static class GameConf {

    #region Unit Properties

    public static readonly float unitStartHealth = 100.0f;
    public static readonly float unitMaxHealth = 100.0f;
    public static readonly float unitBasicRegenSpeed = 5.0f;
    public static readonly float unitDamage = 30.0f;
    public static readonly float unitCritDamage = 5.0f;
    public static readonly float unitAttackRadius = 5.0f;
    public static readonly float unitAttackSpeed = 1.0f;
    public static readonly float unitBasicCritChance = 0.1f;
    public static readonly float unitMoveSpeed = 30.0f;
    public static readonly float unitReactRadius = 5.0f;
    public static readonly float unitDetectRadius = 20.0f;
    #endregion

    #region Suprime Properties

    public static readonly float suprimeStartHealth = 100.0f;
    public static readonly float suprimeMaxHealth = 100.0f;
    public static readonly float suprimeBasicRegenSpeed = 5.0f;
    public static readonly float suprimeStartEnergy = 0.0f;
    public static readonly float suprimeMaxEnergy = 100.0f;
    public static readonly int suprimeStartLevel = 0;
    public static readonly int suprimeMaxLevel = 10;
    public static readonly float suprimeDamage = 20.0f;
    public static readonly float suprimeCritDamage = 5.0f;
    public static readonly float suprimeAttackRadius = 5.0f;
    public static readonly float suprimeAttackSpeed = 2.0f;
    public static readonly float suprimeMoveSpeed = 10.0f;
    public static readonly float suprimeReactRadius = 10.0f;
    public static readonly float suprimeDetectRadius = 10.0f;
    #endregion

    #region Crystall Properties
    public static readonly float crysStartEnergy = 0.0f;
    public static readonly float crysMaxEnergy = 100.0f;
    public static readonly int crysStartLevel = 1;
    public static readonly int crysMaxLevel = 3;
    public static readonly float crysAlarmRadius = 10.0f;
    public static readonly float crysDetectRadius = 20.0f;



    public static float getCrysRegenSpeed(int curentLevel) {
        switch (curentLevel) {
            case 1:
                return 5.0f;
            case 2:
                return 10.0f;
            case 3:
                return 15.0f;
            default:
                return 5.0f;
        }
    }
    #endregion

    #region Player Properties

    public static readonly int maxSuprimeAmount = 9;
    #endregion

    #region Magic Properties

    // Телепорт
    public const float TeleportCastTime = 2.0f;
    public const float TeleportCostEnergy = 10.0f;
    // Захват кристалла
    public const float CrystalCaptureCastTime = 10.0f;
    public const float CrystalCaptureCostEnergy = 50.0f;
    #endregion


}