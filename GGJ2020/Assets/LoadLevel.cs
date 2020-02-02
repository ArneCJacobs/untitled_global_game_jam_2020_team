using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    AudioSource AudioSource = null;
    AudioClip AudioClip = null;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = this.GetComponent<AudioSource>();
        AudioClip = Resources.Load<AudioClip>("Sounds/Music/menuloop");
        var m_hammerImageObject = GameObject.Find("BlackGround");
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.anyKey)
        {
            SceneManager.LoadScene("MainWithQuests", LoadSceneMode.Single);
            StartCoroutine(findAudioAndFadeOut());
        }


        if (!AudioSource.isPlaying)
        {
            AudioSource.clip = AudioClip;
            AudioSource.loop = true;
            AudioSource.Play();
        }
    }
    IEnumerator findAudioAndFadeOut()
    {
        // Find Audio Music in scene
        var allAudioSources = FindObjectsOfType<AudioSource>();

        int secondsToFadeOut = 5;

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
