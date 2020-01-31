namespace Logic
{
    public class BasicQuest : Quest
    {

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