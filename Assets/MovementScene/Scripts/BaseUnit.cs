using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour {
    public bool IsSelectable;
    public bool IsSelected = false;
    public bool IsOnScreen;
    public Vector2 ScreenPos;
    public float MoveSpeed;
    public float Health;

}
