
public abstract class SuprimeMagic : Magic {

	float castRadius; // максимальное расстояние до цели

    protected bool IsCasting {
        get { return Caster.Magic.IsCasting; }
    }

    protected bool CanNotRun() {
        // Если суприм побежал, то сбрасываем каст
        return !Caster.MoveSystem.IsFinishedMovement;
    }
}
