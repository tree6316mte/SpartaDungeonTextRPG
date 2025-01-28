namespace TextRPG
{
    public class IronArmor : Item
    {
        public IronArmor():base(2, 2000, "무쇠 갑옷", "방어력 +9","무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.Cloth){}

        public override void AddEquipment(Player player){
            base.AddEquipment(player);
            player.armorCurrent += 9;
        }

        public override void RemoveEquipment(Player player){
            base.RemoveEquipment(player);
            player.armorCurrent -= 9;
        }
    }
}