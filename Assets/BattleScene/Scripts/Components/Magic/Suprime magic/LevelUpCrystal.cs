
public class LevelUpCrystal : SuprimeMagic {
    public override void Setup(Suprime caster) {
        base.Setup(caster,
            GameConf.lvlUpEnergyCost,
            GameConf.lvlUpCastTime);
    }

    protected override bool IsAbleToCast() {
        return !CanNotRun()
            && Caster.CurrentCrystal != null
            && Caster.CurrentCrystal.ControllingPlayer == Caster.ControllingPlayer
            && Caster.CurrentCrystal.LevelSystem.CurrentLevel < Caster.CurrentCrystal.LevelSystem.MaxLevel;
    }

    protected override void ApplyMagic() {
        base.ApplyMagic();
        Caster.CurrentCrystal.LevelUp();
    }

    override public void TryCast() {
        base.TryCast();
        if (!Caster.Magic.IsCasting
            && Caster.EnergySystem.CurrentEnergy >= EnergyCost
            && Caster.CurrentCrystal != null
            && Caster.CurrentCrystal.ControllingPlayer == Caster.ControllingPlayer
            && Caster.CurrentCrystal.LevelSystem.CurrentLevel < Caster.CurrentCrystal.LevelSystem.MaxLevel) {
            base.StartCasting();
        }
    }
}
