using Game.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
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

        public static Part GeneratePart(PartType? type = null, VisualPartType? visualPartType = null)
        {
            Part newPart = new Part();

            if (!type.HasValue)
            {
                int maxValue = (int)Enum.GetValues(typeof(VisualPartType)).Cast<VisualPartType>().Max();
                newPart.VisualType = (VisualPartType)rnd.Next(3, maxValue + 1);


            }
            else if (type.HasValue && !visualPartType.HasValue)
                newPart.VisualType = GuiHelpers.GetRandomPartForCategory(type.Value);
            else
            {
                if (visualPartType.HasValue)
                {
                    newPart.VisualType = visualPartType.Value;
                }
            }

            newPart.Description = "test description"; //TODO
            newPart.Name = newPart.VisualType.ToString(); //TODO
            GenerateStats();

            return newPart;
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

            int maxValue = (int) Enum.GetValues(typeof(VisualPartType)).Cast<VisualPartType>().Max();

            newPart.VisualType = (VisualPartType) rnd.Next(3, maxValue + 1);
            newPart.Type = GuiHelpers.GetPartTypeDetails(newPart.VisualType).Type;
            newPart.Description = "test description"; //TODO
            newPart.Name = newPart.Type.ToString(); //TODO
            newPart.Stats = GenerateStats();
            return newPart;
        }

        public static Part GeneratePart(Rarity rarity, VisualPartType type)
        {
            var typedets = GuiHelpers.GetPartTypeDetails(type);
            Part part = new Part();
            part.Type = typedets.Type;
            part.Stats = GenerateStats();
            part.Stats *= rarityModifiers[rarity];
            part.Name = rarity.ToString() + " " + type.ToString();
            return part;

        }
    }
}