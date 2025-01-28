
namespace TextRPG
{
    class ShopManager : Helper.Singleton<ShopManager>
    {
        private List<Item> m_items;
        public List<Item> items
        {
            get
            {
                if (m_items == null)
                {
                    m_items = new List<Item>();
                    m_items.Add(new NoviceArmor());
                    m_items.Add(new IronArmor());
                    m_items.Add(new SpartaArmor());
                    m_items.Add(new OldSword());
                    m_items.Add(new BronzeAx());
                    m_items.Add(new SpartaSpear());
                }
                return m_items;
            }
        }
        public void ShopMain()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.instance.player.gold} G");
            
            Console.WriteLine("[아이템 목록]");
            for(int i = 0; i < items.Count; i++){
                Console.Write($"- ");
                items[i].ItemException();

                var playerItems = GameManager.instance.player.items;
                bool exists = playerItems.Any(item => item.id == items[i].id);
                if(exists){
                    Console.WriteLine($"구매완료");
                } else {
                    Console.WriteLine($"{m_items[i].gold} G");
                }
            }

            Console.WriteLine("1. 아이템 구매\n0. 나가기\n");
            SceneManager.instance.Menu(ShopMain, GameManager.instance.GameMain, ShopItemBuy);
        }
        
        public void ShopItemBuy()
        {
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.instance.player.gold} G");
            
            Console.WriteLine("[아이템 목록]");
            for(int i = 0; i < m_items.Count; i++){
                Console.Write($"- {i}");
                items[i].ItemException();

                var playerItems = GameManager.instance.player.items;
                bool exists = playerItems.Any(item => item.id == items[i].id);
                if(exists){
                    Console.WriteLine($"구매완료");
                } else {
                    Console.WriteLine($"{m_items[i].gold} G");
                }
            }

            Console.WriteLine("0. 나가기\n");
            SceneManager.instance.Menu(ShopItemBuy, GameManager.instance.GameMain, ShopMain);
        }
    }
}