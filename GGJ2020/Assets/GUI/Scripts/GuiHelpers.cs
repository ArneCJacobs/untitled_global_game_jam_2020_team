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
            { PartType.HEAD,  new PartFactoryType() { AssetName = "Textures/BodyParts/Zombie/Zombie_Head_01" } },
            { PartType.LEFTARM,  new PartFactoryType() { AssetName = "Textures/BodyParts/Zombie/Zombie_Arm_Left_01" } },
            { PartType.LEFTLEG,  new PartFactoryType() { AssetName = "Textures/BodyParts/Zombie/Zombie_Leg_Left_01" } },
            { PartType.RIGHTARM,  new PartFactoryType() { AssetName = "Textures/BodyParts/Zombie/Zombie_Arm_Right_01" } },
            { PartType.RIGHTLEG,  new PartFactoryType() { AssetName = "Textures/BodyParts/Zombie/Zombie_Leg_Right_01" } },

        };

        public static PartFactoryType GetPartTypeDetails(PartType partType)
        {
            return m_partsDict[partType];
        }
    }

    public struct PartFactoryType
    {
        public string AssetName;
    }
}
