using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeltManager : MonoBehaviour
{
    public bool IsScrolling = false;
    public float ScrollSpeed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var scrollComps = GetComponentsInChildren<TextureScroll>();
        var rotationComps = GetComponentsInChildren<Rotation>();
        foreach (var scrollcomp in scrollComps)
            scrollcomp.ScrollSpeed = ScrollSpeed;
        foreach (var rotationComp in rotationComps)
            rotationComp.RotationSpeed = ScrollSpeed * 160;
    }
}
