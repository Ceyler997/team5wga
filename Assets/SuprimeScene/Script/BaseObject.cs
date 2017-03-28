using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour {
    private Player player;

    public Vector3 getPosition() {
        return transform.position;
    }

    public Player Player {
        get {
            return player;
        }

        set {
            player = value;
        }
    }
}
