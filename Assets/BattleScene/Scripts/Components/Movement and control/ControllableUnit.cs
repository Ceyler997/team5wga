using UnityEngine;

[RequireComponent(typeof(Movement))]
public class ControllableUnit : Photon.PunBehaviour {

    #region fields

    public Projector selectionProjector;
    public Material FriendProjector;
    public Material EnemyProjector;
    #endregion

    #region getters and setters
    public Movement UnitMoveSystem { get; private set; }

    public Suprime Subject { get; set; }
    public BehaviourStates UnitsState { get; set; }
    #endregion

    #region MonoBehaviour methods

    // Use this for initialization
    void Start() {
        UnitMoveSystem = GetComponent<Movement>();
        Subject = GetComponent<Suprime>();

        if(selectionProjector == null) {
            throw new SystemIsNotSettedUpException();
        }

        if (photonView.isMine) {
            selectionProjector.material = FriendProjector;
            DeselectUnit();
        } else {
            selectionProjector.material = EnemyProjector;
            SelectUnit();
        }
    }
    #endregion

    #region public methods

    public void SelectUnit() {
        selectionProjector.enabled = true;
    }

    public void DeselectUnit() {
        selectionProjector.enabled = false;
    }
    #endregion
}
