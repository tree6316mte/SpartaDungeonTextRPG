namespace TextRPG
{
    public class Player : Character
    {
        public Player(int _level, string _name, string _job, int _attack, int _armor, int _health, int _gold):base(_level, _name, _job, _attack, _armor, _health, _gold)
        {

        }
        
        public Player():base(1, "Chad", "전사", 10, 5, 100, 1500)
        {

        }


        public void Information()
        {
            Console.WriteLine($"Lv. {level.ToString().PadLeft(2, '0')}\n{name} ({job})\n공격력 : {attack}\n방어력 : {armor}\n체 력 : {health}\nGold : {gold}\n");
        }

        public void Inventory(){
            Console.WriteLine("[아이템 목록]");
            // TODO 보유중인 [아이템 목록] 보여주기
        }
    }
}