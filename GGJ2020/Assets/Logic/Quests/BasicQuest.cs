namespace Logic.Quests
{
    public class BasicQuest : Quest
    {
        public BasicQuest(float difficulty) : base(difficulty)
        {
            Description = "do a generic thing";
            Title = "Basic Quest";
        }

        public override QuestResult GetResult(Party party)
        {
            var result = new QuestResult();
            if (!(party.GetAverageStats().Vitality > 100)) return result;
            result.success = true;
            result.Report = "Lots of stuff happened. You should have been there.";
            result.ReturnParty = party;
            result.Gold = 1000;
            return result;
        }
    }
}