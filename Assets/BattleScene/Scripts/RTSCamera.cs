using UnityEngine;

public class RTSCamera : MonoBehaviour {

    public float ScrollZone = 30;//Размер границы края экрана для перемещения камеры
    public float ScrollSpeed = 5; //Скорость приближения
    public float RotateSpeed = 3; //Скорость вращения
    public float MoveSpeed = 10; // Скорость перемещения

    public float xMax;// Максимальное смещение по оси X
    public float xMin;// Минимальное смещение по оси X
    public float zMax;// Максимальное смещение по оси Z
    public float zMin;// Минимальное смещение по оси Z

    public float ZoomMin;//Минимальное значение приближения
    public float ZoomMax;//Максимальное значение приближения

    public bool keyboardAllowed = false; //Использование клавиатуры для пермещения
    public bool mouseAllowed = true; //Использование клавиатуры для пермещения   
    
    public bool IsMoved { get; private set; }
    
    private Transform cameraTransform;

    public static RTSCamera Instance { get; set; }
    public Vector3 Position {
        get { return transform.position; }
    }

    #region MonoBehaviour methods

    void Start() {
        Instance = this;
        cameraTransform = transform.GetChild(0).GetComponent<Transform>();
    }

    void Update() {
        // Скорость перемещеня камеры
        float speed = ScrollSpeed * Time.deltaTime;

        // Если разрешено перемещение с клавиатуры
        if (keyboardAllowed) {
            MoveCamera(KeyboardInput(speed));
        }
        // Если разрешено перемещение с помощью мыши и не передвигали с клавиатуры
        if (mouseAllowed && !IsMoved) {
            MoveCamera(MouseInput(speed));
        }

        ZoomCamera();
        // Поворот камеры
        if (Input.GetMouseButton(2)) {
            float rotateSpeed = 3F;
            transform.Rotate(Vector3.up, rotateSpeed * Input.GetAxis("Mouse X"), Space.World);
        }
    }
    #endregion

    #region public methods

    // Перемещение камеры по х и z координате
    public void MoveCamera(Vector3 moveDelta) {
        moveDelta.y = 0;
        moveDelta = Vector3.ClampMagnitude(moveDelta, MoveSpeed);
        if (!moveDelta.Equals(Vector3.zero)) {
            IsMoved = true;
            Vector3 move = moveDelta + transform.position;
            move.x = Mathf.Clamp(move.x, xMin, xMax);
            move.z = Mathf.Clamp(move.z, zMin, zMax);
            transform.position = move;
        } else {
            IsMoved = false;
        }
    }
    #endregion

    #region Input methods

    // Управление камерой с клавиатуры
    Vector3 KeyboardInput(float speed) {
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        return RotateByScreen(new Vector3(x, 0, z));
    }

    // Перемещение камеры с помощью мыши
    Vector3 MouseInput(float speed) {
        float x = 0, z = 0;
        if (Input.mousePosition.x < ScrollZone)
            x -= speed;
        else if (Input.mousePosition.x > Screen.width - ScrollZone)
            x += speed;

        if (Input.mousePosition.y < ScrollZone)
            z -= speed;
        else if (Input.mousePosition.y > Screen.height - ScrollZone)
            z += speed;


        return RotateByScreen(new Vector3(x, 0, z));
    }

    private Vector3 RotateByScreen(Vector3 moveDelta) {
        return transform.rotation * moveDelta;
    }
    #endregion

    #region Move methods

    void ZoomCamera() {
        Vector3 localCameraPosition = cameraTransform.localPosition;

        if (Input.mouseScrollDelta.y > 0) {
            localCameraPosition.z += 5;
        } else if (Input.mouseScrollDelta.y < 0) {
            localCameraPosition.z -= 5;
        }

        localCameraPosition.z = Mathf.Clamp(localCameraPosition.z, ZoomMin, ZoomMax);
        cameraTransform.localPosition = localCameraPosition;
    }
    #endregion
}
