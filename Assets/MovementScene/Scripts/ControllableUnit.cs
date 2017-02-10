using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllableUnit : BaseUnit {

    public enum UnitStates
    {
        IDLE,
        WALKING
    }

    public UnitStates UnitState = UnitStates.IDLE;

    public Transform Target;

    public NavMeshAgent mAgent;
    public NavMeshObstacle mObstacle;
    public bool isCameraLockedOn = false;

    Renderer[] mRenderers;
    HighlightsFX highlight;

    // Use this for initialization
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mObstacle = GetComponent<NavMeshObstacle>();
        mRenderers = GetComponentsInChildren<Renderer>();
        highlight = Camera.main.GetComponent<HighlightsFX>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (UnitState)
        {
            case UnitStates.WALKING:
                //mObstacle.enabled = false;
                //mAgent.enabled = true;
                break;

            case UnitStates.IDLE:
                //mAgent.enabled = false;
                //mObstacle.enabled = true;
                break;
        }

        if (mAgent.pathStatus == NavMeshPathStatus.PathComplete && UnitState == UnitStates.WALKING)
        {
            UnitState = UnitStates.IDLE;

        }
    }

    public void SelectUnit()
    {
        foreach (Renderer rend in mRenderers)
            highlight.ObjectRenderers.Add(rend);
        IsSelected = true;
        //highlight.ObjectRenderers.AddRange();
    }

    public void DeselectUnit()
    {
        highlight.ObjectRenderers.Clear();
        IsSelected = false;
    }

    public void MoveTo(Vector3 position)
    {
        UnitState = UnitStates.WALKING;
        mObstacle.enabled = false;
        mAgent.enabled = true;
        Target.position = position;
        mAgent.SetDestination(Target.position);
    }
}
