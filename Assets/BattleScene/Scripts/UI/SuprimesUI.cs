using System.Collections.Generic;
using UnityEngine;

public class SuprimesUI : MonoBehaviour {

    private Player Owner { get; set; }
    private List<SuprimeUI> SuprimeUIs { get; set; }

	// Use this for initialization
	void Start () {        
        SuprimeUIs = new List<SuprimeUI>(GetComponentsInChildren<SuprimeUI>());
	}
	
	// Update is called once per frame
	void Update () {
        if(Owner == null) {
            Player owner;
            GameManager.Instance.Players.TryGetValue(PhotonNetwork.player.ID, out owner);
            Owner = owner;
            return;
        }

        int unusedUI = SuprimeUIs.Count;

        foreach (Suprime suprime in Owner.Suprimes) {
            SuprimeUIs [SuprimeUIs.Count - (unusedUI--)].Subject = suprime;
        }

        while(unusedUI > 1) {
            SuprimeUIs [SuprimeUIs.Count - (unusedUI--)].Subject = null;
        }
	}
}
