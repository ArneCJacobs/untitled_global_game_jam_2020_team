using System.Collections.Generic;

namespace Logic
{
    public class Body
    {
        public List<SnappingPoint> Slots;
        public Stats BaseStats;

        public Body()
        {
            BaseStats = new Stats();
            Slots = new List<SnappingPoint>();
            for (int i = 0; i < 5; i++)
            {
                Slots.Add(new SnappingPoint());
            }
        }

        public Stats CalculateStats()
        {
            var counter = new Stats();
            foreach (var slot in Slots)
            {
                if (slot.AssignedPart != null)
                {
                    var comp = slot.AssignedPart.GetComponent<BodyPartVisual>();

                    counter += comp.AssignedPart.Stats;
                }
            }

            return counter;
        }
    }
}