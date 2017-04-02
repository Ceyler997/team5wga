using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

    public ButtonAnim[] btns;
    private void OnMouseEnter()
    {
        print("in");
        foreach (ButtonAnim btn in btns)
            btn.GUIReact(false);
    }

    private void OnMouseExit()
    {
        print("exit");
        foreach (ButtonAnim btn in btns)
            btn.GUIReact(true);
    }
}
