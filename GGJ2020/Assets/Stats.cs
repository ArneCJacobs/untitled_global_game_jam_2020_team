public class Stats
{
    public float Speed;
    public float Vitality;
    public float Intelligence;
    public float Strength;
    public float Dexterity;
    public float Charisma;
    public float Durability;

    public static Stats operator +(Stats first, Stats second)
    {
        Stats result = new Stats();
        result.Speed = first.Speed + second.Speed;
        result.Vitality = first.Vitality + second.Vitality;
        result.Intelligence = first.Intelligence + second.Intelligence;
        result.Strength = first.Strength + second.Strength;
        result.Dexterity = first.Dexterity + second.Dexterity;
        result.Charisma = first.Charisma + second.Charisma;
        result.Durability = first.Durability + second.Durability;
        return result;
    }

}