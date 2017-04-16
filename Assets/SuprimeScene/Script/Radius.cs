using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Все кто реализуют интерфейс и компонент класса, будут получать информацию о врагах
interface IEnemyChecker {
    void ObjectEnter(BaseObject baseObject);
    void ObjectExit(BaseObject baseObject);
}

public class Radius : MonoBehaviour {
    public SphereCollider radius; // Размер области видимости
    public List<BaseObject> objectList; // Список врагов, находящихся в области кристалла
    BaseObject owner = null; // Объект у которого есть радиус

    // Конструктор кристалла 
    public void Initialization (float radiusSize, BaseObject ownerObject) {
        objectList = new List<BaseObject>();
        radius = gameObject.AddComponent<SphereCollider>();
		radius.radius = radiusSize; 	
		radius.isTrigger = true;
        owner = ownerObject;
    }

    // Если враг зашел в область видимости
	void OnTriggerEnter(Collider other) {
        BaseObject baseObject = other.GetComponent<BaseObject>();
        if(baseObject != null && other.GetType() != radius.GetType()) {
            // if(enemy != owner) {
                objectList.Add(baseObject);
                owner.ObjectEnter(baseObject);
           // }
        }
    }

    // Враг покидает область видимости
	void OnTriggerExit(Collider other) {
		BaseObject baseObject = other.GetComponent<BaseObject>();
        if(baseObject != null && other.GetType() != radius.GetType()) {
            if(baseObject != owner) {
                objectList.Remove(baseObject);
                owner.ObjectExit(baseObject);
            }
        }
	}

    //Если владелец имеет хотя бы одиного врага
    public bool HasEnemies() {
        foreach (BaseObject enemy in objectList) {
            if (enemy.ControllPlayer != owner.ControllPlayer) {
                return true;
            }
        }
        return false;
    }
    //Если владелец имеет хотя бы одиного союзника
    public bool HasFriends() {
        foreach (BaseObject friend in objectList) {
            if (friend.ControllPlayer == owner.ControllPlayer) {
                return true;
            }
        }
        return false;
    }
}
