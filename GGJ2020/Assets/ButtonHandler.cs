using System;
using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseEnter()
    {
        SFXEventHandler.SendButtonHover();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
