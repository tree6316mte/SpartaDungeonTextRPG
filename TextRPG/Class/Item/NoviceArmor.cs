namespace TextRPG
{
    public class NoviceArmor : Item
    {
        public NoviceArmor():base(1, 1000, "수련자 갑옷", "방어력 +5","수련에 도움을 주는 갑옷입니다.", ItemType.Cloth){}

        public override void AddEquipment(Player player){
            base.AddEquipment(player);
            player.armorCurrent += 5;
        }

        public override void RemoveEquipment(Player player){
            base.RemoveEquipment(player);
            player.armorCurrent -= 5;
        }
    }
}