using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var textComp = GetComponent<Text>();
        textComp.text = Input.GetKeyDown(KeyCode.Space) ? "" : "Hold Space";
    }
}
