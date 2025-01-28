namespace TextRPG
{
    public class SpartaArmor : Item
    {
        public SpartaArmor():base(3, 3500, "스파르타의 갑옷", "방어력 +15","스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", ItemType.Cloth){}

        public override void AddEquipment(ref Player player){
            player.armor += 15;
        }

        public override void RemoveEquipment(ref Player player){
            player.armor -= 15;
        }
    }
}