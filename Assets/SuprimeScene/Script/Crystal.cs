using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Energy))]
public class Crystal : BaseObject {
    public Player controllPlayer;
    private Energy energy;
    void Start () {
        //energy = GetComponent<Energy>();
        //setEnergy();
    }

	 void setEnergy() {
        energy.setEnergy(controllPlayer.getManager.MaxSuprimeEnergy,
                        controllPlayer.getManager.MaxSuprimeEnergy);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
