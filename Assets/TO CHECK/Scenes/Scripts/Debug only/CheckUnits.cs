using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// НЕИСПОЛЬЗУЕМЫЙ КЛАСС

//Временный скрипт для отладки юнитов при создании кристалла

//public class CheckUnits : MonoBehaviour {

//	public BaseCharacter selectUnit = null;
//	private Camera mainCamera; 
//	// Use this for initialization
//	void Start () {
//	}
	
///* 	// Update is called once per frame
//	void Update () {
//		if(selectUnit!=null)
//			Moving();

//		/*if (Input.GetKeyDown ("space")) {
//            Debug.Log("Press Key");
//            Destroy(selectUnit.gameObject);
//        }
//	//	MouseSelect();
	
//		 if(Input.GetMouseButtonDown(0)) {
//			RaycastHit hitInfo = new RaycastHit();
//			bool hit = Physics.Raycast( mainCamera.ScreenPointToRay(Input.mousePosition),out hitInfo);
//			if(hit) {
//				Suprime unit = hitInfo.transform.gameObject.GetComponent<Suprime>();
//				if(unit) {
//					selectUnit = unit;
//				}
//			}
//		 }
	
//	}
	
//	void Moving() {
//		float h = Input.GetAxis("Horizontal")*selectUnit.movementSpeed;
//		float v = Input.GetAxis("Vertical")*selectUnit.movementSpeed;
//		Vector3 move = new Vector3(v * -1, 0, h);
//		move = Vector3.ClampMagnitude(move, selectUnit.movementSpeed);
//		move *= Time.deltaTime;
//		selectUnit.transform.Translate(move);
//	}
//*/
//	void MouseSelect() {
//	}
//}
