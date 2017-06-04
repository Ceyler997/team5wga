
public class WhiteMagic : ColorMagic {

    override public void Setup(Suprime caster) {
        ID = 2;
        base.Setup(caster, BattleMagicColor.WHITE);
    }
}

