using System;
using System.Collections.Generic;

namespace Logic
{
    public class PartyGenerator
    {
        public static Party GenerateRandomParty()
        {
            var result = new Party();
            var rng = new Random();
            for (var i = 0; i < 5; i++)
            {
                result.Bodies.Add(new Body());
            }
            var partAmount = rng.Next( 5 * result.Bodies.Count);
            var parts = new List<Part>();

            for (var i = 0; i < partAmount; i++)
            {
                result.Bodies[rng.Next(result.Bodies.Count)].Slots[rng.Next(5)].AssignedPart =
                    PartGenerator.GeneratePart();
            }

            return result;
        }
    }
}