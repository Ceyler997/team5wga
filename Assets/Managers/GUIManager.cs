using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GUIManager {

	static public void showCanvasGroup(CanvasGroup group) {
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    static public void hideCanvasGroup(CanvasGroup group) {
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}
