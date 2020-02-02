using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
    private bool paused;

    public void Start()
    {
        Restart();
    }

    public void Pause()
    {
        paused = true;
    }

    public void Play()
    {
        paused = false;
    }

    public void Update()
    {
        if (paused) return;
        if (!(QuestEndTime < Time.time)) return;
        FinishQuest();
        IncreaseDifficulty();
        StartNewQuest();
    }

    public void FinishQuest()
    {
        LastQuestResult = CurrentQuest?.GetResult(Party);
        QuestEventManager.SendQuestFinished(CurrentQuest, LastQuestResult);
    }

    public void StartNewQuest()
    {
        CurrentQuest = _questGenerator.GenerateQuest(Difficulty);
        QuestEndTime = Time.time + CurrentQuest.MaxDuration;
        QuestEventManager.SendQuestStarted(CurrentQuest);
    }

    public void IncreaseDifficulty()
    {
        Difficulty += _difficultyDelta;
        _difficultyDelta += 1;
    }

    public void Restart()
    {
        Difficulty = 0;
        Gold = 0;

        Party = new Party();

        StartNewQuest();

        BeltContent = new List<BodyPartVisual>();
        PartQueue = new List<BodyPartVisual>();
        BodyQueue = new List<Body>();
        LastQuestResult = null;
    }
}