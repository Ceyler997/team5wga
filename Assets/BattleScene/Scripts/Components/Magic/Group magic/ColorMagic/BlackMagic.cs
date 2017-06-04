
public class BlackMagic : ColorMagic {

    override public void Setup(Suprime caster) {
        ID = 3;
        base.Setup(caster, BattleMagicColor.BLACK);
    }
}
