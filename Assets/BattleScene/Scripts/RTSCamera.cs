using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour {

    public float ScrollZone = 30;//Размер границы края экрана для перемещения камеры
    public float ScrollSpeed = 5; //Скорость приближения

    public float xMax;// Максимальное смещение по оси X
    public float xMin;// Минимальное смещение по оси X
    public float zMax;// Максимальное смещение по оси Z
    public float zMin;// Минимальное смещение по оси Z

    public float ZoomMin;//Минимальное значение приближения
    public float ZoomMax;//Максимальное значение приближения

    public bool bUseKeyboard = false; //Использование клавиатуры для пермещения
    public bool bUseMouse = true; //Использование клавиатуры для пермещения    

    private Vector3 desiredPosition;
    private Transform cameraTransform;

    void Start () {
        desiredPosition = transform.position;
        cameraTransform = transform.GetChild(0).GetComponent<Transform>();
	}

    // Управление камерой с клавиатуры
    Vector3 keyboardInput(float speed) {
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        return new Vector3(x, 0, z);
    }

    // Перемещение камеры с помощью мыши
    Vector3 mouseInput(float speed) {
        float x = 0, z = 0;
        if (Input.mousePosition.x < ScrollZone)
            x -= speed;
        else if (Input.mousePosition.x > Screen.width - ScrollZone)
            x += speed;

        if (Input.mousePosition.y < ScrollZone)
            z -= speed;
        else if (Input.mousePosition.y > Screen.height - ScrollZone)
            z += speed;
 
        
        return new Vector3(x, 0, z);
    }

    void zoomCamera() {
        Vector3 localCameraPosition = cameraTransform.localPosition;
        if (Input.mouseScrollDelta.y > 0) {
            localCameraPosition.z += 5;
        } else if (Input.mouseScrollDelta.y < 0) {
            localCameraPosition.z -= 5;
        }
        localCameraPosition.z = Mathf.Clamp(localCameraPosition.z, ZoomMin, ZoomMax);
        cameraTransform.localPosition = localCameraPosition;
    }

    void Update() {
        // Скорость перемещеня камеры
        float speed = ScrollSpeed * Time.deltaTime;

        // Если разрешено перемещение с клавиатуры
        if (bUseKeyboard)
            MoveCamera(keyboardInput(speed));

        // Если разрешено перемещение с помощью мыши
        if (bUseMouse)
            MoveCamera(mouseInput(speed));
        
        zoomCamera();
        // Поворот камеры
        if (Input.GetMouseButton(2)) {
            float rotateSpeed = 3F;
            transform.Rotate(Vector3.up, rotateSpeed * Input.GetAxis("Mouse X"), Space.World);
        }       
    }
    // Перемещени камеры по х и z координате
    void MoveCamera(Vector3 moveDelta) {
        Vector3 move = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * moveDelta + desiredPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        move.z = Mathf.Clamp(move.z, zMin, zMax);
        desiredPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);
    }
}
