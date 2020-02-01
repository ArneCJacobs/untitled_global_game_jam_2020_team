using System.Collections.Generic;
using UnityEngine;

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
                var obj = new GameObject();
                var snappingPoint = obj.AddComponent<SnappingPoint>();
                
                Slots.Add(snappingPoint);
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