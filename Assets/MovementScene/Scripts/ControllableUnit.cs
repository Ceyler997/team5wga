using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Movement))]
public class ControllableUnit : MonoBehaviour {

    #region private fields
    
    private Movement unitMoveSystem;
    public Projector selectionProjector;
    
    private bool isHighlighted = false;

    private Renderer [] mRenderers;
    private HighlightsFX highlight;
    #endregion

    #region getters and setters

    private Projector SelectionProjector {
        get { return selectionProjector; }
        set { selectionProjector = value; }
    }

    private Renderer [] MRenderers {
        get { return mRenderers; }
        set { mRenderers = value; }
    }

    private HighlightsFX Highlight {
        get { return highlight; }
        set { highlight = value; }
    }

    public Movement UnitMoveSystem {
        get { return unitMoveSystem; }
        set { unitMoveSystem = value; }
    }

    private bool IsHighlighted {
        get { return isHighlighted; }
        set { isHighlighted = value; }
    }
    #endregion

    #region MonoBehaviour methods

    // Use this for initialization
    void Start() {
        UnitMoveSystem = GetComponent<Movement>();
        MRenderers = GetComponentsInChildren<Renderer>();
        Highlight = Camera.main.GetComponent<HighlightsFX>();
    }

    private void OnMouseOver() {
        print("Highlighted");
        HighLight(true);
    }

    private void OnMouseExit() {
        print("Unhighlighted");
        HighLight(false);
    }
    #endregion

    #region public methods

    public void selectUnit() {
        SelectionProjector.enabled = true;
    }

    public void DeselectUnit() {
        SelectionProjector.enabled = false;
    }
    #endregion

    #region private methods

    private void HighLight(bool state) {
        if (state) {
            if (IsHighlighted)
                return;

            foreach (Renderer rend in MRenderers)
                Highlight.ObjectRenderers.Add(rend);

            IsHighlighted = true;
        } else {
            Highlight.ObjectRenderers.Clear();
            IsHighlighted = false;
        }

    }
    #endregion

}
