
public class CaptureCrystal : SuprimeMagic {

    public override bool IsAbleToStartCast {
        get {
            return !IsCasting
            && Caster.CurrentCrystal != null
            && !Caster.CurrentCrystal.DetectRadius.HasFriends();
        }
    }

    override public void Setup(Suprime caster) {
        base.Setup(caster, GameConf.crystalCaptureEnergyCost, GameConf.crystalCaptureCastTime);
    }
    
    protected override bool IsAbleToCast() {
        // не может бежать и кастовать
        return !CanNotRun() && !Caster.CurrentCrystal.DetectRadius.HasFriends();
    }

    protected override void ApplyMagic() {
        base.ApplyMagic();
        GameManager.Instance.SwapCrystals(Caster.ControllingPlayer, Caster.CurrentCrystal);
    }
}
