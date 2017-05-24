
abstract public class CrystalMagic : Magic {

    protected override void ApplyMagic() {
        Caster.CurrentCrystal.EnergySystem.changeEnergy(-EnergyCost);
        base.ApplyMagic();
    }

    protected bool IsCrystalFree() {
        return Caster.CurrentCrystal != null
            && Caster.CurrentCrystal.ControllingPlayer == Caster.ControllingPlayer
            && !Caster.CurrentCrystal.DetectRadius.HasEnemies();
    }

}
