using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {
    private float energy; //Текущее кол-во энергии
    private float maxEnergy; //Максимальное кол-во энергии

    // Use this for initialization
    void Start () {
		
	}
	public void restoreEnergy(float energy) {
        energy += energy;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
