
namespace Logic
{
    public class Part
    {
        public string Name { get; set; } = "";
        public Stats Stats { get; set; } = new Stats();
        public string Description { get; set; } = "test";
        public PartType Type { get; set; } = PartType.HEAD;

    }
}
