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
            SceneManager.LoadScene("Main", LoadSceneMode.Single);

        if (!AudioSource.isPlaying)
        {
            AudioSource.clip = AudioClip;
            AudioSource.loop = true;
            AudioSource.Play();
        }
    }
}
