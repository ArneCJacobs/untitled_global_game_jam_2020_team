using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Logic;
using UnityEngine;
using UnityEngine.Serialization;

public class ClickDragTest : MonoBehaviour
{
    public float snappingDistance = 10f;
    private Vector3 startPos;
    private GameObject snappedTo;

    private void OnMouseDown()
    {
        startPos = transform.position;
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
        
        SnappingPoint[] snaps = GameObject.FindObjectsOfType<SnappingPoint>();
        var rslt = GetDistanceToClosestSnappingPoint(snaps);
        PartComponent partComponent = GetComponent<PartComponent>();
        if (rslt.distance< snappingDistance && (partComponent == null || rslt.target.CanSnap(partComponent.part)))
        {
            transform.position= rslt.target.transform.position;
            snappedTo = rslt.target.gameObject;
           // this.transform.SetParent(snappedTo.transform);
        }
        else
        {
            transform.position = startPos;
        }

    }

    private (SnappingPoint target, float distance) GetDistanceToClosestSnappingPoint(SnappingPoint[] enemies)
    {
        SnappingPoint bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (SnappingPoint potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
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