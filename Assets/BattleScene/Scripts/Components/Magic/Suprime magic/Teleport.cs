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

            // Место телепортации
            Vector2 shift = Random.insideUnitCircle * GameConf.teleportSpawnRadius;
            Vector3 newPositon = new Vector3(crystal.transform.position.x + shift.x,
                Caster.transform.position.y,
                crystal.transform.position.z + shift.y);
            
            // Телепортация ВС
            Caster.transform.position = newPositon;
        }
    }
}
