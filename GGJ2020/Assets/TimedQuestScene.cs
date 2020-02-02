using Logic;
using UnityEngine;
using UnityEngine.UI;

public class TimedQuestScene : MonoBehaviour
{
    [SerializeField] private float initialTimer = 60f;
    private float timer;
    private GameObject questPopup;
    private Quest myQuest;
    private Party myParty;
    private QuestResult myResult;
    private GameObject questTitle;
    private GameObject questDescription;
    private GameObject questResults;
    private bool finished = false;

    // Start is called before the first frame update
    void Start()
    {

        timer = initialTimer;
        questPopup = transform.Find("QuestCompletePopup").gameObject;
        questPopup.GetComponent<Canvas>().enabled = false;

        myQuest = new QuestGenerator().GenerateQuest(10);
        questTitle = transform.Find("QuestCompletePopup/QuestTitle").gameObject;
        questDescription = transform.Find("QuestCompletePopup/QuestDescription").gameObject;
        questResults = transform.Find("QuestCompletePopup/QuestResults").gameObject;

        questTitle.GetComponent<Text>().text = myQuest.Title;
        questDescription.GetComponent<Text>().text = myQuest.Description;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (finished) return;
            FinishQuest();
            finished = true;
        }
    }

    public void FinishQuest()
    {
        myParty = PartyGenerator.GenerateRandomParty();
        myResult = myQuest.GetResult(myParty);
        questPopup.GetComponent<Canvas>().enabled = true;
        questResults.GetComponent<QuestResultBox>().Display(myResult);
    }

    public void PopupContinueClicked()
    {
        finished = false;
        timer = initialTimer;
        questPopup.GetComponent<Canvas>().enabled = false;
        myQuest = new QuestGenerator().GenerateQuest(10);
    }
}