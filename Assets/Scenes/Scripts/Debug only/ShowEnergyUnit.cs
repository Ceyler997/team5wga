using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnergyUnit : MonoBehaviour {
	public TextMesh text;
	public Suprime suprime;
	// Use this for initialization
	void Start () {
		GameObject gameObj = new GameObject("Text");
		gameObj.transform.SetParent(gameObject.transform);
		text = gameObj.AddComponent<TextMesh>();
		suprime = gameObject.GetComponent<Suprime>();
		text.transform.position = suprime.transform.position;
		text.transform.position += new Vector3(0,2.5f,-1);
		text.transform.localEulerAngles = new Vector3(128,57.9f,-178.5f);
	}
	
	// Update is called once per frame
	void Update () {
		text.text = Mathf.Round(suprime.energy).ToString();	
	}
}
