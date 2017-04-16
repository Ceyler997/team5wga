using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SuprimeMagic : Magic {
    private Suprime suprime; // ВС который кастует
	float castRadius; // максимальное расстояние до цели
	public Suprime OwnerSuprime {set { suprime = value; } get { return suprime; }}
    
    public void setup(Suprime suprime, float castEnergy, float durationTime) {
        base.setup(castEnergy, durationTime);
        this.suprime = suprime;
    }
    
    public void ChangeEnergy() {
        // Отнимаем энергию
        OwnerSuprime.EnergyComponent.changeEnergy(CastEnergy);
    }
}
