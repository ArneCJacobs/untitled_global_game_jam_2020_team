using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var gs = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        var stats = gs.Party.GetAverageStats();

        var textComp = GetComponent<Text>();
        textComp.text = $"Time left: {gs.QuestTimer:n2}s";
    }
}
