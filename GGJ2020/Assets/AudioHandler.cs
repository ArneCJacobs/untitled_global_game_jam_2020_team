using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    // Start is called before the first frame update
    List<AudioSource> AudioSources = new List<AudioSource>();
    float FadeInTime = 0;
    float MaxFadeInTime = 5.0f;
    void Start()
    {
        AudioSources = this.GetComponentsInChildren<AudioSource>().ToList();
            
        foreach ( var audioSource in AudioSources)
        {
            audioSource.volume = 0.0f;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeInTime < MaxFadeInTime)
        {
            float factor = FadeInTime / MaxFadeInTime;
            FadeInTime += Time.deltaTime;
            foreach (var audioSource in AudioSources)
            {
                audioSource.volume = Mathf.Lerp(0, 1.0f, factor);
            }
        }

    }
}
