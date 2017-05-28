using System.Collections.Generic;
using UnityEngine;

public class SuprimesUI : MonoBehaviour {

    private List<Suprime> Suprimes { get; set; }
    private List<SuprimeUI> SuprimeUIs { get; set; }

	// Use this for initialization
	void Start () {
        print(GameManager.Instance.GetLocalPlayer());
        Suprimes = GameManager.Instance.GetLocalPlayer().Suprimes;
        SuprimeUIs = new List<SuprimeUI>(GetComponentsInChildren<SuprimeUI>());
	}
	
	// Update is called once per frame
	void Update () {
        int unusedUI = SuprimeUIs.Count;

        foreach (Suprime suprime in Suprimes) {
            SuprimeUIs [SuprimeUIs.Count - (unusedUI--)].Subject = suprime;
        }

        while(unusedUI-- > 1) {
            SuprimeUIs [SuprimeUIs.Count - (unusedUI)].Subject = null;
        }
	}
}
