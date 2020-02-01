using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    // Start is called before the first frame update
    List<Light> Lights = new List<Light>();
    void Start()
    {
        Lights = this.GetComponentsInChildren<Light>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var light in Lights)
        {
            var minFlickerIntensity = 3.75f;
            var maxFlickerIntensity = 4.0f;
  
            light.intensity = (Random.Range (minFlickerIntensity, maxFlickerIntensity));       
        }
    }
}
