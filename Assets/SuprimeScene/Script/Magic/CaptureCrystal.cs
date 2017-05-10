
public class CaptureCrystal : SuprimeMagic {

    public void Setup(Suprime caster) {
        base.Setup(caster, GameConf.crystalCaptureEnergyCost, GameConf.crystalCaptureCastTime);
    }

    public override void TryCast() {
        // если мы уже кастуем и не бежим, то ничего не меняем
        if (!IsCasting && !CanNotRun())
            if (Caster.CurrentCrystal != null) {
                // Если не имеем врагов в пределах видимости радиуса
                if (!Caster.CurrentCrystal.DetectRadius.HasFriends()) {
                    base.TryCast();
                }
            }
    }
    
    protected override bool IsAbleToCast() {
        // не может бежать и кастовать
        return !CanNotRun();
    }
    protected override void ApplyMagic() {
        base.ApplyMagic();
        GameManager.Instance.SwapCrystals(Caster.ControllingPlayer, Caster.CurrentCrystal);
    }
}
