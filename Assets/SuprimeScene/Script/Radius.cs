using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Все кто реализуют интерфейс и компонент класса, будут получать информацию о врагах
interface IEnemyChecker {
    void EnemyCheck(BaseObject enemy);
}

public class Radius : MonoBehaviour {
    public SphereCollider radius; // Размер области видимости
    public List<BaseObject> enemyList; // Список врагов, находящихся в области кристалла
    BaseObject owner = null; // Объект у которого есть радиус

    // Конструктор кристалла 
    public void Initialization (float radiusSize, BaseObject ownerObject) {
        enemyList = new List<BaseObject>();
        radius = gameObject.AddComponent<SphereCollider>();
		radius.radius = radiusSize; 	
		radius.isTrigger = true;
        owner = ownerObject;
    }
    // Если враг зашел в область видимости
	void OnTriggerEnter(Collider other) {
        BaseObject enemy = other.GetComponent<BaseObject>();
        if(enemy != null && other.GetType() != radius.GetType()) {
            if(enemy != owner) {
                enemyList.Add(enemy);
                owner.EnemyCheck(enemy);
            }
        }
    }
    // Враг покидает область видимости
	void OnTriggerExit(Collider other) {
		BaseObject enemy = other.GetComponent<BaseObject>();
        if(enemy != null && other.GetType() != radius.GetType()) {
            if(enemy != owner) {
                enemyList.Remove(enemy);
            }
        }
	}
	
}
