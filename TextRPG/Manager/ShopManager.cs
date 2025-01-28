
namespace TextRPG
{
    class ShopManager : Helper.Singleton<ShopManager>
    {
        private List<Item>? m_items;
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
            Console.WriteLine($"{GameManager.instance.player.gold} G\n");
            
            Console.WriteLine("[아이템 목록]");
            for(int i = 0; i < items.Count; i++){
                Console.Write($"- ");
                items[i].ItemException();

                var playerItems = GameManager.instance.player.items;
                bool exists = playerItems.Any(item => item.id == items[i].id);
                if(exists){
                    Console.WriteLine($"구매완료");
                } else {
                    Console.WriteLine($"{items[i].gold} G");
                }
            }

            Console.WriteLine("\n1. 아이템 구매\n0. 나가기\n");
            SceneManager.instance.Menu(ShopMain, GameManager.instance.GameMain, ShopItemBuy);
        }
        
        public void ShopItemBuy()
        {
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.instance.player.gold} G\n");

            List<Action> actions = new List<Action>();
            actions.Add(ShopMain); // 0번째는 나가기

            Console.WriteLine("[아이템 목록]");
            for(int i = 0; i < items.Count; i++){
                Console.Write($"- {i+1} ");
                items[i].ItemException();

                var playerItems = GameManager.instance.player.items;
                bool exists = playerItems.Any(item => item.id == items[i].id);
                if(exists){
                    Console.WriteLine($" | 구매완료");
                } else {
                    Console.WriteLine($" | {items[i].gold} G");
                }

                int temp = i; // i값 변하기 때문에 지역변수로 캐싱해서 값 잡아둠
                actions.Add(()=>BuyItem(temp+1));
            }

            Console.WriteLine("\n0. 나가기\n");
            SceneManager.instance.Menu(ShopItemBuy, actions.ToArray());
        }

        public void BuyItem(int _id){
            Console.WriteLine($"{_id} 아이고난");
            Thread.Sleep(1000);
            SceneManager.instance.GoMenu(ShopItemBuy);
        }
    }
}