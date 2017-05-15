using System.Collections;
using UnityEngine;

public class HealMagic : GroupMagic {

    private float Duration { get { return GameConf.healDuration; } }
    private float OldRegen { get; set; }

    public void Setup(Suprime caster) {
        
        base.Setup(caster,
            GameConf.healEnergyCost,
            GameConf.healCastTime);
    }

    protected override void ApplyMagic() {
        base.ApplyMagic();
        
        OldRegen = Units[0].HealthSystem.RegenSpeed;

        StartCoroutine(ApplyRegen());
    }

    private IEnumerator ApplyRegen() {
        float regenSpeed = GameConf.GetHealRegenSpeed(Caster.LevelSystem.CurrentLevel);
        foreach (Unit unit in Units) {
            unit.HealthSystem.RegenSpeed = regenSpeed;
        }

        yield return new WaitForSeconds(Duration);
        CancelMagic();
    }

    protected override void CancelMagic() {
        base.CancelMagic();
        foreach (Unit unit in Units) {
            unit.HealthSystem.RegenSpeed = OldRegen;
        }
    }
}
