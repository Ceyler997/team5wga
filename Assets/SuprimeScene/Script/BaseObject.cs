﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Интерфейс для тех кто получает дамаг

public class BaseObject : MonoBehaviour {
    private Player player;
	public Vector3 getPosition() {
        return player.transform.position;
    }
    public void setPlayer(Player player) {
        this.player = player;
    }

	public Player getPlayer { get { return player; } }

}
