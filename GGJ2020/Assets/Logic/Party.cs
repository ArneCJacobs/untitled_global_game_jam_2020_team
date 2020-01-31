using System.Collections.Generic;

namespace Logic
{
    public class Party 
    {
        public List<Body> Bodies { get; set; }
        public Party()
        {
            Bodies = new List<Body>();
        }
        public Stats GetAverageStats()
        {
            var counter = new Stats();
            foreach (var body in Bodies)
            {
                counter += body.CalculateStats();
            }
            return counter / Bodies.Count;
        }
    }
}
