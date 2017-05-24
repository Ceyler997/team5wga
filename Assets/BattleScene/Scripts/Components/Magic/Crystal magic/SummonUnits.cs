
using System.Collections;
using UnityEngine;

public class SummonUnits : CrystalMagic {

    public override void Setup(Suprime caster) {
        base.Setup(caster,
            GameConf.unitsSummonEnergyCost,
            GameConf.unitsSummonCastTime);
    }

    protected override bool IsAbleToCast() {
        return Caster.MoveSystem.IsFinishedMovement
            && Caster.CurrentCrystal.EnergySystem.CurrentEnergy >= EnergyCost;
    }

    protected override void ApplyMagic() {
        base.ApplyMagic();
        StartCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits() {
        int unitsAmount = GameConf.GetUnitsAmount(Caster.LevelSystem.CurrentLevel);

        while (unitsAmount-- > 0) {
            Vector2 shift = Random.insideUnitCircle * Caster.CurrentCrystal.DetectRadius.RadiusValue;
            Caster.AddUnit(Caster.CurrentCrystal.Position
            + new Vector3(shift.x, 0, shift.y));
            yield return new WaitForSeconds(GameConf.unitsSummonDelay);
        }
    }

    public override void TryCast() {
        base.TryCast();

        if(!Caster.Magic.IsCasting
            && Caster.CurrentCrystal != null
            && Caster.CurrentCrystal.ControllingPlayer == Caster.ControllingPlayer
            && Caster.CurrentCrystal.EnergySystem.CurrentEnergy >= EnergyCost
            && Caster.Units.Count == 0) {

            base.StartCasting();
        }
    }
}
