using System.Collections.Generic;

namespace Logic
{
    public class Slot
    {
        private List<PartType> AcceptedTypes { get; set; }
        private PartType Preferred { get; set; } 
        public Part AssignedPart { get; set; }

        public Slot()
        {
            AcceptedTypes = new List<PartType>();
            Preferred = PartType.HEAD;
        }
    }
}
