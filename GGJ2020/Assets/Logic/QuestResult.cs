using System.Collections.Generic;

namespace Logic
{
    public class QuestResult
    {
        public Party ReturnParty { get; set; }
        private List<Part> Loot { get; set; }
        public float Gold {get; set;}

        public QuestResult()
        {
            Loot = new List<Part>();
        }

    }
}