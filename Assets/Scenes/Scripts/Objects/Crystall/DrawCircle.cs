using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour {

	private int lengthOfLineRenderer = 20;
	private float ThetaScale = 0.01f;
	int size;
	LineRenderer line1;
	LineRenderer line2;
	Crystall crystall;
	void Start() {
		crystall = gameObject.GetComponent<Crystall>();
		line1 = createLine("radius1");
		line2 = createLine("radius2");
	}

	void Update() {
		DrawCrystallCircle(line1,crystall.radiusFirst);
		DrawCrystallCircle(line2,crystall.radiusSecond);
	}

	private LineRenderer createLine(string name) {
		GameObject gameObj = new GameObject(name);
		gameObj.transform.SetParent(this.transform);
		LineRenderer lineRender = gameObj.AddComponent<LineRenderer>();
		lineRender.startWidth = 0.02f; 
		lineRender.endWidth = 0.02f; 
		lineRender.widthMultiplier = 3; 
		float sizeValue = (2.0f * Mathf.PI) / ThetaScale;
		size = (int)sizeValue; 
		size++;
		lineRender.numPositions = size;
		return lineRender;
	}
	public void DrawCrystallCircle(LineRenderer lineRenderer,float Radius)  { 
		Vector3 pos; 
		float theta = 0f; 
		for (int i = 0; i < size; i++) { 
			theta += (2.0f * Mathf.PI * ThetaScale); 
			float x = Radius * Mathf.Cos(theta); 
			float z = Radius * Mathf.Sin(theta); 
			x += gameObject.transform.position.x; 
			z += gameObject.transform.position.z; 
			pos = new Vector3(x, 0, z); 
			lineRenderer.SetPosition(i, pos); 
		}
	}
}
