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
        LevelSystem.setupSystem(GameConf.colorStartLevel,
            GameConf.colorMaxLevel);
    }

    override protected void ApplyMagic() {
        base.ApplyMagic();
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
