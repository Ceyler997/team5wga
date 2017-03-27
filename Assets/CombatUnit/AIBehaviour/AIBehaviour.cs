using UnityEngine;
using System;

abstract public class AIBehaviour {
    abstract public BaseObject Subject {get;set;}

    abstract public void UpdateState();
}
