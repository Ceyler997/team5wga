using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour {
    // Скорость передвижения юнита может быть изменена в Nav Mesh Agent in the Unity Redactor

    #region private fields

    NavMeshAgent navigationAgent;
    #endregion

    #region MonoBehaviour methods

    void Start() {
        navigationAgent = GetComponent<NavMeshAgent>();
    }
    #endregion

    #region public methods

    // Moving to the point
    public void moveTo(Vector3 targetPosition) {
        navigationAgent.SetDestination(targetPosition);
    }

    // stopping movement
    public void stop() {
        navigationAgent.Stop();
    }
    #endregion
}
