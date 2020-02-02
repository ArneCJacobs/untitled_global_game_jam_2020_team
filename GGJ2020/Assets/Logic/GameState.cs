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
    private float questTimer;
    public QuestResult LastQuestResult;
    private int _difficultyDelta = 2;
    private bool paused;

    private enum QuestState
    {
        New,
        Complete
    }

    private QuestState questState;

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
        if (questTimer > 0)
        {
            questTimer -= Time.deltaTime;
        }
        else
        {
            if (questState == QuestState.New)
            {
                FinishQuest();
                IncreaseDifficulty();
            }
            else
            {
                StartNewQuest();
            }
        }
    }

    public void FinishQuest()
    {
        questState = QuestState.Complete;
        LastQuestResult = CurrentQuest?.GetResult(Party);
        QuestEventManager.SendQuestFinished(CurrentQuest, LastQuestResult);
    }

    public void StartNewQuest()
    {
        questState = QuestState.New;
        CurrentQuest = _questGenerator.GenerateQuest(Difficulty);
        questTimer = CurrentQuest.MaxDuration;
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
        questTimer = CurrentQuest.MaxDuration;

        BeltContent = new List<BodyPartVisual>();
        PartQueue = new List<BodyPartVisual>();
        BodyQueue = new List<Body>();
        LastQuestResult = null;
    }
}