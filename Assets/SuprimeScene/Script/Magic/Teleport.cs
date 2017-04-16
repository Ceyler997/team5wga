using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : SuprimeMagic {

    public override void cast() {
        int lenght = OwnerSuprime.ControllPlayer.crystalls.Count;
        if (OwnerSuprime.EnergyComponent.energy >= CastEnergy && lenght > 0) {
            //Установка начальныйх значений времени каста
            CurrentDurationTime = GameConf.TeleportCastTime;
            //Запуск таймера
            IsAbleToCast = true;
        }
    }

    protected override void CastMagic() {
        float lenght = OwnerSuprime.ControllPlayer.crystalls.Count;
        if (lenght > 0) {
            int randIndex = (int)UnityEngine.Random.Range(0, lenght);
            Crystal crystall = OwnerSuprime.ControllPlayer.crystalls[randIndex];
            // Место телепортации
            Vector3 position = crystall.transform.position +
                               new Vector3(UnityEngine.Random.Range(-10, 10),
                               crystall.transform.position.y,
                               UnityEngine.Random.Range(-10, 10));

            // Отниманем энергию за использование нашей услуги доставки ВС к кристаллу
            OwnerSuprime.EnergyComponent.changeEnergy(-1.0f * CastEnergy);
            //Чтобы не бежал к последней точке после телепорта
            OwnerSuprime.GetComponent<ControllableUnit>().MoveTo(position);
            // Телепортация ВС
            OwnerSuprime.transform.position = position;
        }
    }
}
