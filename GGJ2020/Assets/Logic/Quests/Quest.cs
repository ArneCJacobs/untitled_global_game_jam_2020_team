using System;
using System.Linq;

namespace Logic
{
    public abstract class Quest
    {
        public string Description;

        public string Title;
        public string Hints;
        public float MaxDuration = 20; // the maximum amount of time the player can take to prepare for the quest

        public float Difficulty;

        public Quest(float difficulty)
        {
            Difficulty = difficulty;
        }

        public abstract QuestResult GetResult(Party party);


        protected static void DamageRandomParts(Party party, float avrDamage)
        {
            var random = new Random();
            foreach (var body in party.Bodies)
            {
                // foreach (var part in from slot in body.Slots
                //     where slot.AssignedPart != null
                //     where random.NextDouble() > 0.5
                //     select slot.AssignedPart)
                // {
                //     // part.Stats.Durability -= (float) random.NextDouble() * avrDamage;
                // }
            }
        }
    }
}