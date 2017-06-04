public class HealMagic : GroupMagic {
    
    private float OldRegen { get; set; }

    override public void Setup(Suprime caster) {
        ID = 1;
        base.Setup(caster,
            GameConf.healEnergyCost,
            GameConf.healCastTime,
            GameConf.healDuration);
    }

    protected override void ApplyMagic() {
        base.ApplyMagic();
        
        OldRegen = Units[0].HealthSystem.RegenSpeed;

        float regenSpeed = GameConf.GetHealRegenSpeed(Caster.LevelSystem.CurrentLevel);
        foreach (Unit unit in Units) {
            unit.HealthSystem.RegenSpeed = regenSpeed;
        }

        StartCoroutine(WaitForEffect());
    }

    protected override void CancelMagic() {
        base.CancelMagic();
        foreach (Unit unit in Units) {
            unit.HealthSystem.RegenSpeed = OldRegen;
        }
    }
}
