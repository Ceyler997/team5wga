using System;
using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour, IDeathObserver, IRadiusSubject {

    #region private fields

    private SphereCollider radiusCollider;
    private List<BaseObject> objectsInside;
    private Player owner;
    private bool isSettedUp;
    #endregion

    #region getters and setters

    public SphereCollider RadiusCollider { // TODO make private later
        get { return radiusCollider; }
        set { radiusCollider = value; }
    }

    public List<BaseObject> ObjectsInside {
        get { return objectsInside; }
        set { objectsInside = value; }
    }

    private bool IsSettedUp {
        get { return isSettedUp; }
        set { isSettedUp = value; }
    }

    public Player Owner {
        get { return owner; }
        set { owner = value; }
    }

    private List<IRadiusObserver> RadiusObservers { get; set; }
    #endregion

    #region MonoBehaviour methods

    public void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetType() == radiusCollider.GetType()) { // мы столкнулись с радиусом другого объекта
            return;
        }

        tryToAddObject(other.GetComponentInParent<BaseObject>()); // ищем объект в родителе т.к. мы столкнулись с самой моделью, а модель - сын объекта
    }

    void OnTriggerExit(Collider other) {
        if (other.GetType() == radiusCollider.GetType()) { // мы столкнулись с радиусом другого объекта
            return;
        }

        tryToRemoveObject(other.GetComponentInParent<BaseObject>()); // ищем объект в родителе т.к. мы столкнулись с самой моделью, а модель - сын объекта

    }
    #endregion

    #region private methods

    // пытается добавить объект в список
    private void tryToAddObject(BaseObject enteredObject) {

        if (enteredObject != null) {
            ObjectsInside.Add(enteredObject);

            if (enteredObject is IDeathSubject) { // подписываемся на смерть объекта если он смертный
                ((IDeathSubject) enteredObject).Attach(this);
            }

            foreach (IRadiusObserver observer in RadiusObservers) {
                observer.onObjectEnter(enteredObject);
            }
        }
    }

    // пытается убрать объект из списка
    private void tryToRemoveObject(BaseObject exitedObject) {
        if (exitedObject != null) {
            ObjectsInside.Remove(exitedObject);

            if(exitedObject is IDeathSubject) { // отписываемся от смерти объекта, если он смертный
                ((IDeathSubject) exitedObject).Detach(this);
            }

            foreach(IRadiusObserver observer in RadiusObservers) {
                observer.onObjectExit(exitedObject);
            }
        }
    }
    #endregion

    #region public methods

    public void setupSystem(float radiusSize, Player radiusOwner) {
        ObjectsInside = new List<BaseObject>();
        RadiusCollider = gameObject.AddComponent<SphereCollider>();
        RadiusCollider.isTrigger = true;
        RadiusCollider.radius = radiusSize;
        Owner = radiusOwner;
        IsSettedUp = true;

        RadiusObservers = new List<IRadiusObserver>();
    }
    #endregion

    #region IDeathObserver implementation

    public void onSubjectDeath(IDeathSubject subject) {
        if (subject is BaseObject) {
            tryToRemoveObject((BaseObject) subject);
        } else {
            throw new WrongDeathSubsciptionException();
        }
    }
    #endregion

    #region IRadiusSubject implementation

    public void Attach(IRadiusObserver observer) {
        RadiusObservers.Add(observer);
    }

    public void Detach(IRadiusObserver observer) {
        RadiusObservers.Remove(observer);
    }
    #endregion
}

public interface IRadiusObserver {
    void onObjectEnter(BaseObject enteredObject);
    void onObjectExit(BaseObject enteredObject);
}

public interface IRadiusSubject {
    void Attach(IRadiusObserver observer);
    void Detach(IRadiusObserver observer);
}