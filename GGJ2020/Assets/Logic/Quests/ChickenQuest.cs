using System;
using System.Net.Http.Headers;
using System.Text;

namespace Logic.Quests
{
    public class ChickenQuest : Quest
    {
        public ChickenQuest(float difficulty) : base(difficulty)
        {
            Title = "Chicken Chaser";
            var sb = new StringBuilder();
            var random = new Random();
            sb.Append("HELP! My farm is overrun with chickens!\n");
            sb.Append("They have taken control!\n");
            sb.Append("You need to kill ");
            sb.Append((int) difficulty * 0.5 + random.Next((int) -difficulty, (int) difficulty));
            sb.Append(" chickens!\n");
            Description = sb.ToString();
        }


        public override QuestResult GetResult(Party party)
        {
            int killedChickens = (int) ((party.GetAverageStats().Strength / 3) + party.GetAverageStats().Dexterity -
                                        Difficulty);

            var qr = new QuestResult();
            if (killedChickens <= 0) return qr;

            qr.Gold = killedChickens * 100;
            for (var i = 0; i < killedChickens / 2; i++)
            {
                qr.Loot.Add(new Part());
            }

            return qr;
        }
    }
}