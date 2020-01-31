using System.Collections.Generic;

namespace Logic
{
    public class Body
    {
        public List<Slot> Slots;
        public Stats BaseStats;

        public Body()
        {
            BaseStats = new Stats();
            Slots = new List<Slot>();
            for (int i = 0; i < 5; i++)
            {
                Slots.Add(new Slot());
            }
        }

        public Stats CalculateStats()
        {
            var counter = new Stats();
            foreach (var slot in Slots)
            {
                if (slot.AssignedPart != null)
                {
                    counter += slot.AssignedPart.Stats;
                }
            }

            return counter;
        }
    }
}