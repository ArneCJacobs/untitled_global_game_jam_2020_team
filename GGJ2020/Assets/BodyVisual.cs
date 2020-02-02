using Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyVisual : MonoBehaviour
{
    private Body Body;

    List<SnappingPoint> SnapList = null;

    // Start is called before the first frame update
    void Start()
    {
        // Body = new Body();
        // foreach (var item in GetComponentsInChildren<SnappingPoint>())
            // Body.Slots.Add(item.AssignedPart.GetComponent<BodyPartVisual>().AssignedPart);
    }

    // Update is called once per frame
    void Update()
    {
    }
}