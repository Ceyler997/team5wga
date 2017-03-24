using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject {
    private Player player;
	public Vector3 getPosition() {
        return player.transform.position;
    }

	public Player getPlayer { get { return player; } }

}
