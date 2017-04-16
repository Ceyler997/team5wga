using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Radius))]
[RequireComponent(typeof(Player))]
public abstract class BaseObject : MonoBehaviour, IEnemyChecker {
    private Player controllPlayer; //Игрок, который является владельцем объекта
    private Radius radius; //Радиус обнаружения врагов

    public void setPlayer(Player player) {
        controllPlayer = GetComponent<Player>();
        controllPlayer = player;
        radius = GetComponent<Radius>();
    }

    //Методы коллайдера для отследивания юнитов вокруг
    public abstract void ObjectEnter(BaseObject baseObject);
    public abstract void ObjectExit(BaseObject baseObject);

    public Player ControllPlayer{ get { return controllPlayer; } set { controllPlayer = value; } }
    public Radius Radius{get { return radius; } set { radius = value; } }

}
