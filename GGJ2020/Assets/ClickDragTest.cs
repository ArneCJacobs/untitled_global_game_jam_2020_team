using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DigitalRuby.LightningBolt;
using Logic;
using UnityEngine;
using UnityEngine.Serialization;

public class ClickDragTest : MonoBehaviour
{
    public float snappingDistance = 100f;
    private Vector3 startPos;
    public GameObject snappedTo;
    public GameObject startSnappedTo;
    private SnappingPoint prevsnappingPoint;
    private GameObject lightning;
    private LightningBoltScript lScript;
    private LineRenderer lightningRenderer;
    private AudioSource audioSource;

    public void Start()
    {
        lightning = transform.Find("/Lightning").gameObject;
        if (lightning == null) return;
        lightningRenderer = lightning.GetComponent<LineRenderer>();
        lScript = lightning.GetComponent<LightningBoltScript>();
        audioSource = transform.gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Sound/Effects/electriccurrent");
    }

    private void OnMouseDown()
    {
        SFXEventHandler.SendClickPart();
        startPos = transform.position;
        if (snappedTo == null) return;
        startSnappedTo = snappedTo;
        prevsnappingPoint = snappedTo.GetComponent<SnappingPoint>();
        prevsnappingPoint.UnSnap();
        snappedTo = null;
    }

    void OnMouseDrag()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //getting cursor position
        transform.position = cursorPosition;

        var snaps = GameObject.FindObjectsOfType<SnappingPoint>().ToArray();
        var snapsInChildren = GetComponentsInChildren<SnappingPoint>();
        var rslt = GetDistanceToClosestSnappingPoint(snaps.Where(o => !snapsInChildren.Contains(o)).ToArray());
        if (rslt.distance < snappingDistance)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            if (lightning != null)
            {
                lScript.StartObject = transform.gameObject;
                lScript.EndObject = rslt.target.gameObject;
                lScript.Trigger();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Update()
    {
        if (snappedTo != null)
        {
            var transFormComp = snappedTo.GetComponent<Transform>();
            var bodyPartVisual = GetComponent<BodyPartVisual>();
            var details = Game.GUI.GuiHelpers.GetPartTypeDetails(bodyPartVisual.AssignedPart.VisualType);
            this.GetComponent<Transform>().position =
                (transFormComp.position + new Vector3(0.0f, 0.0f, details.ZOffset));
        }
    }

    private void OnMouseUp()
    {
        audioSource.Stop();

        SnappingPoint[] snaps = GameObject.FindObjectsOfType<SnappingPoint>();
        var rslt = GetDistanceToClosestSnappingPoint(snaps);
        if (rslt.distance < snappingDistance)
        {
            Debug.DrawLine(transform.position, rslt.target.transform.position, Color.green, 0, false);

            transform.position = rslt.target.transform.position;

            prevsnappingPoint = null;
            if (snappedTo != null)
                prevsnappingPoint = snappedTo.GetComponent<SnappingPoint>();


            snappedTo = rslt.target.gameObject;

            SnappingPoint snappingPoint = snappedTo.GetComponent<SnappingPoint>();

            if (snappingPoint != null)
            {
                SFXEventHandler.SendAttachPart(snappingPoint.gameObject);
                if (prevsnappingPoint != null)
                    prevsnappingPoint.UnSnap();
                snappingPoint.UnSnap();
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
            SFXEventHandler.SendAttachPart(null);
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