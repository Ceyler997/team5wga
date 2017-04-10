using UnityEngine;

[RequireComponent(typeof(Radius))]
public class BaseObject : MonoBehaviour {

    #region private fields

    private Player controllingPlayer; // игрок, который является владельцем объекта
    private float reactDistance; // на этом расстоянии происходит взаимодействие с объектом
    private Radius detectRadius; // радиус вокруг объекта, в котором будут видны объекты
    private bool isSettedUp;
    #endregion

    #region getters and setters

    public Player ControllingPlayer {
        get { return controllingPlayer; }

        set {
            controllingPlayer = value;
            DetectRadius.Owner = value;
            DetectRadius.UpdateEnemyList();
        }
    }

    public float ReactDistance {
        get { return reactDistance; }
    }

    public Radius DetectRadius {
        get { return detectRadius; }
    }

    private bool IsSettedUp {
        get { return isSettedUp; }

        set { isSettedUp = value; }
    }

    public Vector3 Position {
        get { return transform.position; }
    }
    #endregion

    #region MonoBehaviour methods

    public void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }
    #endregion

    #region protected methods

    protected void setupBaseObject(Player controllingPlayer, float reactDistance, float detectRadius) {
        this.controllingPlayer = controllingPlayer;
        this.reactDistance = reactDistance;
        this.detectRadius = GetComponent<Radius>();
        this.detectRadius.setupSystem(detectRadius, controllingPlayer);
        IsSettedUp = true;
    }
    #endregion
}
