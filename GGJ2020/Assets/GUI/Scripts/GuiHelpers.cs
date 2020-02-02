using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.GUI
{
    public struct PartFactoryType
    {
        public string AssetName;
        public string BeltAssetName;
        public float SizeModifier;
        public float RotationEuler;
        public float YOffset;
        public float ZOffset;
        public PartType Type;
    }

    static class GuiHelpers
    {
        static Dictionary<VisualPartType, PartFactoryType> m_partsDict = new Dictionary<VisualPartType, PartFactoryType>()
        {
            { VisualPartType.ZOMBIE_HEAD,        new PartFactoryType() {Type = PartType.HEAD,    AssetName = "Textures/BodyParts/Zombie/Zombie_Head_01"          ,BeltAssetName = "Textures/BodyParts/Zombie/Zombie_Head_Belt_01"      ,SizeModifier = 1.0f, RotationEuler = 10.0f, YOffset = -1.3f ,ZOffset = -0.25f        } },
            { VisualPartType.ZOMBIE_LEFTARM,     new PartFactoryType() {Type = PartType.LEFTARM, AssetName = "Textures/BodyParts/Zombie/Zombie_Arm_Left_01"      ,BeltAssetName = "Textures/BodyParts/Zombie/Zombie_Arm_Left_Belt_01"  ,SizeModifier = 0.4f, RotationEuler = 85.0f, YOffset = -2.2f ,ZOffset = -0.25f        } },
            { VisualPartType.ZOMBIE_LEFTLEG,     new PartFactoryType() {Type = PartType.LEFTLEG, AssetName = "Textures/BodyParts/Zombie/Zombie_Leg_Left_01"      ,BeltAssetName = "Textures/BodyParts/Zombie/Zombie_Leg_Left_Belt_01"  ,SizeModifier = 0.3f, RotationEuler = 90.0f, YOffset = -2.2f ,ZOffset = 0.25f        } },
            { VisualPartType.ZOMBIE_RIGHTARM,    new PartFactoryType() {Type = PartType.RIGHTARM,AssetName = "Textures/BodyParts/Zombie/Zombie_Arm_Right_01"     ,BeltAssetName = "Textures/BodyParts/Zombie/Zombie_Arm_Right_Belt_01" ,SizeModifier = 0.4f, RotationEuler = 85.0f, YOffset = -2.2f ,ZOffset = -0.25f        } },
            { VisualPartType.ZOMBIE_RIGHTLEG,    new PartFactoryType() {Type = PartType.RIGHTLEG,AssetName = "Textures/BodyParts/Zombie/Zombie_Leg_Right_01"     ,BeltAssetName = "Textures/BodyParts/Zombie/Zombie_Leg_Right_Belt_01" ,SizeModifier = 0.3f, RotationEuler = -95.0f, YOffset = -2.2f,ZOffset = 0.25f        } },
            { VisualPartType.ZOMBIE_TORSO,       new PartFactoryType() {Type = PartType.TORSO,   AssetName = "Textures/BodyParts/Zombie/Zombie_Torso_01"         ,BeltAssetName = "Textures/BodyParts/Zombie/Zombie_Torso_01"          ,SizeModifier = 0.3f, RotationEuler = 0.0f, YOffset = 0.0f   ,ZOffset = -0.1f        } },

            { VisualPartType.ZOMBIE_HEAD_FEMALE, new PartFactoryType() {Type = PartType.HEAD,    AssetName = "Textures/BodyParts/Zombie/Zombie_Head_Female_01"  ,BeltAssetName = "Textures/BodyParts/Zombie/Zombie_Head_Female_Belt_01" ,SizeModifier = 1.0f, RotationEuler = 10.0f, YOffset = -1.3f ,ZOffset = -0.25f        } },
            { VisualPartType.ZOMBIE_TORSO_FEMALE, new PartFactoryType() {Type = PartType.TORSO,   AssetName = "Textures/BodyParts/Zombie/Zombie_Torso_Female_01" ,BeltAssetName = "Textures/BodyParts/Zombie/Zombie_Torso_Female_01"    ,SizeModifier = 0.3f, RotationEuler = 0.0f, YOffset = 0.0f   ,ZOffset = -0.1f        } },

            { VisualPartType.HUMAN_HEAD,        new PartFactoryType() {Type = PartType.HEAD,    AssetName = "Textures/BodyParts/Human/Human_Head_01"          ,BeltAssetName = "Textures/BodyParts/Human/Human_Head_Belt_01"      ,SizeModifier = 1.0f, RotationEuler = 10.0f, YOffset = -1.3f  ,ZOffset = -0.25f        } },
            { VisualPartType.HUMAN_LEFTARM,     new PartFactoryType() {Type = PartType.LEFTARM, AssetName = "Textures/BodyParts/Human/Human_Arm_Left_01"      ,BeltAssetName = "Textures/BodyParts/Human/Human_Arm_Left_Belt_01"  ,SizeModifier = 0.4f, RotationEuler = 85.0f, YOffset = -2.2f  ,ZOffset = -0.25f        } },
            { VisualPartType.HUMAN_LEFTLEG,     new PartFactoryType() {Type = PartType.LEFTLEG, AssetName = "Textures/BodyParts/Human/Human_Leg_Left_01"      ,BeltAssetName = "Textures/BodyParts/Human/Human_Leg_Left_Belt_01"  ,SizeModifier = 0.3f, RotationEuler = 90.0f, YOffset = -2.2f  ,ZOffset = 0.25f         } },
            { VisualPartType.HUMAN_RIGHTARM,    new PartFactoryType() {Type = PartType.RIGHTARM,AssetName = "Textures/BodyParts/Human/Human_Arm_Right_01"     ,BeltAssetName = "Textures/BodyParts/Human/Human_Arm_Right_Belt_01" ,SizeModifier = 0.4f, RotationEuler = 85.0f, YOffset = -2.2f  ,ZOffset = -0.25f        } },
            { VisualPartType.HUMAN_RIGHTLEG,    new PartFactoryType() {Type = PartType.RIGHTLEG,AssetName = "Textures/BodyParts/Human/Human_Leg_Right_01"     ,BeltAssetName = "Textures/BodyParts/Human/Human_Leg_Right_Belt_01" ,SizeModifier = 0.3f, RotationEuler = -95.0f, YOffset = -2.2f ,ZOffset = 0.25f         } },
            { VisualPartType.HUMAN_TORSO,       new PartFactoryType() {Type = PartType.TORSO,   AssetName = "Textures/BodyParts/Human/Human_Torso_01"         ,BeltAssetName = "Textures/BodyParts/Human/Human_Torso_01"          ,SizeModifier = 0.3f, RotationEuler = 0.0f, YOffset = 0.0f    ,ZOffset = -0.1f          } },

        };

        public static PartFactoryType GetPartTypeDetails(VisualPartType partType)
        {
            return m_partsDict[partType];
        }

        public static VisualPartType GetRandomPartForCategory(PartType type)
        {
            var items = m_partsDict.Where(o => o.Value.Type == type).ToArray();
            var rnd = new System.Random();
            return items[rnd.Next(0, items.Count())].Key;
        }
    }
}
