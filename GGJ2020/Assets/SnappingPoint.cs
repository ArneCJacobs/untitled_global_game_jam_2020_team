using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;

public class SnappingPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        tag = "snap";

    }

    public List<PartType> AcceptedTypes; 
    public PartType Preferred { get; set; }
    public Part AssignedPart; 

    public SnappingPoint()
    {
        AcceptedTypes = new List<PartType>();
        // Preferred = PartType.HEAD;
    }


    public bool CanSnap(Part part)
    {
        return AssignedPart == null && AcceptedTypes.Contains(part.Type);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
