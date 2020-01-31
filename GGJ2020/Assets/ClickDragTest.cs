using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDragTest : MonoBehaviour
{
    public SpringJoint2D spring;

    float distance = 10;

    private void OnMouseDrag()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        transform.position =mousePos;
    }
}