namespace Logic
{
    public abstract class Quest
    {
        public string Description;

        public string Title;
        public float Timelimit;

        public float Difficulty;

        public abstract QuestResult GetResult(Party party);

    }
}