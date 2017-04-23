using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRadiusObserver {
    void onObjectEnter(BaseObject enteredObject);
    void onObjectExit(BaseObject enteredObject);
}

public interface IRadiusSubject {
    void Attach(IRadiusObserver observer);
    void Detach(IRadiusObserver observer);
}

public class Radius : MonoBehaviour, IDeathObserver, IRadiusSubject {

    #region private fields

    private SphereCollider radiusCollider;
    public List<BaseObject> objectsInside;
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
        // столкновение с радиусом другого объекта
        if (other.GetType() == radiusCollider.GetType()) { 
            return;
        }
        // ищем объект
        tryToAddObject(other.GetComponentInParent<BaseObject>()); 
    }

    void OnTriggerExit(Collider other) {
        // покидание радиуса другого объекта
        if (other.GetType() == radiusCollider.GetType()) {
            return;
        }
        // ищем объект в родителе 
        tryToRemoveObject(other.GetComponentInParent<BaseObject>()); 

    }
    #endregion

    #region private methods


    private void tryToAddObject(BaseObject enteredObject) {
        // добавляем объект в список
        if (enteredObject != null) {
            ObjectsInside.Add(enteredObject);
            // подписываемся на смерть объекта если он смертный
            if (enteredObject is IDeathSubject) { 
                ((IDeathSubject) enteredObject).Attach(this);
            }
            // рассылаем уведомления
            foreach (IRadiusObserver observer in RadiusObservers) {
                observer.onObjectEnter(enteredObject);
            }
        }
    }

    // убраем объект из списка
    private void tryToRemoveObject(BaseObject exitedObject) {
        if (exitedObject != null) {
            ObjectsInside.Remove(exitedObject);

            // отписываемся от смерти объекта, если он смертный
            if (exitedObject is IDeathSubject) { 
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

    public bool HasEnemies() {
        foreach(BaseObject objectInside in ObjectsInside) {
            if(objectInside.ControllingPlayer != Owner) {
                return true;
            }
        }

        return false;
    }

    public bool HasFriends() {
        foreach (BaseObject objectInside in ObjectsInside) {
            if (objectInside.ControllingPlayer == Owner) {
                return true;
            }
        }

        return false;
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

