namespace TextRPG
{
    public class SpartaSpear : Item
    {
        public SpartaSpear():base(6, 2500, "스파르타의 창", "공격력 +7","스파르타의 전사들이 사용했다는 전설의 창입니다.", ItemType.Weapon){}

        public override void AddEquipment(ref Player player){
            player.attack += 7;
        }

        public override void RemoveEquipment(ref Player player){
            player.attack -= 7;
        }
    }
}