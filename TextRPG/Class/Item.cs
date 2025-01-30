namespace TextRPG
{
    public enum ItemType
    {
        None,
        Weapon,
        Cloth,
        Consumable,
    }

    public class Item
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

        public Item() {
            id = 0;
            gold = 0;
            name = "0";
            effect = "0";
            exception = "0";
            itemType = 0;
            isEquip = false;
        }

        public void ItemException()
        {
            Console.Write($"{name} | {effect} | {exception}");
        }

        public virtual void AddEquipment(Player player)
        {
            isEquip = true;
            switch (itemType)
            {
                case ItemType.Weapon:
                    if (player.weapon != -1){
                        var item = player.items.FirstOrDefault(item => item.id == player.weapon);
                        item?.RemoveEquipment(player);
                    }
                    player.weapon = id;
                    break;
                case ItemType.Cloth:
                    if (player.cloth != -1){
                        var item = player.items.FirstOrDefault(item => item.id == player.cloth);
                        item?.RemoveEquipment(player);
                    }
                    player.cloth = id;
                    break;

            }
        }
        public virtual void RemoveEquipment(Player player)
        {
            isEquip = false;
            switch (itemType)
            {
                case ItemType.Weapon:
                    player.weapon = -1;
                    break;
                case ItemType.Cloth:
                    player.cloth = -1;
                    break;

            }
        }
    }
}