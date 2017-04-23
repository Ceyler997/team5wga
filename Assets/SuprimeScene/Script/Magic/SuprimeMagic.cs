
public abstract class SuprimeMagic : Magic {
    private Suprime suprime; // ВС который кастует
	float castRadius; // максимальное расстояние до цели
	public Suprime OwnerSuprime {set { suprime = value; } get { return suprime; }}
    
    public void setup(Suprime suprime, float castEnergy, float durationTime) {
        base.setup(castEnergy, durationTime);
        this.suprime = suprime;
    }

    protected bool canNotRun() {
        // Если суприм побежал, то сбрасываем каст
        if (!OwnerSuprime.MoveSystem.IsFinishedMovement) {
            return true;
        }
        return false;
    }
    public void ChangeEnergy() {
        // Отнимаем энергию
        OwnerSuprime.EnergySystem.changeEnergy(CastEnergy);
    }
}
