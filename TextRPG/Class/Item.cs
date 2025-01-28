namespace TextRPG
{
    public enum ItemType
    {
        None,
        Weapon,
        Cloth,
        Consumable,
    }

    public abstract class Item
    {
        public int id;
        public int gold;
        public string name;
        public string effect;
        public string exception;
        public ItemType itemType;
        public bool isEquip;

        public Item(int _id, int _gold, string _name, string _effect, string _exception, ItemType _itemType)
        {
            id = _id;
            gold = _gold;
            name = _name;
            effect = _effect;
            exception = _exception;
            itemType = _itemType;
            isEquip = false;
        }

        public void ItemException()
        {
            Console.Write($"{name} | {effect} | {exception}");
        }

        public virtual void AddEquipment(Player player){
            isEquip = true;
        }
        public virtual void RemoveEquipment(Player player){
            isEquip = false;
        }
    }
}