using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour {

    private static readonly float DESTINATION_EPS = 3.0f; // дистанция, при которой считается, что цель достигнута
    private static readonly float SPEED_EPS = 2f; // скорость, при которой считается, что цель достигнута

    #region getters and setters

    private NavMeshAgent NavigationAgent { get; set; }
    private bool IsSettedUp { get; set; }

    public bool IsFinishedMovement {
        get {
            return NavigationAgent.remainingDistance < DESTINATION_EPS 
                || NavigationAgent.velocity.magnitude < SPEED_EPS;
        }
    }

    public Vector3 Destination {
        get { return NavigationAgent.destination; }
    }

    #endregion

    #region MonoBehaviour methods

    public void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }
    #endregion

    #region public methods

    // Передвижение к точке
    public void MoveTo(Vector3 targetPosition) {
        NavigationAgent.SetDestination(targetPosition);
    }

    // Остановка движения сбросом пути
    // ВНИМАНИЕ! follow будет считать, что цель достигнута и возьмёт новую точку
    public void Stop() {
        NavigationAgent.ResetPath();
    }

    public void SetupSystem(float maxSpeed) {
        NavigationAgent = GetComponent<NavMeshAgent>();
        NavigationAgent.speed = maxSpeed;
        NavigationAgent.stoppingDistance = DESTINATION_EPS;
        IsSettedUp = true;
    }
    #endregion
}
