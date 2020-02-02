using System;
using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;
using UnityEngine.UI;

public class QuestStartPopup : MonoBehaviour
{
    private GameObject questTitle;
    private GameObject questDescription;
    private GameState gameState;
    private GameObject questHints;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
        questTitle = transform.Find("QuestTitle").gameObject;
        questDescription = transform.Find("QuestDescription").gameObject;
        gameState = GameObject.Find("/GameState").gameObject.GetComponent<GameState>();
        QuestEventManager.QuestStartedEvent += DisplayQuest;
    }

    private void OnDisable()
    {
        QuestEventManager.QuestStartedEvent -= DisplayQuest;
    }

    public void DisplayQuest(Quest quest)
    {
        gameState.Pause();
        GetComponent<Canvas>().enabled = true;
        questTitle.GetComponent<Text>().text = quest.Title;
        questDescription.GetComponent<Text>().text = quest.Description;
        questHints.GetComponent<Text>().text = quest.Hints;
    }

    public void OnContinueButtonClick()
    {
        gameState.Play();
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}