using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Logic;
using UnityEngine;
using UnityEngine.Serialization;

public class ClickDragTest : MonoBehaviour
{
    public float snappingDistance = 100f;
    private Vector3 startPos;
    public GameObject snappedTo;
    public GameObject startSnappedTo;

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
        
        var snaps = GameObject.FindObjectsOfType<SnappingPoint>().ToArray();
        var snapsInChildren = GetComponentsInChildren<SnappingPoint>();
        var rslt = GetDistanceToClosestSnappingPoint(snaps.Where(o => !snapsInChildren.Contains(o)).ToArray());
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
        if (rslt.distance< snappingDistance)
        {
            Debug.DrawLine(transform.position, rslt.target.transform.position, Color.green, 0, false);

            transform.position= rslt.target.transform.position;

            SnappingPoint prevsnappingPoint = null;
            if (snappedTo != null)
                prevsnappingPoint = snappedTo.GetComponent<SnappingPoint>();


            snappedTo = rslt.target.gameObject;

            SnappingPoint snappingPoint = snappedTo.GetComponent<SnappingPoint>();

            if (snappingPoint != null)
            {
                if (prevsnappingPoint != null)
                    prevsnappingPoint.UnSnap();

                snappingPoint.AssignGameObject(gameObject);

                var bodyPartComp = GetComponent<BodyPartVisual>();
                if (bodyPartComp != null)
                {
                    bodyPartComp.ResetRotationsAndTranslations(snappingPoint.IsBody, snappedTo.transform);
                }
            }

            //this.transform.SetParent(snappedTo.transform);
        }
        else
        {
            transform.position = startPos;
            snappedTo = startSnappedTo;
        }
    }

    private (SnappingPoint target, float distance) GetDistanceToClosestSnappingPoint(SnappingPoint[] enemies)
    {
        //BodyPartVisual partComponent = GetComponent<BodyPartVisual>();
        SnappingPoint bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (SnappingPoint potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr && potentialTarget.CanSnap(gameObject))
            {
                
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return (bestTarget, closestDistanceSqr);
    }
}