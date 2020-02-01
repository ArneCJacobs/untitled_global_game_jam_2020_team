using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompletePopup : MonoBehaviour
{
    private Quest myQuest;
    private Party myParty;
    private QuestResult myResult;
    private GameObject questTitle;
    private GameObject questDescription;
    private GameObject questResults;
    
    // Start is called before the first frame update
    void Start()
    {
        myQuest = new QuestGenerator().GenerateQuest(10);
        myParty = PartyGenerator.GenerateRandomParty();
        myResult = myQuest.GetResult(myParty);
        questTitle = transform.Find("QuestTitle").gameObject;
        questDescription = transform.Find("QuestDescription").gameObject;
        questResults = transform.Find("QuestResults").gameObject;
        
        questTitle.GetComponent<Text>().text = myQuest.Title;
        questDescription.GetComponent<Text>().text = myQuest.Description;
        questResults.GetComponent<QuestResultBox>().Display(myResult);


    }

    public void OnContinueButtonClick()
    {
        Debug.Log("Continue button clicked!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
