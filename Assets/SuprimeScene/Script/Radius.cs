using System;
using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour, IDeathObserver{

    #region private fields

    private SphereCollider radiusCollider;
    public List<IFightable> enemyList;
    private Player owner;
    private bool isSettedUp;

    private IFightable cachedClosestEnemy; // для оптимизации запроса ближайшего юнита
    #endregion

    #region getters and setters

    private SphereCollider RadiusCollider {
        get { return radiusCollider; }

        set { radiusCollider = value; }
    }

    public List<IFightable> EnemyList {
        get { return enemyList; }

        set { enemyList = value; }
    }

    private bool IsSettedUp {
        get { return isSettedUp; }

        set { isSettedUp = value; }
    }

    public Player Owner {
        get { return owner; }

        set { owner = value; }
    }

    public IFightable CachedClosestEnemy {
        get { return cachedClosestEnemy; }

        set { cachedClosestEnemy = value; }
    }
    #endregion

    #region MonoBehaviour methods
    
    public void Update() {
        if (!IsSettedUp) {
            throw new SystemIsNotSettedUpException();
        }
    }

    public void LateUpdate() {
        CachedClosestEnemy = null; // обновляет кеш между Update
    }

    void OnTriggerEnter(Collider other) {
        if (other is SphereCollider) { // если мы столкнулись с SphereCollider - мы столкнулись с радиусом другого объекта
            return;
        }

        tryToAddEnemy(other.GetComponentInParent<IFightable>()); // ищем объект в родителе т.к. мы столкнулись с самой моделью, а модель - сын объекта
    }

    void OnTriggerExit(Collider other) {
        if (other is SphereCollider) { // если мы столкнулись с SphereCollider - мы столкнулись с радиусом другого объекта
            return;
        }

        tryToRemoveEnemy(other.GetComponentInParent<IFightable>()); // ищем объект в родителе т.к. мы столкнулись с самой моделью, а модель - сын объекта

    }
    #endregion

    #region private methods

    // пытается добавить объект в список
    private void tryToAddEnemy(IFightable enteredObject) {
        if (enteredObject != null && ((BaseObject) enteredObject).ControllingPlayer != owner) {
            EnemyList.Add(enteredObject);
            enteredObject.Attach(this);
        }
    }

    // пытается убрать объект из списка
    private void tryToRemoveEnemy(IFightable exitedObject) {
        if (exitedObject != null && ((BaseObject) exitedObject).ControllingPlayer != owner) {
            if (EnemyList.Remove(exitedObject) 
                && CachedClosestEnemy == exitedObject) {
                CachedClosestEnemy = null;
            }
            exitedObject.Detach(this);
        }
    }
    #endregion

    #region public methods

    public void setupSystem(float radiusSize, Player radiusOwner) { // передавать null как владельца при нейтральном объекте
        EnemyList = new List<IFightable>();
        RadiusCollider = gameObject.AddComponent<SphereCollider>();
        RadiusCollider.isTrigger = true;
        RadiusCollider.radius = radiusSize;
        Owner = radiusOwner;
        IsSettedUp = true;
    }

    // Проверяет, есть ли внутри враги
    public bool isEnemyInside() {
        return EnemyList.Count != 0;
    }

    // Возвращает ближайшего к центру врага. НЕ ВЫЗЫВАТЬ ПРИ ПУСТОМ СПИСКЕ
    // Метод не возвращает null
    public IFightable getClosestUnit() {
        if(CachedClosestEnemy == null) {
            if (!isEnemyInside()) {
                throw new NoUnitsInsideRadiusException();
            }

            Vector3 curCenter = transform.position;
            CachedClosestEnemy = EnemyList [0];
            float distToClosestEnemy = Vector3.Distance(curCenter, CachedClosestEnemy.Position);

            foreach (IFightable enemy in EnemyList) {
                float distToEnemy = Vector3.Distance(curCenter, enemy.Position);

                if (distToEnemy < distToClosestEnemy) {
                    CachedClosestEnemy = enemy;
                    distToClosestEnemy = distToEnemy;
                }
            }
        }

        return CachedClosestEnemy;
    }

    // обновляет список врагов, находящийся внутри
    // ОСТОРОЖНО, ТЯЖЁЛЫЙ МЕТОД
    public void UpdateEnemyList() {
        Collider[] objectsInside = new Collider[GameConf.maxObjectsInsideRadiusAmount];
        Physics.OverlapSphereNonAlloc(transform.position, RadiusCollider.radius, objectsInside);
        EnemyList = new List<IFightable>();

        foreach (Collider other in objectsInside) {
            tryToAddEnemy(other.GetComponentInParent<IFightable>());
        }
    }
    #endregion

    #region IDeathObserver implementation

    public void onSubjectDeath(IFightable subject) {
        tryToRemoveEnemy(subject);
    }
    #endregion
}
