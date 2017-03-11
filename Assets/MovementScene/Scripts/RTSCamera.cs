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
    private Transform cameraTransform;

    void Start () {
        desiredPosition = transform.position;
        cameraTransform = transform.GetChild(0).GetComponent<Transform>();
	}

    void Update() {
        float x = 0, z = 0;
        float speed = ScrollSpeed * Time.deltaTime;

        if (Input.mousePosition.x < ScrollZone)
            x -= speed;
        else if (Input.mousePosition.x > Screen.height - ScrollZone)
            x += speed;

        if (Input.mousePosition.y < ScrollZone)
            z -= speed;
        else if (Input.mousePosition.y > Screen.height - ScrollZone)
            z += speed;

        Vector3 localCameraPosition = cameraTransform.localPosition;
        if (Input.mouseScrollDelta.y > 0) {
            localCameraPosition.z -= 5;
        } else if (Input.mouseScrollDelta.y < 0) {
            localCameraPosition.z += 5;
        }
        localCameraPosition.z = Mathf.Clamp(localCameraPosition.z, ZoomMin, ZoomMax);
        cameraTransform.localPosition = localCameraPosition;

        Vector3 moveDelta = new Vector3(x, 0, z);
        Vector3 move = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * moveDelta + desiredPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        move.z = Mathf.Clamp(move.z, zMin, zMax);
        desiredPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);

        if (Input.GetMouseButton(2)) {
            float rotateSpeed = 3F;
            transform.Rotate(Vector3.up, rotateSpeed * Input.GetAxis("Mouse X"), Space.World);
        }
    }
}
