
public class CaptureCrystal : SuprimeMagic {

    public override void cast() {
        if(OwnerSuprime.CurrentCrystal != null) {
            // Если не имеем врагов в пределах видимости радиуса
            if (!OwnerSuprime.CurrentCrystal.DetectRadius.HasFriends()) {
                //Установка начальныйх значений времени каста
                CurrentDurationTime = GameConf.CrystalCaptureCastTime;
                // Запуск таймера
                IsAbleToCast = true;
            }
        }     
    }

    protected override void CastMagic() {
        // TODO убрать кристалл из списка текущего владельца
        //Захватываем кристалл
        OwnerSuprime.CurrentCrystal.ControllingPlayer = OwnerSuprime.ControllingPlayer;
        //Очищаем радиус кристалла
        OwnerSuprime.CurrentCrystal.DetectRadius.ObjectsInside.Clear();
        // Добавляем игроку кристалл
        OwnerSuprime.ControllingPlayer.addCrystall(OwnerSuprime.CurrentCrystal);
    }
}
