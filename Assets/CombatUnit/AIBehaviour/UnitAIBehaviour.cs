using UnityEngine;

abstract public class UnitAIBehaviour {
    private Unit subject;

    public UnitAIBehaviour(Unit subject) {
        Subject = subject;
    }

    protected Unit Subject {
        get {return subject;}

        set {subject = value;}
    }

    abstract public void UpdateState();

    protected IFightable getClosestUnit() { // getting closest to subject unit
        if(Subject.Inside.Length == 0) { // check, if there is a units Inside
            throw new NoObjectsInsideRadiusException(); // exception, cause this function should not be called in the empty array
        }

        IFightable closestUnit = (IFightable) Subject.Inside[0]; // TODO remove cast after radius setting up
        Vector3 subjectPosition = Subject.transform.position;
        float minDistance = Vector3.Distance(subjectPosition, Subject.Inside [0].getPosition()); // setting min distance to first unit Inside

        foreach(IFightable baseUnit in Subject.Inside) { // baseUnit is unit or suprime
            float distanceToUnit = Vector3.Distance(subjectPosition, baseUnit.getPosition());

            if(distanceToUnit < minDistance) {
                closestUnit = baseUnit;
                minDistance = distanceToUnit;
            }
        }

        return closestUnit;
    }
}
