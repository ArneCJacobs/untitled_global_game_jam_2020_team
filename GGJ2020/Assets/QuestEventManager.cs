using Logic;

public class QuestEventManager
{
    public delegate void OnQuestStarted(Quest startedQuest);

    public static event OnQuestStarted QuestStartedEvent;

    public delegate void OnQuestFinished(Quest quest, QuestResult qr);

    public static event OnQuestFinished QuestFinishedEvent;

    public static void SendQuestStarted(Quest quest)
    {
        QuestStartedEvent?.Invoke(quest);
    }

    public static void SendQuestFinished(Quest quest, QuestResult qr)
    {
        QuestFinishedEvent?.Invoke(quest, qr);
    }
}