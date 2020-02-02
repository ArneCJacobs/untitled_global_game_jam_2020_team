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
        
        
        private static Dictionary<PartType,(float speed, float vitality, float intelligence, float strength, float dexterity, float charisma, float durability)> statModifiersPerType = new Dictionary<PartType, (float, float, float, float, float, float, float)>()
        {
            {PartType.HEAD, (0, 0.2f, 1.0f, 0, 0, 1, 1)},
            {PartType.TORSO, (0.5f, 1.2f, 0, 0.5f, 0.2f, 0, 1)},
            {PartType.LEFTARM, (0.7f, 0.3f, 0, 1, 1, 0, 1)},
            {PartType.RIGHTARM, (0.7f, 0.3f, 0, 1, 1, 0, 1)},
            {PartType.LEFTLEG, (1, 0.3f, 0, 0.6f, 0.3f, 0, 1)},
            {PartType.LEFTLEG, (1, 0.3f, 0, 0.6f, 0.3f, 0, 1)},
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
                newPart.VisualType = (VisualPartType)rnd.Next(2, maxValue + 1);


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

            PartFactoryType factoryType = GuiHelpers.GetPartTypeDetails(newPart.VisualType);
            newPart.Type = factoryType.Type;
            newPart.Description = "test description"; //TODO
            newPart.Name = newPart.VisualType.ToString(); //TODO
            GenerateStats(newPart.Type);

            return newPart;
        }

        private static Stats GenerateStats(PartType type)
        {
            (float speed, float vitality, float intelligence, float strength, float dexterity, float charisma,
                float durability) = statModifiersPerType[type];
            return new Stats()
            {
                Speed = NextFloat() * speed,
                Vitality = NextFloat() *  vitality,
                Intelligence = NextFloat() * intelligence,
                Strength = NextFloat() * strength,
                Dexterity = NextFloat() * dexterity,
                Charisma = NextFloat() * charisma,
                Durability = NextFloat() * durability,
            };
        }

        public static Part GeneratePart()
        {
            Part newPart = new Part();

            int maxValue = (int) Enum.GetValues(typeof(VisualPartType)).Cast<VisualPartType>().Max();

            newPart.VisualType = (VisualPartType) rnd.Next(2, maxValue + 1);
            newPart.Type = GuiHelpers.GetPartTypeDetails(newPart.VisualType).Type;
            newPart.Description = "test description"; //TODO
            newPart.Name = newPart.Type.ToString(); //TODO
            newPart.Stats = GenerateStats(newPart.Type);
            return newPart;
        }

        public static Part GeneratePart(Rarity rarity, VisualPartType type)
        {
            var typedets = GuiHelpers.GetPartTypeDetails(type);
            Part part = new Part();
            part.Type = typedets.Type;
            part.Stats = GenerateStats(part.Type);
            part.Stats *= rarityModifiers[rarity];
            part.Name = rarity.ToString() + " " + type.ToString();
            return part;

        }
    }
}