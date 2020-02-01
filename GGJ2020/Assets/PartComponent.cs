using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;

public class PartComponent : MonoBehaviour
{
    public Part part;
    
    
    void Start()
    {
        //TODO remove these lines
        part = PartGenerator.GeneratePart();
        part.Type = PartType.LEFTLEG;
    }
}
