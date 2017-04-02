using UnityEngine;

[RequireComponent(typeof(Radius))]
public class BaseObject : MonoBehaviour {

    #region private fields

    private Player controllingPlayer; // игрок, контролирующий данного супрайма
    private float followDistance; // расстояние, в пределах которого начинается точка следования
    private float alarmDistance; // расстояние, на котором враги должны быть атакованы
    private Radius detectRadius; // радиус вокруг объекта, в котором будут видны IFightable объекты
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

    public float FollowDistance {
        get { return followDistance; }
    }

    public float AlarmDistance {
        get { return alarmDistance; }
    }

    public Radius DetectRadius{
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

    protected void setupBaseObject (Player controllingPlayer, float followRadius, float alarmRadius, float detectionRadius){
        ControllingPlayer = controllingPlayer;
        followDistance = followRadius;
        alarmDistance = alarmRadius;
        detectRadius = GetComponent<Radius>();
        DetectRadius.setupSystem(detectionRadius, controllingPlayer);
        IsSettedUp = true;
    }
    #endregion
}
