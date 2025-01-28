namespace TextRPG
{
    public class BronzeAx : Item
    {
        public BronzeAx():base(5, 1500, "청동 도끼", "공격력 +5","어디선가 사용됐던거 같은 도끼입니다.", ItemType.Weapon){}

        public override void AddEquipment(Player player){
            base.AddEquipment(player);
            player.attackCurrent += 5;
        }

        public override void RemoveEquipment(Player player){
            base.RemoveEquipment(player);
            player.attackCurrent -= 5;
        }
    }
}