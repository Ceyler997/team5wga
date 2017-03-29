using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour {
    // Скорость передвижения юнита может быть изменена в Nav Mesh Agent in the Unity Redactor

    private static readonly float DESTINATION_EPS = 3.0f;

    #region private fields

    private NavMeshAgent navigationAgent;
    #endregion

    #region getters and setters

    private NavMeshAgent NavigationAgent {
        get {return navigationAgent;}

        set {navigationAgent = value;}
    }
    #endregion

    #region MonoBehaviour methods

    void Start() {
        NavigationAgent = GetComponent<NavMeshAgent>();
    }
    #endregion

    #region public methods

    // Moving to the point
    public void moveTo(Vector3 targetPosition) {
        NavigationAgent.SetDestination(targetPosition);
    }

    //Follow the target
    public void follow(BaseObject target) {
        if (NavigationAgent.remainingDistance < DESTINATION_EPS) { // Can be improved with target movement interpolation
            Vector2 shift = Random.insideUnitCircle * target.FollowRadius;            
            moveTo(target.getPosition() + new Vector3(shift.x, 0, shift.y));
        }
    }

    // stopping movement
    public void stop() {
        NavigationAgent.Stop();
    }
    #endregion
}
