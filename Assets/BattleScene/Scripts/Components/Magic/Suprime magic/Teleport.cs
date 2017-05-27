using UnityEngine;

public class Teleport : SuprimeMagic {

    public override bool IsAbleToStartCast {
        get {
            Crystal crys = Caster.CurrentCrystal;
            return !Caster.Magic.IsCasting
            && Caster.EnergySystem.CurrentEnergy >= EnergyCost
            && Caster.ControllingPlayer.Crystals.Count > 0;
        }
    }

    override public void Setup(Suprime caster) {
        base.Setup(caster, GameConf.teleportEnergyCost, GameConf.teleportCastTime);
    }

    protected override bool IsAbleToCast() {
        return !CanNotRun();
    }

    protected override void ApplyMagic() {
        base.ApplyMagic();
        float lenght = Caster.ControllingPlayer.Crystals.Count;
        if (lenght > 0) {
            int randIndex = (int) Random.Range(0, lenght);
            Crystal crystal = Caster.ControllingPlayer.Crystals [randIndex];
            // Отниманем энергию за использование нашей услуги доставки ВС к кристаллу
            // отнимается в базе
            //Чтобы не бежал к последней точке после телепорта
            // Останавливается при касте OwnerSuprime.MoveSystem.Stop();

            // Место телепортации
            Vector2 shift = Random.insideUnitCircle * GameConf.teleportSpawnRadius;
            // Телепортация ВС
            Caster.transform.position = crystal.transform.position + new Vector3(shift.x, shift.y);
        }
    }
}
