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

    static public void setCanvasGroupInactive(CanvasGroup group) {
        group.alpha = 0.5f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}
