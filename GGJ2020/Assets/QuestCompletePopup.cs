using System;
using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompletePopup : MonoBehaviour
{
    private GameObject questTitle;
    private GameObject questDescription;
    private GameObject questResults;
    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
        questTitle = transform.Find("QuestCompletePopup/QuestTitle").gameObject;
        questDescription = transform.Find("QuestCompletePopup/QuestDescription").gameObject;
        questResults = transform.Find("QuestCompletePopup/QuestResults").gameObject;
        gameState = GameObject.Find("/GameState").gameObject.GetComponent<GameState>();
        QuestEventManager.QuestFinishedEvent += DisplayQuest;
    }


    private void OnDisable()
    {
        QuestEventManager.QuestFinishedEvent -= DisplayQuest;
    }

    private void DisplayQuest(Quest quest, QuestResult result)
    {
        gameState.Pause();
        GetComponent<Canvas>().enabled = true;
        questTitle.GetComponent<Text>().text = quest.Title;
        questDescription.GetComponent<Text>().text = quest.Description;
        questResults.GetComponent<QuestResultBox>().Display(result);
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