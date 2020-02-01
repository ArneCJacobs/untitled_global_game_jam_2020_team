using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GUI
{
    static class GuiHelpers
    {
        static Dictionary<PartType, PartFactoryType> m_partsDict = new Dictionary<PartType, PartFactoryType>()
        {
            { PartType.HEAD,  new PartFactoryType() { AssetName = "Textures/BodyParts/Zombie/Zombie_Head_01", SizeModifier = 1.0f, RotationEuler = 10.0f, YOffset = 0.0f } },
            { PartType.LEFTARM,  new PartFactoryType() { AssetName = "Textures/BodyParts/Zombie/Zombie_Arm_Left_01", SizeModifier = 0.4f, RotationEuler = 85.0f, YOffset = 0.0f } },
            { PartType.LEFTLEG,  new PartFactoryType() { AssetName = "Textures/BodyParts/Zombie/Zombie_Leg_Left_01", SizeModifier = 0.3f, RotationEuler = 90.0f, YOffset = 2.0f } },
            { PartType.RIGHTARM,  new PartFactoryType() { AssetName = "Textures/BodyParts/Zombie/Zombie_Arm_Right_01", SizeModifier = 0.4f, RotationEuler = 85.0f, YOffset = 2.0f } },
            { PartType.RIGHTLEG,  new PartFactoryType() { AssetName = "Textures/BodyParts/Zombie/Zombie_Leg_Right_01", SizeModifier = 0.3f, RotationEuler = -95.0f, YOffset = 2.0f } },

        };

        public static PartFactoryType GetPartTypeDetails(PartType partType)
        {
            return m_partsDict[partType];
        }
    }

    public struct PartFactoryType
    {
        public string AssetName;
        public float SizeModifier;
        public float RotationEuler;
        public float YOffset;
    }
}
