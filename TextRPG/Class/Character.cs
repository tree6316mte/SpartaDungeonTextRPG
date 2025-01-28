namespace TextRPG
{
    public enum CharacterJob
    {
        None,
        Worrier,
        Thief
    }

    public class Character
    {
        public int level;
        public string name;
        public string job;
        public int attack;
        public int attackCurrent;
        public int armor;
        public int armorCurrent;
        public int health;
        public int healthCurrent;
        public int gold;
        public bool isDead { get { return health <= 0; } }

        public Character(int _level, string _name, string _job, int _attack, int _armor, int _health, int _gold)
        {
            level = _level;
            name = _name;
            job = _job;
            attack = _attack;
            attackCurrent = _attack;
            armor = _armor;
            armorCurrent = _armor;
            health = _health;
            healthCurrent = _health;
            gold = _gold;
        }
    }
}