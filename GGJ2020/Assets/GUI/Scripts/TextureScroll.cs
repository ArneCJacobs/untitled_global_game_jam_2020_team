using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureScroll : MonoBehaviour
{
    public float ScrollSpeed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(-(Time.time * ScrollSpeed), 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }

    public void SetScrollSpeed(float speed)
    {
        ScrollSpeed = speed;
    }
}
