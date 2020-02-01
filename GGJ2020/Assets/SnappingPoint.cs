using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;

public class SnappingPoint : MonoBehaviour
{
    public bool IsTypeRestricted = false;
    public List<PartType> AcceptedTypes; 
    public PartType Preferred { get; set; }
    public GameObject AssignedPart; 
    
    // Start is called before the first frame update
    void Start()
    {
        tag = "snap";

    }


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

        AssignedPart = part;
        return AssignedPart == null && (IsTypeRestricted ? AcceptedTypes.Contains(bodypartVisualComp.AssignedPart.Type) : true);
    }


    public void UnSnap(bool destroy = false)
    {
        if (destroy && AssignedPart != null)
        {
            GameObject.Destroy(AssignedPart);
        }
        AssignedPart = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
