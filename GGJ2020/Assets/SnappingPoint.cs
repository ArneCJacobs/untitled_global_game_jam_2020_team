using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;

public class SnappingPoint : MonoBehaviour
{
    public bool IsTypeRestricted = false;
    public GameObject SnappedObject = null;
    // Start is called before the first frame update
    void Start()
    {
        tag = "snap";

    }

    public List<PartType> AcceptedTypes; 
    public PartType Preferred { get; set; }
    public GameObject AssignedPart; 

    public SnappingPoint()
    {
        AcceptedTypes = new List<PartType>();
        // Preferred = PartType.HEAD;
    }


    public bool CanSnap(GameObject part)
    {
        var bodypartVisualComp = part.GetComponent<BodyPartVisual>();
        if (bodypartVisualComp == null)
            return false;

        SnappedObject = part;
        return AssignedPart == null && (IsTypeRestricted ? AcceptedTypes.Contains(bodypartVisualComp.AssignedPart.Type) : true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
