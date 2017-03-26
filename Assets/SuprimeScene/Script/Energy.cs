using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {
    private float energy; //Текущее кол-во энергии
    private float maxEnergy; //Максимальное кол-во энергии
	public void changeEnergy(float deltaEnergy) {
        energy += deltaEnergy;
    }
	public void setEnergy(float maxEnergy, float curentEnergy) {
        this.maxEnergy = maxEnergy;
        this.energy = curentEnergy;
    }
}
