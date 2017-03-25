using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject:MonoBehaviour {
    private Player player;
    public BaseObject() {
        player = new Player("Empty");
    }
	public Vector3 getPosition() {
        return player.transform.position;
    }

	public Player getPlayer { get { return player; } }

}
