namespace TextRPG
{
    public class IronArmor : Item
    {
        public IronArmor():base(2, 2000, "무쇠 갑옷", "방어력 +9","무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.Cloth){}

        public override void AddEquipment(ref Player player){
            player.armor += 9;
        }

        public override void RemoveEquipment(ref Player player){
            player.armor -= 9;
        }
    }
}