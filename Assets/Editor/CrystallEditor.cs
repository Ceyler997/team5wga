using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Crystal))]
public class CrystallEditor : Editor {

    private void OnSceneGUI()
    {
        Crystal crystall = (Crystal)target;

        Handles.color = Color.magenta;
        Handles.DrawWireArc(crystall.transform.position, Vector3.up, Vector3.forward, 360, 10);
        Handles.color = Color.cyan;
        Handles.DrawWireArc(crystall.transform.position, Vector3.up, Vector3.forward, 360, 20);
    }
}
