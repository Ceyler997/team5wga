
using UnityEngine;

public class SummonSuprime : CrystalMagic {

    public override bool IsAbleToStartCast { get {
            return !Caster.Magic.IsCasting
            && IsCrystalFree()
            && Caster.CurrentCrystal.EnergySystem.CurrentEnergy >= EnergyCost
            && !Caster.ControllingPlayer.IsSuprimesCountMax();
        }
    }

    public override void Setup(Suprime caster) {
        base.Setup(caster, GameConf.suprimeSummonEnergyCost, GameConf.suprimeSummonCastTime);
    }

    protected override bool IsAbleToCast() {
        return Caster.MoveSystem.IsFinishedMovement
            && IsCrystalFree()
            && Caster.CurrentCrystal.EnergySystem.CurrentEnergy >= EnergyCost
            && !Caster.ControllingPlayer.IsSuprimesCountMax();
    }

    protected override void ApplyMagic() {
        base.ApplyMagic();
        Vector2 shift = Random.insideUnitCircle * Caster.CurrentCrystal.DetectRadius.RadiusValue;
        Caster.ControllingPlayer.AddSuprime(Caster.CurrentCrystal.Position 
            + new Vector3(shift.x, 0, shift.y));
    }
}
