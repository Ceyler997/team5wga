using System;
using UnityEngine;

public class Teleport : SuprimeMagic {

    public void Setup(Suprime caster) {
        base.Setup(caster, GameConf.teleportCastEnergy, GameConf.teleportCastTime);
    }

    public override void TryCast() {
        int lenght = Caster.ControllingPlayer.Crystals.Count;
        // если мы уже кастуем, то ничего не меняем
        if (!IsCasting)
            if (Caster.EnergySystem.CurrentEnergy >= CastEnergy && lenght > 0) {
                base.TryCast();
            }
    }

    protected override bool IsAbleToCast() {
        return !CanNotRun();
    }

    protected override void ApplyMagic() {
        base.ApplyMagic();
        float lenght = Caster.ControllingPlayer.Crystals.Count;
        if (lenght > 0) {
            int randIndex = (int)UnityEngine.Random.Range(0, lenght);
            Crystal crystal = Caster.ControllingPlayer.Crystals [randIndex];
            // Отниманем энергию за использование нашей услуги доставки ВС к кристаллу
            // отнимается в базе
            //Чтобы не бежал к последней точке после телепорта
            // Останавливается при касте OwnerSuprime.MoveSystem.Stop();

            // Место телепортации
            Vector2 shift = UnityEngine.Random.insideUnitCircle * GameConf.teleportSpawnRadius;
            // Телепортация ВС
            Caster.transform.position = crystal.transform.position + new Vector3(shift.x, shift.y);
        }
    }
}
