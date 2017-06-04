using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GroupMagic : Magic {

    public override bool IsAbleToStartCast {
        get {
            return !Caster.Magic.IsCasting
            && !IsActive
            && Caster.EnergySystem.CurrentEnergy >= EnergyCost
            && Caster.Units.Count > 0;
        }
    }

    protected bool IsActive {
        get {
            return Caster.Magic.IsActive;
        }
        set {
            Caster.Magic.IsActive = value;
        }
    }
    protected bool IsCasting {
        get { return Caster.Magic.IsCasting; }
    }

    protected List<Unit> Units { get; private set; } // Юниты, к которым применяется эффект
    public Level LevelSystem { get; protected set; } // уровень заклинания (уровень мага или уровень заклинания для боевых)
    private float Duration { get; set; } // продолжительность эффекта
    public int ID { get; set; }

    virtual protected void Setup(Suprime caster, float energyCost, float castTime, float durationTime) {
        base.Setup(caster, energyCost, castTime);
        Units = caster.Units;
        Duration = durationTime;
    }

    override protected void ApplyMagic() {
        base.ApplyMagic();

        foreach(Unit unit in Units) {
            unit.SpellID = ID;
            unit.UnitModelMaterial.color = GameConf.GetModelColorByID(ID);
            unit.UnitParticleMaterial.SetColor("_TintColor", GameConf.GetParticlesColorByID(ID));
        }

        Caster.EnergySystem.ChangeEnergy(-EnergyCost);
        IsActive = true;
    }

    protected IEnumerator WaitForEffect() {
        yield return new WaitForSeconds(Duration);
        CancelMagic();
    }

    virtual protected void CancelMagic() {
        IsActive = false;
        foreach (Unit unit in Units) {
            unit.SpellID = 0;
            unit.UnitModelMaterial.color = GameConf.GetModelColorByID(0);
            unit.UnitParticleMaterial.SetColor("_TintColor", GameConf.GetParticlesColorByID(0));
        }
    }

    // возвращает true, если маг может кастовать (не в движении, хватает маны и есть юниты)
    override protected bool IsAbleToCast() {
        return Caster.MoveSystem.IsFinishedMovement
            && !IsActive
            && Caster.EnergySystem.CurrentEnergy >= EnergyCost
            && Caster.Units.Count > 0;
    }
}

public class BattleMagicColor {
    public static readonly BattleMagicColor NO_COLOR = new BattleMagicColor(NO_COLOR, 0);
    public static readonly BattleMagicColor WHITE = new BattleMagicColor(BLACK, 1);
    public static readonly BattleMagicColor RED = new BattleMagicColor(WHITE, 2);
    public static readonly BattleMagicColor BLACK = new BattleMagicColor(RED, 3);

    public BattleMagicColor CounterMagic { get; private set; }

    public int MagicID { get; private set; }

    private BattleMagicColor(BattleMagicColor counterMagic, int ID) {
        CounterMagic = counterMagic;
        MagicID = ID;
    }

    public static BattleMagicColor GetMagicByID(int ID) {
        if (ID == NO_COLOR.MagicID) {
            return NO_COLOR;
        } else if (ID == WHITE.MagicID) {
            return WHITE;
        } else if (ID == RED.MagicID) {
            return RED;
        } else if (ID == BLACK.MagicID) {
            return BLACK;
        } else {
            throw new UnknownMagicException();
        }
    }
}