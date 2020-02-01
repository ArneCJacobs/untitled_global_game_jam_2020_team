using System;
using System.Collections.Generic;
using Logic.Quests;

namespace Logic
{
    public class QuestGenerator
    {
        private List<Func<float, Quest>> questList;

        public QuestGenerator()
        {
            questList = new List<Func<float, Quest>>
            {
                 (d) => new BasicQuest(d),
                 (d) => new ChickenQuest(d)
            };
        }

        public Quest GenerateQuest(float difficulty)
        {
            var random = new Random();
            return questList[random.Next(questList.Count)](difficulty);
        }
    }
}