using Game.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace Logic
{
    public class PartGenerator
    {
        private static int amount = 100;
        private static float mean = calculateMean();
        private static Random rnd = new Random();

        private static float calculateMean()
        {
            float total = 0;
            for (int i = 0; i < amount; i += 1)
            {
                total += NextFloat();
            }

            return total / amount;
        }

        private static Dictionary<Rarity, float> rarityModifiers = new Dictionary<Rarity, float>()
        {
            {Rarity.COMMON, 1f},
            {Rarity.UNCOMMON, 1.3f},
            {Rarity.RARE, 1.5f},
            {Rarity.LEGENDARY, 2.3f}
        };

        private static
            Dictionary<PartType, (float speed, float vitality, float intelligence, float strength, float dexterity,
                float charisma, float durability)> statModifiersPerType =
                new Dictionary<PartType, (float, float, float, float, float, float, float)>()
                {
                    {PartType.HEAD, (0, 0.01f, 1.0f, 0, 0, 1, 1)},
                    {PartType.TORSO, (0.1f, 1.2f, 0, 0.5f, 0.2f, 0, 1)},
                    {PartType.LEFTARM, (0.7f, 0.3f, 0, 1, 1, 0, 1)},
                    {PartType.RIGHTARM, (0.7f, 0.3f, 0, 1, 1, 0, 1)},
                    {PartType.LEFTLEG, (1, 0.3f, 0, 0.6f, 0.3f, 0, 1)},
                    {PartType.RIGHTLEG, (1, 0.3f, 0, 0.6f, 0.3f, 0, 1)},
                };

        private static float NextFloat()
        {
            if (rnd == null)
                rnd = new Random();

            double u1 = 1.0 - rnd.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rnd.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = Math.Abs((randStdNormal));
            return (float) (randNormal * 100);
        }

        private static Stats GenerateStats(PartType type)
        {
            (float speed, float vitality, float intelligence, float strength, float dexterity, float charisma,
                float durability) = statModifiersPerType[type];
            return new Stats()
            {
                Speed = NextFloat() * speed,
                Vitality = NextFloat() * vitality,
                Intelligence = NextFloat() * intelligence,
                Strength = NextFloat() * strength,
                Dexterity = NextFloat() * dexterity,
                Charisma = NextFloat() * charisma,
                Durability = NextFloat() * durability,
            };
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
            newPart.Stats = GenerateStats(newPart.Type);
            newPart.Name = GenerateName(newPart);

            return newPart;
        }

        private static string GenerateName(Part part)
        {
            
            float max = 0;
            string maxStat = null;
            Stats stats = part.Stats;

            FieldInfo[] properties = typeof(Stats).GetFields();

            foreach (FieldInfo property in properties)
            {
                float value = (float)property.GetValue(stats);
                // float value = 0;
                if (value > max)
                {
                    max = value;
                    maxStat = property.Name.ToString();
                }
            }


            float ratio = max / mean;
            string rarity;
            if (ratio <= 1)
            {
                rarity = "Common";
            } else if (ratio < 2)
            {
                rarity = "Uncommon";
            } else if (ratio < 3)
            {
                rarity = "Rare";
            }
            else
            {
                rarity = "Legendary";
            }


            return rarity + " " + part.Type + " of " + maxStat;
        }


        // public static Part GeneratePart()
        // {
        //     Part newPart = new Part();
        //
        //     int maxValue = (int) Enum.GetValues(typeof(VisualPartType)).Cast<VisualPartType>().Max();
        //
        //     newPart.VisualType = (VisualPartType) rnd.Next(2, maxValue + 1);
        //     newPart.Type = GuiHelpers.GetPartTypeDetails(newPart.VisualType).Type;
        //     newPart.Description = "test description"; //TODO
        //     newPart.Name = newPart.Type.ToString(); //TODO
        //     newPart.Stats = GenerateStats(newPart.Type);
        //     return newPart;
        // }

        // public static Part GeneratePart(Rarity rarity, VisualPartType type)
        // {
        //     var typedets = GuiHelpers.GetPartTypeDetails(type);
        //     Part part = new Part();
        //     part.Type = typedets.Type;
        //     part.Stats = GenerateStats(part.Type);
        //     part.Stats *= rarityModifiers[rarity];
        //     part.Name = rarity.ToString() + " " + type.ToString();
        //     return part;
        //
        // }
    }
}