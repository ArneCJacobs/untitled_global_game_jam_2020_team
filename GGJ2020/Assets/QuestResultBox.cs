using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;
using UnityEngine.UI;

public class QuestResultBox : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject goldReward;
    private GameObject questOutcome;
    private GameObject loot;

    void Awake()
    {
        goldReward = transform.Find("GoldReward").gameObject;
        questOutcome = transform.Find("QuestOutcome").gameObject;
        loot = transform.Find("ScrollView/Viewport/Loot").gameObject;
    }

    public void Display(QuestResult qr)
    {
        Debug.Log(qr.Gold.ToString());
        goldReward.GetComponent<Text>().text = $"Gold: {qr.Gold.ToString()}";
        questOutcome.GetComponent<Text>().text = qr.success ? "Success!" : "Fail!";
        loot.GetComponent<LootRewardBox>().Display(qr.Loot);
    }


    // Update is called once per frame
    void Update()
    {
    }
}