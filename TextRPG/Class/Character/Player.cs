namespace TextRPG
{
    public class Player : Character
    {
        public List<Item> items = new List<Item>();


        public Player(int _level, string _name, string _job, int _attack, int _armor, int _health, int _gold):base(_level, _name, _job, _attack, _armor, _health, _gold)
        {
            
        }
        
        public Player():base(1, "Chad", "전사", 10, 5, 100, 2000)
        {
            
        }


        public void Information()
        {
            string? attackPlus = attackCurrent-attack > 0 ? " (+"+(attackCurrent-attack).ToString()+")" : attackCurrent-attack == 0 ? null : " ("+(attackCurrent-attack).ToString()+")";
            string? armorPlus = armorCurrent-armor > 0 ? " (+"+(armorCurrent-armor).ToString()+")" : armorCurrent-armor == 0 ? null : " ("+(armorCurrent-armor).ToString()+")";

            string levelText = level.ToString().PadLeft(2, '0');
            string nameText = $"{name} ({job})";
            string attackText = $"{attackCurrent}"+ attackPlus ?? "";
            string armorText = $"{armorCurrent}" + armorPlus ?? "";

            Console.WriteLine($"Lv. {levelText}\n{nameText}\n공격력 : {attackText}\n방어력 : {armorText}\n체 력 : {health}\nGold : {gold}\n");
        }

        public void Inventory(){
            Console.WriteLine("[아이템 목록]");
            // TODO 보유중인 [아이템 목록] 보여주기
        }
    }
}