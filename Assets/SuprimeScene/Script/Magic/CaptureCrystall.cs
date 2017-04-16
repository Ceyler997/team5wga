using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureCrystall : SuprimeMagic {

    public override void cast() {
        if(OwnerSuprime.curentCrystall != null) {
            // Если не имеем врагов в пределах видимости радиуса
            if (!OwnerSuprime.curentCrystall.Radius.HasFriends()) {
                //Установка начальныйх значений времени каста
                CurrentDurationTime = GameConf.CrystallCaptureCastTime;
                // Запуск таймера
                IsAbleToCast = true;
            }
        }     
    }

    protected override void CastMagic() {
        //Захватываем кристалл
        OwnerSuprime.curentCrystall.setPlayer(OwnerSuprime.ControllPlayer);
        // Добавляем игроку кристалл
        OwnerSuprime.ControllPlayer.addCrystall(OwnerSuprime.curentCrystall);
    }
}
