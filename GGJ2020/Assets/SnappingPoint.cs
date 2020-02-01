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
    
    public List<PartType> AcceptedTypes { get; set; }
    public PartType Preferred { get; set; } 
    public Part AssignedPart { get; set; }

    public SnappingPoint()
    {
        AcceptedTypes = new List<PartType>();
        // Preferred = PartType.HEAD;
    }


    public CanSnap()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
