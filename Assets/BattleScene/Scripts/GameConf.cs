using System;

static class GameConf {

    #region Unit Properties

    public static readonly float unitStartHealth = 50.0f;
    public static readonly float unitMaxHealth = 50.0f;
    public static readonly float unitBasicRegenSpeed = - unitMaxHealth / 50.0f;
    public static readonly float unitDamage = 2.0f;
    public static readonly float unitCritDamage = 6.0f;
    public static readonly float unitAttackRadius = 5.0f;
    public static readonly float unitAttackSpeed = 1.0f;
    public static readonly float unitBasicCritChance = 0.1f;
    public static readonly float unitMoveSpeed = 15.0f;
    public static readonly float unitReactRadius = 5.0f;
    public static readonly float unitDetectRadius = 20.0f;
    public static readonly float unitFollowDistance = 20.0f;

    public static readonly float regenDelay = 50.0f;
    #endregion

    #region Suprime Properties

    public static readonly float suprimeStartHealth = 100.0f;
    public static readonly float suprimeMaxHealth = 100.0f;
    public static readonly float suprimeBasicRegenSpeed = 2.0f;
    public static readonly float suprimeStartEnergy = 100.0f;
    public static readonly float suprimeMaxEnergy = 100.0f;
    public static readonly int suprimeStartLevel = 1;
    public static readonly int suprimeMaxLevel = 5;
    public static readonly float suprimeDamage = 20.0f;
    public static readonly float suprimeCritDamage = 5.0f;
    public static readonly float suprimeAttackRadius = 5.0f;
    public static readonly float suprimeAttackSpeed = 2.0f;
    public static readonly float suprimeMoveSpeed = 5.0f;
    public static readonly float suprimeReactRadius = 10.0f;
    public static readonly float suprimeDetectRadius = 20.0f;
    internal static readonly float suprimeLevelUpExp = 100.0f;
    #endregion

    #region Crystal Properties
    public static readonly float crysStartEnergy = 0.0f;
    public static readonly float crysMaxEnergy = 100.0f;
    public static readonly int crysStartLevel = 1;
    public static readonly int crysMaxLevel = 3;
    public static readonly float crysAlarmRadius = 10.0f;
    public static readonly float crysDetectRadius = 20.0f;
    public static readonly float crysMaxTransferSpeed = 4;
    public static readonly float crysMinTransferSpeed = 1;

    public static float GetCrysRegenSpeed(int curentLevel) {
        return curentLevel * 5.0f;
    }
    #endregion

    #region Player Properties

    public static readonly int maxSuprimeAmount = 9;
    #endregion

    #region Magic Properties

    // Телепорт
    public static readonly float teleportCastTime = 2.0f;
    public static readonly float teleportEnergyCost = 10.0f;
    public static readonly float teleportSpawnRadius = 10.0f;
    // Захват кристалла
    public static readonly float crystalCaptureCastTime = 3.0f;
    public static readonly float crystalCaptureEnergyCost = 50.0f;
    // Улучшение кристалла
    public static readonly float lvlUpCastTime = 2.0f;
    // Призыв suprime
    public static readonly float suprimeSummonCastTime = 2.0f;
    public static readonly float suprimeSummonEnergyCost = 50.0f;
    // Призыв юнитов
    public static readonly float unitsSummonCastTime = 2.0f;
    public static readonly float unitsSummonEnergyCost = 50.0f;
    public static readonly float unitsSummonDelay = 1.0f;

    public static int GetUnitsAmount(int level) {
        return level + 4;
    }

    #region Group Magic

    public static readonly float healEnergyCost = 50.0f;
    public static readonly float healCastTime = 1.0f;
    public static readonly float healDuration = 20.0f;

    public static float GetHealRegenSpeed(int level) {
        return ((0.1f + level / 10.0f) * unitMaxHealth) / healDuration;
    }

    public static readonly float colorEnergyCost = 50.0f;
    public static readonly float colorCastTime = 1.0f;
    public static readonly float colorDuration = 20.0f;
    public static readonly int colorStartLevel = 1;
    public static readonly int colorMaxLevel = 10;

    public static float GetCritChance(int level) {
        return (0.1f + ((float) level / (colorMaxLevel - colorStartLevel)) * 0.4f);
    }
    #endregion
    #endregion
}