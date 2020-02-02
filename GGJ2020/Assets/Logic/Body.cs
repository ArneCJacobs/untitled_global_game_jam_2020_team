using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class Body
    {
        public List<Part> Slots;
        public Stats BaseStats;

        public Body()
        {
            BaseStats = new Stats();
            Slots = new List<Part>();
        }

        public Stats CalculateStats()
        {
            var counter = new Stats();
            foreach (var slot in Slots)
            {
                counter += slot.Stats;
            }

            return counter;
        }
    }
}