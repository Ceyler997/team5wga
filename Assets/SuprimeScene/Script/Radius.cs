using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour {
    public SphereCollider radius;
    List<BaseCharacter> enemyList;
    // Use this for initialization
    public void Initialization (float radiusSize) {
        enemyList = new List<BaseCharacter>();
        radius = gameObject.AddComponent<SphereCollider>();
		radius.radius = radiusSize; 	
		radius.isTrigger = true;
	}

	void OnTriggerEnter(Collider other) {
        Debug.Log("Enter " + other.name);
    }

	void OnTriggerExit(Collider other) {
		Debug.Log("Exit " + other.name);
    	
	}
	
}
