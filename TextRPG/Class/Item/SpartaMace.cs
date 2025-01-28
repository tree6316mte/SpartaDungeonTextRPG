namespace TextRPG
{
    public class SpartaMace : Item
    {
        public SpartaMace():base(7, 0, "스파르타 메이스", "공격력 +30 방어력 -5","던전이 힘들면 구매하세요 공짜 입니다...", ItemType.Weapon){}

        public override void AddEquipment(Player player){
            base.AddEquipment(player);
            player.attackCurrent += 30;
            player.armorCurrent -= 5;
        }

        public override void RemoveEquipment(Player player){
            base.RemoveEquipment(player);
            player.attackCurrent -= 30;
            player.armorCurrent += 5;
        }
    }
}