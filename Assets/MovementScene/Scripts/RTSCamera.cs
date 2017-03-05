using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour {

    public float ScrollZone = 30;
    public float ScrollSpeed = 5;

    public float xMax;
    public float xMin;

    public float zMax;
    public float zMin;

    public float ZoomMin;
    public float ZoomMax;

    private Vector3 desiredPosition;

	void Start () {
        desiredPosition = transform.position;
	}

    void Update() {
        float y = 0, z = 0;
        float speed = ScrollSpeed * Time.deltaTime;

        if (Input.mousePosition.y < ScrollZone)
            z -= speed;
        else if (Input.mousePosition.y > Screen.height - ScrollZone)
            z += speed;

        if (Input.mouseScrollDelta.y > 0) {
            y -= 5;
            if (transform.position.y > ZoomMin + 5)
                z += 3;
        } else if (Input.mouseScrollDelta.y < 0) {
            //print(y);
            y += 5;
            if (transform.position.y < ZoomMax - 5)
                z -= 3;
        }

        Vector3 moveDelta = new Vector3(0, y, z);
        Vector3 move = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * moveDelta + desiredPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        move.z = Mathf.Clamp(move.z, zMin, zMax);
        move.y = Mathf.Clamp(move.y, ZoomMin, ZoomMax);
        desiredPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);

        if (Input.GetMouseButton(1)) {
            float rotateSpeed = 3F;
            transform.Rotate(Vector3.up, rotateSpeed * Input.GetAxis("Mouse X"), Space.World);
        }
    }
}
