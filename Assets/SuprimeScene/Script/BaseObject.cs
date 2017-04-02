using UnityEngine;

[RequireComponent(typeof(Radius))]
public class BaseObject : MonoBehaviour {

    #region private fields

    private Player controllingPlayer; // игрок, контролирующий данного супрайма
    private float followDistance; // расстояние, в пределах которого начинается точка следования
    private float alarmDistance; // расстояние, на котором враги должны быть атакованы
    private Radius unitsRadius; // радиус вокруг объекта, в котором будут видны IFightable объекты
    private bool isSettedUp;
    #endregion

    #region getters and setters

    public Player ControllingPlayer {
        get { return controllingPlayer; }

        set { controllingPlayer = value; }
    }

    public float FollowDistance {
        get { return followDistance; }

        set { followDistance = value; }
    }

    public float AlarmDistance {
        get { return alarmDistance; }

        set { alarmDistance = value; }
    }

    public Radius UnitsRadius{
        get { return unitsRadius; }

        set { unitsRadius = value; }
    }

    private bool IsSettedUp {
        get { return isSettedUp; }

        set { isSettedUp = value; }
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
        FollowDistance = followRadius;
        AlarmDistance = alarmRadius;
        UnitsRadius = GetComponent<Radius>();
        UnitsRadius.setupSystem(detectionRadius);
        IsSettedUp = true;
    }
    #endregion

    #region public methods

    public Vector3 Position() {
        return transform.position;
    }
    #endregion
}
