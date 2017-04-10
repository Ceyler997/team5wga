
using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatRadius : IRadius {

    #region properties

    private IRadius Filling { get; set; }
    private IFightable CachedClosestEnemy { get; set; }
    private bool IsCheckedInUpdate { get; set; }
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
        if (!IsCheckedInUpdate) {
            getClosestEnemy();
        }

        return (CachedClosestEnemy != null);
    }

    // Возвращает ближайшего к центру врага. null при отсутствии врагов
    public IFightable getClosestEnemy() {
        if (!IsCheckedInUpdate) {

            Vector3 curCenter = Filling.getCenter();
            CachedClosestEnemy = null;
            float distToClosestEnemy = 0;

            foreach (BaseObject objectInside in Filling.ObjectsInside) {
                if (objectInside is IFightable && objectInside.ControllingPlayer != Owner) {
                    float distToEnemy = Vector3.Distance(curCenter, objectInside.Position);

                    if (distToEnemy < distToClosestEnemy || distToClosestEnemy == 0) {
                        CachedClosestEnemy = (IFightable) objectInside;
                        distToClosestEnemy = distToEnemy;
                    }
                }
            }

            IsCheckedInUpdate = true;
        }

        return CachedClosestEnemy;
    }

    // Очищает кеш ближайшего врага, стоит вызывать в LateUpdate
    public void clearCache() {
        CachedClosestEnemy = null;
        IsCheckedInUpdate = false;
    }
    #endregion
}
