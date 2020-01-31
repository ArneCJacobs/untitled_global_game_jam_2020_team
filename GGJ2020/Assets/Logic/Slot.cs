using System.Collections.Generic;

namespace Logic
{
    public class Slot
    {
        public List<PartType> AcceptedTypes { get; set; }
        public PartType Preferred { get; set; } 
        public Part AssignedPart { get; set; }

        public Slot()
        {
            AcceptedTypes = new List<PartType>();
            Preferred = PartType.HEAD;
        }
    }
}
