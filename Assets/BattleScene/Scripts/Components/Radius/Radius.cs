using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour, IRadius {

    #region private fields

    private SphereCollider radiusCollider;
    public List<BaseObject> objectsInside; // TODO make private later
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
    #endregion

    #region MonoBehaviour methods
    
    public void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other is SphereCollider) { // если мы столкнулись с SphereCollider - мы столкнулись с радиусом другого объекта
            return;
        }

        tryToAddEnemy(other.GetComponentInParent<BaseObject>()); // ищем объект в родителе т.к. мы столкнулись с самой моделью, а модель - сын объекта
    }

    void OnTriggerExit(Collider other) {
        if (other is SphereCollider) { // если мы столкнулись с SphereCollider - мы столкнулись с радиусом другого объекта
            return;
        }

        tryToRemoveEnemy(other.GetComponentInParent<BaseObject>()); // ищем объект в родителе т.к. мы столкнулись с самой моделью, а модель - сын объекта

    }
    #endregion

    #region private methods

    // пытается добавить объект в список
    private void tryToAddEnemy(BaseObject enteredObject) {

        if (enteredObject != null) {
            ObjectsInside.Add(enteredObject);

            if (enteredObject is IDeathSubject) { // подписываемся на смерть объекта если он смертный
                ((IDeathSubject) enteredObject).Attach(this);
            }
        }
    }

    // пытается убрать объект из списка
    private void tryToRemoveEnemy(BaseObject exitedObject) {
        if (exitedObject != null) {
            ObjectsInside.Remove(exitedObject);

            if(exitedObject is IDeathSubject) { // отписываемся от смерти объекта, если он смертный
                ((IDeathSubject) exitedObject).Detach(this);
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
    }

    // Ещё не тестировался
    // ОСТОРОЖНО, ТЯЖЁЛЫЙ МЕТОД
    public void updateEnemyList() {
        Collider[] objectsInside = new Collider[GameConf.maxObjectsInsideRadiusAmount];
        Physics.OverlapSphereNonAlloc(transform.position, RadiusCollider.radius, objectsInside);
        ObjectsInside = new List<BaseObject>();

        foreach (Collider other in objectsInside) {
            if (other != null) {
                tryToAddEnemy(other.GetComponentInParent<BaseObject>());
            }
        }
    }

    public Vector3 getCenter() {
        return transform.position;
    }
    #endregion

    #region IDeathObserver implementation

    public void onSubjectDeath(IDeathSubject subject) {
        if (subject is BaseObject) {
            tryToRemoveEnemy((BaseObject) subject);
        } else {
            throw new WrongDeathSubsciptionException();
        }
    }
    #endregion
}
