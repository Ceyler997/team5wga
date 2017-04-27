using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour {

    private static readonly float DESTINATION_EPS = 3.0f; // дистанция, при которой считается, что цель достигнута
    private static readonly float SPEED_EPS = 2f; // скорость, при которой считается, что цель достигнута

    #region private fields

    private NavMeshAgent navigationAgent; // агент от юнити
    private bool isSettedUp;
    #endregion

    #region getters and setters

    private NavMeshAgent NavigationAgent {
        get {return navigationAgent;}
        set {navigationAgent = value;}
    }

    public bool IsFinishedMovement {
        get { return NavigationAgent.remainingDistance < DESTINATION_EPS || NavigationAgent.velocity.magnitude < SPEED_EPS; }
    }

    public bool IsSettedUp {
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

    #region public methods

    // Передвижение к точке
    public void MoveTo(Vector3 targetPosition) {
        NavigationAgent.SetDestination(targetPosition);
    }

    // Следование за целью
    public void Follow(BaseObject target) { // TODO Can be improved with target movement interpolation
        // Если мы пришли к точке, взять новую в окружности радиусом FollowRadius вокруг цели
        if (IsFinishedMovement || Vector3.Distance(target.Position, NavigationAgent.destination) > target.ReactDistance) {
            Vector2 shift = Random.insideUnitCircle * target.ReactDistance;
            MoveTo(target.Position + new Vector3(shift.x, 0, shift.y));
        }
    }

    // Остановка движения сбросом пути
    // ВНИМАНИЕ follow будет считать, что цель достигнута и возьмёт новую точку
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
