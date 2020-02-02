using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class audioFadeOut : MonoBehaviour
{

    public int secondsToFadeOut = 5;

    public void findAudio()
    {
        StartCoroutine(findAudioAndFadeOut());
    }

    IEnumerator findAudioAndFadeOut()
    {
        // Find Audio Music in scene
        var allAudioSources = FindObjectsOfType<AudioSource>();


        foreach (var audio in allAudioSources)
        {
            while (audio.volume > 0.01f)
            {
                audio.volume -= Time.deltaTime / secondsToFadeOut;
                yield return null;
            }

            // Make sure volume is set to 0
            audio.volume = 0;

            // Stop Music
            audio.Stop();

            // Destroy
            Destroy(audio);
        }
    }
}