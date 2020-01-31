public class Quest
{
    public string Description;

    public string Title;
    public float Timelimit;

    public float Difficulty;

    public float CalculateResult(Party party)
    {
        float counter = 0;
        foreach (Body body in party.Bodies)
        {
            counter += body.CalculateStats().Vitality;
        }
        return counter;
    }
    public Reward GetReward()
    {

    }


}