namespace Logic.Quests
{
    public class BasicQuest : Quest
    {
        public BasicQuest(float difficulty) : base(difficulty)
        {
            Description = "do a generic thing";
            Title = "Chicken Chaser";

        }

        public override QuestResult GetResult(Party party)
        {
            QuestResult result = new QuestResult();
            if (party.GetAverageStats().Vitality > 100)
            {

                result.ReturnParty = party;
                result.Gold = 1000;
            }
            else
            {
                result.Gold = 0;

            }
            return result;
        }

    }
}