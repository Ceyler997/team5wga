using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour {
    private Player player;
    private float followRadius;

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

    public float FollowRadius {
        get { return followRadius; }

        set { followRadius = value; }
    }
}
