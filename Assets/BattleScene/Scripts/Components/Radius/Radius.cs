using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRadiusObserver {
    void OnObjectEnter(BaseObject enteredObject);
    void OnObjectExit(BaseObject enteredObject);
}

public interface IRadiusSubject {
    void Attach(IRadiusObserver observer);
    void Detach(IRadiusObserver observer);
}

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
        // столкновение с радиусом другого объекта
        if (other.GetType() == radiusCollider.GetType()) { 
            return;
        }
        // ищем объект
        TryToAddObject(other.GetComponentInParent<BaseObject>()); 
    }

    void OnTriggerExit(Collider other) {
        // покидание радиуса другого объекта
        if (other.GetType() == radiusCollider.GetType()) {
            return;
        }
        // ищем объект в родителе 
        TryToRemoveObject(other.GetComponentInParent<BaseObject>()); 

    }
    #endregion

    #region private methods


    private void TryToAddObject(BaseObject enteredObject) {
        // добавляем объект в список
        if (enteredObject != null) {
            ObjectsInside.Add(enteredObject);
            // подписываемся на смерть объекта если он смертный
            if (enteredObject is IDeathSubject) { 
                ((IDeathSubject) enteredObject).Attach(this);
            }
            // рассылаем уведомления
            foreach (IRadiusObserver observer in RadiusObservers) {
                observer.OnObjectEnter(enteredObject);
            }
        }
    }

    // убраем объект из списка
    private void TryToRemoveObject(BaseObject exitedObject) {
        if (exitedObject != null) {
            ObjectsInside.Remove(exitedObject);

            // отписываемся от смерти объекта, если он смертный
            if (exitedObject is IDeathSubject) { 
                ((IDeathSubject) exitedObject).Detach(this);
            }

            foreach(IRadiusObserver observer in RadiusObservers) {
                observer.OnObjectExit(exitedObject);
            }
        }
    }
    #endregion

    #region public methods

    public void SetupSystem(float radiusSize, Player radiusOwner) {
        ObjectsInside = new List<BaseObject>();
        RadiusCollider = gameObject.AddComponent<SphereCollider>();
        RadiusCollider.isTrigger = true;
        RadiusCollider.radius = radiusSize;
        RadiusCollider.center = Vector3.zero;
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

    public void OnSubjectDeath(IDeathSubject subject) {
        if (subject is BaseObject) {
            TryToRemoveObject((BaseObject) subject);
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

