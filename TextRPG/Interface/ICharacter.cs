namespace TextRPG
{
    public interface ICharacter
    {
        int level { get; set; }
        string name { get; set; }
        int health { get; set; }
        int attack { get; set; }
        bool isDead { get; }

        // void TakeDamage(int damage);
    }
}