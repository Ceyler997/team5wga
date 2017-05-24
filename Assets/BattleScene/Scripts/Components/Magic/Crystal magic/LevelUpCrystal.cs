
public class LevelUpCrystal : CrystalMagic {
    public override void Setup(Suprime caster) {
        base.Setup(caster,
            0, // будет перезаписано на момент каста
            GameConf.lvlUpCastTime);
    }

    protected override bool IsAbleToCast() {
        Crystal crys = Caster.CurrentCrystal;
        return Caster.MoveSystem.IsFinishedMovement
            && IsCrystalFree()
            && crys.EnergySystem.CurrentEnergy == crys.EnergySystem.MaxEnergy
            && crys.LevelSystem.CurrentLevel < crys.LevelSystem.MaxLevel;
    }

    protected override void ApplyMagic() {
        base.ApplyMagic();
        Caster.CurrentCrystal.LevelUp();
    }

    override public void TryCast() {
        base.TryCast();
        Crystal crys = Caster.CurrentCrystal;

        if (!Caster.Magic.IsCasting
            && IsCrystalFree()
            && crys.EnergySystem.CurrentEnergy == crys.EnergySystem.MaxEnergy
            && crys.LevelSystem.CurrentLevel < crys.LevelSystem.MaxLevel) {

            EnergyCost = Caster.CurrentCrystal.EnergySystem.MaxEnergy;
            base.StartCasting();
        }
    }
}
