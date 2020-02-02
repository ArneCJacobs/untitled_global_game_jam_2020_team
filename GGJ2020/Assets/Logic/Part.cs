
namespace Logic
{
    public enum VisualPartType
    {
        HUMAN_TORSO,
        ZOMBIE_TORSO,

        ZOMBIE_HEAD,
        ZOMBIE_LEFTARM,
        ZOMBIE_LEFTLEG,
        ZOMBIE_RIGHTARM,
        ZOMBIE_RIGHTLEG,

        HUMAN_HEAD,
        HUMAN_LEFTARM,
        HUMAN_LEFTLEG,
        HUMAN_RIGHTARM,
        HUMAN_RIGHTLEG,

    }

    public class Part
    {
        public string Name { get; set; } = "";
        public Stats Stats;
        public string Description;
        public PartType Type;
        public Rarity Rarity;
        public VisualPartType VisualType;
    }
}
