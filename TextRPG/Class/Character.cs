namespace TextRPG
{
    public class Character : ICharacter
    {
        public int level { get; set; }
        public string name { get; set; }
        public string job { get; set; }
        public int attack { get; set; }
        public int armor { get; set; }
        public int health { get; set; }
        public int gold { get; set; }
        public bool isDead { get { return health <= 0; } }

        public Character(int _level, string _name, string _job, int _attack, int _armor, int _health, int _gold)
        {
            level = _level;
            name = _name;
            job = _job;
            attack = _attack;
            armor = _armor;
            health = _health;
            gold = _gold;
        }
    }
}