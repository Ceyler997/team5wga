
using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatRadius : IRadius {

    #region properties

    private IRadius Filling { get; set; }
    private IFightable CachedClosestEnemy { get; set; }
    #endregion

    #region constructors

    public CombatRadius(IRadius radiusToWrap) {
        Filling = radiusToWrap;
    }
    #endregion

    #region IRadius wrapping

    public List<BaseObject> ObjectsInside {
        get { return Filling.ObjectsInside; }
        set { Filling.ObjectsInside = value; }
    }

    public Player Owner {
        get { return Filling.Owner; }
        set { Filling.Owner = value; }
    }

    public void onSubjectDeath(IDeathSubject subject) {
        Filling.onSubjectDeath(subject);
        if (subject == CachedClosestEnemy) {
            clearCache();
        }
    }

    public void setupSystem(float radiusSize, Player radiusOwner) {
        Filling.setupSystem(radiusSize, radiusOwner);
    }

    public void updateEnemyList() {
        Filling.updateEnemyList();
    }

    public Vector3 getCenter() {
        return Filling.getCenter();
    }
    #endregion

    #region additional functionality

    // Проверяет, есть ли внутри враги
    public bool isEnemyInside() {
        return Filling.ObjectsInside.Count != 0;
    }

    // Возвращает ближайшего к центру врага. null при отсутствии врагов
    public IFightable getClosestUnit() {
        if (CachedClosestEnemy == null) {
            if (!isEnemyInside()) {
                return null;
            }

            Vector3 curCenter = Filling.getCenter();
            CachedClosestEnemy = null;
            float distToClosestEnemy = -1.0f;

            foreach (IFightable enemy in Filling.ObjectsInside) {
                float distToEnemy = Vector3.Distance(curCenter, enemy.Position);

                if (distToEnemy < distToClosestEnemy || distToClosestEnemy == -1.0f) {
                    CachedClosestEnemy = enemy;
                    distToClosestEnemy = distToEnemy;
                }
            }
        }

        return CachedClosestEnemy;
    }

    // Очищает кеш ближайшего врага, стоит вызывать в LateUpdate
    public void clearCache() {
        CachedClosestEnemy = null;
    }
    #endregion
}
