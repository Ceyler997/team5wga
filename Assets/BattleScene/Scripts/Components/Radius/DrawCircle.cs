using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Radius))]
public class DrawCircle : MonoBehaviour {

	private float ThetaScale = 0.01f;
	int size;
    Radius drawRadius;
    LineRenderer line1;

    void Start() {
        drawRadius = GetComponent<Radius>();
        if (drawRadius.RadiusCollider != null)
            line1 = createLine("radius1");
        else
            Debug.Log("null draw RadiusCollider start");
    }

	void Update() {
        if(line1 == null) {
            if (drawRadius.RadiusCollider != null)
                line1 = createLine("radius1");
        }
		if(drawRadius.RadiusCollider != null)
			DrawCrystallCircle(line1,drawRadius.RadiusCollider.radius);
        else
            Debug.Log("null draw RadiusCollider");
    }

	private LineRenderer createLine(string name) {
		GameObject gameObj = new GameObject(name);
		gameObj.transform.SetParent(this.transform);
		LineRenderer lineRender = gameObj.AddComponent<LineRenderer>();
		lineRender.startWidth = 0.05f; 
		lineRender.endWidth = 0.05f; 
		lineRender.widthMultiplier = 3; 
		float sizeValue = (2.0f * Mathf.PI) / ThetaScale;
		size = (int)sizeValue; 
		size++;
		lineRender.positionCount = size;
		return lineRender;
	}
	public void DrawCrystallCircle(LineRenderer lineRenderer,float circleRadius)  {
        Vector3 pos; 
		float theta = 0f; 
		for (int i = 0; i < size; i++) { 
			theta += (2.0f * Mathf.PI * ThetaScale); 
			float x = circleRadius * Mathf.Cos(theta); 
			float z = circleRadius * Mathf.Sin(theta); 
			x += gameObject.transform.position.x; 
			z += gameObject.transform.position.z; 
			pos = new Vector3(x, gameObject.transform.position.y, z); 
			lineRenderer.SetPosition(i, pos); 
		}
	}
}
