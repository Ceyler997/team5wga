using UnityEngine;

public class BaseObject : MonoBehaviour {
    private Player player;
    private float followRadius;
    private IFightable [] radiusStub;

    public Vector3 getPosition() {
        return transform.position;
    }

    public Player Player {
        get {
            return player;
        }

        set {
            player = value;
        }
    }

    public float FollowRadius {
        get { return followRadius; }

        set { followRadius = value; }
    }

    public IFightable [] RadiusStub {
        get { return radiusStub; }

        set { radiusStub = value; }
    }

    public IFightable getClosestUnitStub() { // getting closest to subject unit
        if (RadiusStub.Length == 0) { // check, if there is a units Inside
            throw new NoObjectsInsideRadiusException(); // exception, cause this function should not be called in the empty array
        }

        IFightable closestUnit = RadiusStub [0];
        Vector3 subjectPosition = transform.position;
        float minDistance = Vector3.Distance(subjectPosition, RadiusStub [0].getPosition()); // setting min distance to first unit Inside

        foreach (IFightable baseUnit in RadiusStub) { // baseUnit is unit or suprime
            if (!baseUnit.getHealthSystem().IsDead) { // Don't take dead targets
                float distanceToUnit = Vector3.Distance(subjectPosition, baseUnit.getPosition());

                if (distanceToUnit < minDistance) {
                    closestUnit = baseUnit;
                    minDistance = distanceToUnit;
                }
            }
        }

        return closestUnit;
    }
}
