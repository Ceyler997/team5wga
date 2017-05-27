using UnityEngine;

[RequireComponent(typeof(Level))]
abstract public class ColorMagic : GroupMagic {

    private BattleMagicColor Color { get; set; }
    private float OldCritChance { get; set; }

    protected void Setup(Suprime caster, BattleMagicColor color) {
        base.Setup(caster,
            GameConf.colorEnergyCost,
            GameConf.colorCastTime,
            GameConf.colorDuration);

        Color = color;
        LevelSystem = GetComponent<Level>();
        LevelSystem.SetupSystem(GameConf.colorStartLevel,
            GameConf.colorMaxLevel, 
            GameConf.colorEnergyCost);
    }

    override protected void ApplyMagic() {
        base.ApplyMagic();
        LevelSystem.AddExp(EnergyCost);
        OldCritChance = Units [0].CombatSys.CritChance;

        foreach (Unit unit in Units) {
            unit.CombatSys.CurrentMagicColor = Color;
            unit.CombatSys.CritChance = GameConf.GetCritChance(LevelSystem.CurrentLevel);
        }

        StartCoroutine(WaitForEffect());
    }

    override protected void CancelMagic() {
        base.CancelMagic();
        foreach (Unit unit in Units) {
            unit.CombatSys.CurrentMagicColor = BattleMagicColor.NO_COLOR;
            unit.CombatSys.CritChance = OldCritChance;
        }
    }
}
