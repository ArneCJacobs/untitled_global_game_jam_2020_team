using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Logic;
using UnityEngine;

public class QuestToolTip : SimpleTooltip
{
    // Start is called before the first frame update
    private bool initialized = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameState gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();

        StringBuilder sb = new StringBuilder();
        sb.Append(gameState.CurrentQuest.Title + "\n");
        sb.Append(gameState.CurrentQuest.Description);
        infoLeft = sb.ToString();

        infoRight = gameState.CurrentQuest.Difficulty.ToString("0");
    }
}