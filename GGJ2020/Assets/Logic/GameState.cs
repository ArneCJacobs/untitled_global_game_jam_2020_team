using System;
using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public Party Party;
    public Quest CurrentQuest;
    
    public List<BodyPartVisual> BeltContent;
    public List<BodyPartVisual> PartQueue;
    public List<Body> BodyQueue;
    
    public int Gold;
    public float Difficulty;
    
    private static QuestGenerator _questGenerator = new QuestGenerator();

    public float QuestEndTime;


    public QuestResult LastQuestResult;

    private int _difficultyDelta = 2;

    public void Update()
    {
        if (QuestEndTime < Time.time)
        {
            LastQuestResult = CurrentQuest.GetResult(Party);
            CurrentQuest = _questGenerator.GenerateQuest(Difficulty);
            QuestEndTime = Time.time + CurrentQuest.MaxDuration;
            Difficulty += _difficultyDelta;
            _difficultyDelta += 1;
        }
        
    }


    public void Restart()
    {
        Difficulty = 0;
        Gold = 0;
        
        Party = new Party();

        CurrentQuest = _questGenerator.GenerateQuest(Difficulty);
        QuestEndTime = CurrentQuest.MaxDuration + Time.time;
        
        BeltContent = new List<BodyPartVisual>();
        PartQueue = new List<BodyPartVisual>();
        BodyQueue = new List<Body>();
        LastQuestResult = null;
    }
}
