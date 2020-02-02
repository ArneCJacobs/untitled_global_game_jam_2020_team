using System;
using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;
using Random = System.Random;

public class SFXPlayer : MonoBehaviour
{
    private Dictionary<string, List<AudioClip>> audioBank;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioBank = new Dictionary<string, List<AudioClip>>
        {
            {
                "clickPart", new List<AudioClip>()
                {
                    Resources.Load<AudioClip>("Sound/Effects/grab1"),
                    Resources.Load<AudioClip>("Sound/Effects/grab2"),
                    Resources.Load<AudioClip>("Sound/Effects/shortsplat"),
                }
            },
            {
                "attachPart", new List<AudioClip>()
                {
                    Resources.Load<AudioClip>("Sound/Effects/shortsplat"),
                    Resources.Load<AudioClip>("Sound/Effects/zipsplat"),
                    Resources.Load<AudioClip>("Sound/Effects/zipsplat2"),
                    Resources.Load<AudioClip>("Sound/Effects/zipsplat3"),
                    Resources.Load<AudioClip>("Sound/Effects/zipsplat4"),
                }
            },
            {
                "hoverButton", new List<AudioClip>()
                {
                    Resources.Load<AudioClip>("Sound/Effects/hover"),
                }
            },
            {
                "clickButton", new List<AudioClip>()
                {
                    Resources.Load<AudioClip>("Sound/Effects/click")
                }
            }
        };
    }

    private void OnEnable()
    {
        SFXEventHandler.ClickPartEvent += PlayPartClick;
        SFXEventHandler.AttachPartEvent += PlayPartAttach;
        SFXEventHandler.ButtonHoverEvent += PlayButtonHover;
        SFXEventHandler.ButtonClickEvent += PlayButtonClick;
    }

    private void OnDisable()
    {
        SFXEventHandler.ClickPartEvent -= PlayPartClick;
        SFXEventHandler.AttachPartEvent -= PlayPartAttach;
        SFXEventHandler.ButtonHoverEvent -= PlayButtonHover;
        SFXEventHandler.ButtonClickEvent -= PlayButtonClick;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void PlayRandom(string type)
    {
        var rand = new Random();
        var clips = audioBank[type];
        audioSource.PlayOneShot(clips[rand.Next(clips.Count)]);
    }

    private void PlayPartClick()
    {
        PlayRandom("clickPart");
    }

    private void PlayPartAttach(GameObject target)
    {
        PlayRandom("attachPart");
    }

    private void PlayButtonHover()
    {
        PlayRandom("hoverButton");
    }

    private void PlayButtonClick()
    {
        PlayRandom("clickButton");
    }
}