using System;
using System.Net.Http.Headers;
using System.Text;
using UnityEngine.UI;

namespace Logic.Quests
{
    public class ChickenQuest : Quest
    {
        private int chickenCount;

        public ChickenQuest(float difficulty) : base(difficulty)
        {
            Title = "Chicken Chaser";
            var sb = new StringBuilder();
            var random = new Random();
            chickenCount = (int) (difficulty * 0.5 + random.Next((int) -difficulty, (int) difficulty));
            sb.Append("HELP! My farm is overrun with chickens!\n");
            sb.Append("They have taken control!\n");
            sb.Append("You need to kill ");
            sb.Append(chickenCount);
            sb.Append(" chickens!\n");
            Description = sb.ToString();
        }


        public override QuestResult GetResult(Party party)
        {
            var killedChickens = (int) ((party.GetAverageStats().Strength / 3) + party.GetAverageStats().Dexterity -
                                        Difficulty);

            var qr = new QuestResult {ReturnParty = party};
            DamageRandomParts(qr.ReturnParty, Difficulty / 100);
            if (killedChickens <= 0) return qr;
            if (killedChickens >= chickenCount)
            {
                qr.success = true;
            }

            qr.Gold = killedChickens;
            for (var i = 0; i < killedChickens / 2; i++)
            {
                qr.Loot.Add(PartGenerator.GeneratePart());
            }

            return qr;
        }
    }
}