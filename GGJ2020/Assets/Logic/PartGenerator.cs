using System;
using System.Linq;
using Random = System.Random;

namespace Logic
{
    public class PartGenerator
    {
        private static Random rnd = new Random();

        private static float NextFloat()
        {
            double u1 = 1.0-rnd.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0-rnd.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = Math.Abs((randStdNormal)); 
            return (float) (randNormal* 100);
        }

        public static Part GeneratePart(PartType? type = null)
        {
            Part newPart = new Part();

            if (!type.HasValue)
            {
                int maxValue = (int)Enum.GetValues(typeof(PartType)).Cast<PartType>().Max();
                newPart.Type = (PartType)rnd.Next(1,maxValue + 1);
            }
            else
                newPart.Type = type.Value;
            newPart.Description = "test description"; //TODO
            newPart.Name = newPart.Type.ToString(); //TODO
            newPart.Stats = new Stats()
            {
                Speed = NextFloat(),
                Vitality = NextFloat(),
                Intelligence = NextFloat(),
                Strength = NextFloat(),
                Dexterity = NextFloat(),
                Charisma = NextFloat(),
                Durability = NextFloat(),
            };
            return newPart;
        }
    }
}