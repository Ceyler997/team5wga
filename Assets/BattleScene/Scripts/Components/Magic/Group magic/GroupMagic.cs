using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Level))]
abstract public class GroupMagic : Magic {

    protected bool IsActive { get; set; } // активен ли эффект, должно быть возможен только один эффект на мага
    
    protected List<Unit> Units { get; set; } // Юниты, к которым применяется эффект
    protected Level LevelSystem { get; set; } // уровень заклинания (уровень мага или уровень заклинания для боевых)

    override protected void Setup(Suprime caster, float castEnergy, float durationTime) {
        base.Setup(caster, castEnergy, durationTime);
        Units = caster.Units;
    }

    protected override void ApplyMagic() {
        base.ApplyMagic();
        IsActive = true;
    }

    public override void TryCast() {
        if (!IsCasting 
            && !IsActive
            && Caster.EnergySystem.CurrentEnergy >= CastEnergy
            && Caster.Units.Count > 0) {
            base.TryCast();
        }
    }

    // возвращает true, если маг может кастовать (в движении или не хватает маны)
    protected override bool IsAbleToCast() {
        return Caster.MoveSystem.IsFinishedMovement
            && !IsActive
            && Caster.EnergySystem.CurrentEnergy >= CastEnergy
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