using System;

namespace Logic
{
    public abstract class Quest
    {
        public string Description;

        public string Title;
        public float Timelimit;

        public float Difficulty;

        public Quest(float difficulty)
        {
            Difficulty = difficulty;
        }

        public abstract QuestResult GetResult(Party party);


        protected static void DamageRandomParts(Party party, float avrDamage)
        {
            Random random = new Random();
            foreach (Body body in party.Bodies)
            {
                foreach (SnappingPoint slot in body.Slots)
                {
                    if (random.NextDouble() > 0.5)
                    {
                      //  Part part = slot.AssignedPart;
                        //part.Stats.Durability -= (float) random.NextDouble() * avrDamage;
                    }
                }
            }
        }
    }
}