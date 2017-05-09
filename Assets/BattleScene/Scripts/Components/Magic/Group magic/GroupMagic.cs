using System;
using System.Collections.Generic;
using UnityEngine;

//Должен идти как отдельный объект
[RequireComponent(typeof(Level))]
abstract public class GroupMagic : Magic {

    protected static bool IsActive { get; set; }

    protected Suprime Caster { get; set; }
    protected List<Unit> Units { get; set; }
    protected Level LevelSystem { get; set; }

    protected void Setup(Suprime caster, float castEnergy, float durationTime) {
        base.setup(castEnergy, durationTime);
        Caster = caster;
        Units = caster.Units;
    }

    public override void cast() {
        if (!IsAbleToCast 
            && !IsActive
            && Caster.EnergySystem.CurrentEnergy >= CastEnergy
            && Caster.Units.Count > 0) {

            CurrentDurationTime = DurationTime;
            Caster.MoveSystem.Stop();
            IsAbleToCast = true;
        }
    }

    // возвращает true, если маг не может колдовать (в движении или не хватает маны)
    protected override bool CastCondition() {
        return !Caster.MoveSystem.IsFinishedMovement 
            || Caster.EnergySystem.CurrentEnergy < CastEnergy
            || IsActive;
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