using System.Collections.Generic;
using UnityEngine;

public interface IRadius : IDeathObserver {
    #region properties
    
    List<BaseObject> ObjectsInside { get; set; }
    Player Owner { get; set; }
    #endregion

    #region methods

    void setupSystem(float radiusSize, Player radiusOwner); // передавать null как владельца при нейтральном объекте
    void updateEnemyList(); // обновляет список врагов, находящихся внутри
    Vector3 getCenter(); // возвращает центр радиуса

    #endregion
}
