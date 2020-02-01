using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ClickDragTest : MonoBehaviour
{
    public float snappingDistance = 10f;
    private Vector3 startPos;
    private GameObject snappedTo;
    private GameObject startSnappedTo;

    private void OnMouseDown()
    {
        startPos = transform.position;
        if (snappedTo == null) return;
        startSnappedTo = snappedTo;
        snappedTo = null;
    }

    void OnMouseDrag()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //getting cursor position
        transform.position = cursorPosition;
    }

    private void Update()
    {
        if (snappedTo != null)
        {
            var transFormComp = snappedTo.GetComponent<Transform>();
            this.GetComponent<Transform>().position = transFormComp.position;
        }
    }

    private void OnMouseUp()
    {
        var snaps = GameObject.FindGameObjectsWithTag("snap").Select((o => o.transform)).ToArray();
        var rslt = GetDistanceToClosestSnappingPoint(snaps);
        if (rslt.distance < snappingDistance)
        {
            transform.position = rslt.target.position;
            snappedTo = rslt.target.gameObject;
            // this.transform.SetParent(snappedTo.transform);
        }
        else
        {
            transform.position = startPos;
            snappedTo = startSnappedTo;
        }
    }

    private (Transform target, float distance) GetDistanceToClosestSnappingPoint(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return (bestTarget, closestDistanceSqr);
    }
}