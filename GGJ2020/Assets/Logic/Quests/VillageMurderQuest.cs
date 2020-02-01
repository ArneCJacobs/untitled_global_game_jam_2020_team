using System;
using System.Net.Http.Headers;
using System.Text;

namespace Logic.Quests
{
    public class VillageMurderQuest : Quest
    {
        public VillageMurderQuest(float difficulty) : base(difficulty)
        {
            Title = "Village Slaughter";
            var sb = new StringBuilder();
            sb.Append("Attack a nearby settlement for new body parts.");
            Description = sb.ToString();
        }


        public override QuestResult GetResult(Party party)
        {
            int killedVictims =
                (int) ((party.GetAverageStats().Speed / 3 + party.GetAverageStats().Strength / 4 - Difficulty));
            killedVictims = Math.Max(0, killedVictims);

            var qr = new QuestResult();
            qr.ReturnParty = party;
            DamageRandomParts(qr.ReturnParty, Difficulty / 100 + 4);
            if (killedVictims <= 0) return qr;

            qr.Gold = killedVictims * party.GetAverageStats().Intelligence / 100;
            for (var i = 0; i < killedVictims / 2; i++)
            {
                qr.Loot.Add(PartGenerator.GeneratePart());
            }

            return qr;
        }
    }
}