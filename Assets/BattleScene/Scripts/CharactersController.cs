using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour {

    public static CharactersController Instance;

    public LayerMask MoveRaycastMask;
    public LayerMask SelectableMask;
    public ControllableUnit SelectedUnit;
    public bool FinishedDragOnThisFrame;
    public bool UserIsDragging;

    private bool IsFollowing;

    // Use this for initialization
    void Awake() {
        Instance = this;
        MoveRaycastMask = 1 << 14;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonDown(0)) { // Left button
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {

                ControllableUnit unit = hit.transform.GetComponentInParent<ControllableUnit>();
                if (unit != null && (unit.photonView.isMine || !PhotonNetwork.connected)) {
                    if (SelectedUnit != null) {
                        SelectedUnit.DeselectUnit();
                    }

                    unit.SelectUnit();
                    SelectedUnit = unit;
                }
            } else {
                if (SelectedUnit != null) {
                    SelectedUnit.DeselectUnit();
                    SelectedUnit = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) { // Roght button
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, MoveRaycastMask) && SelectedUnit != null) {
                SelectedUnit.UnitMoveSystem.MoveTo(hit.point);
            }
        }

        CameraFollowSelectedUnit();
    }

    // Следование камеры за выбранным юнитом
    private void CameraFollowSelectedUnit() {
        if (IsFollowing == false) {
            return;
        }
        if (SelectedUnit == null) {
            IsFollowing = false;
            return;
        }
        if (RTSCamera.Instance.IsMoved) {
            IsFollowing = false;
            return;
        }

        Vector3 moveDelta = SelectedUnit.transform.position - RTSCamera.Instance.Position;
        RTSCamera.Instance.MoveCamera(moveDelta);
    }

    public void SelectUnit(ControllableUnit unit) {
        if (SelectedUnit != null) {
            SelectedUnit.DeselectUnit();
        }

        SelectedUnit = unit;
        unit.SelectUnit();

        IsFollowing = true;
    }

    //public static bool UnitInsideDrag(Vector2 UnitInScreenPos)
    //{
    //    if (UnitInScreenPos.x > GUISelectorBox.BoxStart.x && UnitInScreenPos.y < GUISelectorBox.BoxStart.y
    //        && UnitInScreenPos.x < GUISelectorBox.BoxFinish.x && UnitInScreenPos.y > GUISelectorBox.BoxFinish.y)
    //        return true;
    //    else return false;

    //}
}
