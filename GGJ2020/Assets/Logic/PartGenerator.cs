using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Random = System.Random;

namespace Logic
{
    public class PartGenerator
    {
        private static Random rnd = new Random();

        private static Dictionary<Rarity, float> rarityModifiers = new Dictionary<Rarity, float>()
        {
            {Rarity.COMMON, 1f},
            {Rarity.UNCOMMON, 1.3f},
            {Rarity.RARE, 1.5f},
            {Rarity.LEGENDARY, 2.3f}
        };

        private static float NextFloat()
        {
            double u1 = 1.0 - rnd.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rnd.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = Math.Abs((randStdNormal));
            return (float) (randNormal * 100);
        }

        private static Stats GenerateStats()
        {
            return new Stats()
            {
                Speed = NextFloat(),
                Vitality = NextFloat(),
                Intelligence = NextFloat(),
                Strength = NextFloat(),
                Dexterity = NextFloat(),
                Charisma = NextFloat(),
                Durability = NextFloat(),
            };
        }

        public static Part GeneratePart()
        {
            Part newPart = new Part();

            int maxValue = (int) Enum.GetValues(typeof(PartType)).Cast<PartType>().Max();
            newPart.Type = (PartType) rnd.Next(maxValue + 1);
            newPart.Description = "test description"; //TODO
            newPart.Name = newPart.Type.ToString(); //TODO
            newPart.Stats = GenerateStats();
            return newPart;
        }

        public static Part GeneratePart(Rarity rarity, PartType type)
        {
            Part part = new Part();
            part.Type = type;
            part.Stats = GenerateStats();
            var t = part.Stats.GetType();
            foreach (PropertyInfo info in t.GetProperties())
            {
                if (info.CanWrite)
                {
                    info.SetValue(part.Stats, (float)info.GetValue(part.Stats) * rarityModifiers[rarity] );
                }
            }

            part.Name = rarity.ToString() + " " + type.ToString();
            return part;

        }
    }
}