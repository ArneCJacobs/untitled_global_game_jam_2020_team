using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDragTest : MonoBehaviour
{
    void OnMouseDrag()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //getting cursor position
        transform.position = cursorPosition;
    }
}