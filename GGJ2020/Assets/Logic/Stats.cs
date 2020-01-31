namespace Logic
{
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
            var result = new Stats
            {
                Speed = first.Speed + second.Speed,
                Vitality = first.Vitality + second.Vitality,
                Intelligence = first.Intelligence + second.Intelligence,
                Strength = first.Strength + second.Strength,
                Dexterity = first.Dexterity + second.Dexterity,
                Charisma = first.Charisma + second.Charisma,
                Durability = first.Durability + second.Durability
            };
            return result;
        }

        public static Stats operator *(Stats first, float scalar)
        {
            var result = new Stats
            {
                Speed = first.Speed * scalar,
                Vitality = first.Vitality * scalar,
                Intelligence = first.Intelligence * scalar,
                Strength = first.Strength * scalar,
                Dexterity = first.Dexterity * scalar,
                Charisma = first.Charisma * scalar,
                Durability = first.Durability * scalar
            };
            return result;

        }

        public static Stats operator /(Stats first, float scalar)
        {
            var result = new Stats
            {
                Speed = first.Speed / scalar,
                Vitality = first.Vitality / scalar,
                Intelligence = first.Intelligence / scalar,
                Strength = first.Strength / scalar,
                Dexterity = first.Dexterity / scalar,
                Charisma = first.Charisma / scalar,
                Durability = first.Durability / scalar
            };
            return result;

        }
    }
}