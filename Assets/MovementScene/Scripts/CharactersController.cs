using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour {

    public static CharactersController Instance;

    public ControllableUnit SelectedUnit;
    public bool FinishedDragOnThisFrame;
    public bool UserIsDragging;


    // Use this for initialization
    void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update() {


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Unit"))
            {
                ControllableUnit unit = hit.transform.GetComponent<ControllableUnit>();
                unit.SelectUnit();
                SelectedUnit = unit;
            }
            else
            {
                if(SelectedUnit != null)
                {
                    SelectedUnit.DeselectUnit();
                    SelectedUnit = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Ground") && SelectedUnit != null)
            {
                SelectedUnit.MoveTo(hit.point);
            }
        }
    }

    //public static bool UnitInsideDrag(Vector2 UnitInScreenPos)
    //{
    //    if (UnitInScreenPos.x > GUISelectorBox.BoxStart.x && UnitInScreenPos.y < GUISelectorBox.BoxStart.y
    //        && UnitInScreenPos.x < GUISelectorBox.BoxFinish.x && UnitInScreenPos.y > GUISelectorBox.BoxFinish.y)
    //        return true;
    //    else return false;

    //}
}
