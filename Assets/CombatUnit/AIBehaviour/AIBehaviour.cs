using UnityEngine;
using System;

abstract class AIBehaviour {
    abstract public BaseObject Subject {get;set;}

    abstract public void UpdateState();
}
