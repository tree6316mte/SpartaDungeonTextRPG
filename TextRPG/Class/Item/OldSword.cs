namespace TextRPG
{
    public class OldSword : Item
    {
        public OldSword():base(4, 600, "낡은 검", "공격력 +2","쉽게 볼 수 있는 낡은 검 입니다.", ItemType.Weapon){}

        public override void AddEquipment(Player player){
            base.AddEquipment(player);
            player.attackCurrent += 2;
        }

        public override void RemoveEquipment(Player player){
            base.RemoveEquipment(player);
            player.attackCurrent -= 2;
        }
    }
}