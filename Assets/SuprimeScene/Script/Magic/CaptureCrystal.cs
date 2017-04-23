
public class CaptureCrystal : SuprimeMagic {

    public override void cast() {
        // если мы уже кастуем и не бежим, то ничего не меняем
        if (!IsAbleToCast && !canNotRun())
            if (OwnerSuprime.CurrentCrystal != null) {
                // Если не имеем врагов в пределах видимости радиуса
                if (!OwnerSuprime.CurrentCrystal.DetectRadius.HasFriends()) {
                    //Установка начальныйх значений времени каста
                    CurrentDurationTime = GameConf.CrystalCaptureCastTime;
                    // Запуск таймера
                    IsAbleToCast = true;
                }
            }
    }
    
    protected override bool CastCondition() {
        // не может бежать и кастовать
        return canNotRun();
    }
    protected override void CastMagic() {
        GameManager.Instance.SwapCrystals(OwnerSuprime.ControllingPlayer, OwnerSuprime.CurrentCrystal);
    }
}
