using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour {

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
        tryToAddEnemy(other);
    }

    void OnTriggerExit(Collider other) {
        if (other.GetComponent<Player>() != owner) {
            IFightable exitedEnemy = other.GetComponent<IFightable>();

            if (exitedEnemy != null) {
                EnemyList.Remove(exitedEnemy);
            }
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
            tryToAddEnemy(other);
        }
    }
    #endregion

    // пытается добавить врага в список по коллайдеру
    #region private methods
    private void tryToAddEnemy(Collider other) {
        if (other.GetComponent<Player>() != owner) {
            IFightable enteredEnemy = other.GetComponent<IFightable>();

            if (enteredEnemy != null) {
                EnemyList.Add(enteredEnemy);
            }
        }
    }
    #endregion
}
