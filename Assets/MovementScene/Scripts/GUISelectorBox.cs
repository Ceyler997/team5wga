using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISelectorBox : MonoBehaviour
{
    // Draggable inspector reference to the Image GameObject's RectTransform.
    public RectTransform selectionBox;

    // This variable will store the location of wherever we first click before dragging.
    private Vector2 initialClickPosition = Vector2.zero;

    public static Vector2 BoxStart;
    public static Vector2 BoxFinish;
    private CharactersController CC;

    private void Awake()
    {
        CC = GetComponent<CharactersController>();
    }


    void Update()
    {
        // Click somewhere in the Game View.
        if (Input.GetMouseButtonDown(0))
        {
            initialClickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            selectionBox.anchoredPosition = initialClickPosition;
            CC.UserIsDragging = true;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 newSize;
            float deltaX = Mathf.Abs(Screen.width - (Screen.width + (Input.mousePosition.x - initialClickPosition.x)));
            float deltaY = Mathf.Abs(Screen.height - (Screen.height + (Input.mousePosition.y - initialClickPosition.y)));

            newSize = new Vector2(deltaX, deltaY);

            selectionBox.localScale = new Vector3(Input.mousePosition.x < initialClickPosition.x ? - 1 : 1,
                Input.mousePosition.y < initialClickPosition.y ? -1 : 1,
                selectionBox.localScale.z);

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            if (selectionBox.localScale.x == 1 && selectionBox.localScale.y == -1) // Bottom right
            {
                BoxStart = new Vector2(Input.mousePosition.x - selectionBox.sizeDelta.x, Input.mousePosition.y + selectionBox.sizeDelta.y);
            }
            else if (selectionBox.localScale.x == -1 && selectionBox.localScale.y == -1) // Bottom left
            {
                BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + selectionBox.sizeDelta.y);
            }
            else if (selectionBox.localScale.x == -1 && selectionBox.localScale.y == 1) // Top left
            {
                BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            else if (selectionBox.localScale.x == 1 && selectionBox.localScale.y == 1) // Top right
            {
                BoxStart = new Vector2(Input.mousePosition.x - selectionBox.sizeDelta.x, Input.mousePosition.y);
            }
            selectionBox.sizeDelta = newSize;

            BoxFinish = new Vector2(BoxStart.x + selectionBox.sizeDelta.x, BoxStart.y - selectionBox.sizeDelta.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectionBox.sizeDelta = Vector2.zero;
            CC.UserIsDragging = false;
        }


    }
}
