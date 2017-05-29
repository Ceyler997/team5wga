using System;
using UnityEngine;

[RequireComponent(typeof(Radius))]
public class BaseObject : Photon.PunBehaviour {

    #region private fields

    public  Player controllingPlayer; // игрок, который является владельцем объекта
    private float reactDistance; // на этом расстоянии происходит взаимодействие с объектом
    private Radius detectRadius; // радиус вокруг объекта, в котором будут видны объекты
    private bool isSettedUp; // для предотвращения использования ненастроенного объекта
    #endregion

    #region getters and setters

    public Player ControllingPlayer {
        get { return controllingPlayer; }

        protected set {
            controllingPlayer = value;
            DetectRadius.Owner = value;
        }
    }

    public float ReactDistance {
        get { return reactDistance; }
    }

    public Radius DetectRadius {
        get { return detectRadius; }
        set { detectRadius = value; }
    }

    private bool IsSettedUp {
        get { return isSettedUp; }
        set { isSettedUp = value; }
    }

    public Vector3 Position {
        get { return transform.position; }
    }

    public int ID { get; set; }
    #endregion

    #region MonoBehaviour methods

    public void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }
    #endregion

    #region protected methods

    protected void SetupBaseObject(Player controllingPlayer, float reactDistance, float detectRadius) {
        this.controllingPlayer = controllingPlayer;
        this.reactDistance = reactDistance;
        this.detectRadius = GetComponent<Radius>();
        this.detectRadius.SetupSystem(detectRadius, controllingPlayer);
        ID = photonView.viewID;
        IsSettedUp = true;
    }
    #endregion
}
