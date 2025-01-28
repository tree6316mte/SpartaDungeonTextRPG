
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
                    m_items.Add(new SpartaMace());
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
                    Console.WriteLine($" | 구매완료");
                } else {
                    Console.WriteLine($" | {items[i].gold} G");
                }
            }

            Console.WriteLine("\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n");
            SceneManager.instance.Menu(ShopMain, GameManager.instance.GameMain, ShopItemBuy, ShopItemSell);
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
                actions.Add(()=>BuyItem(temp));
            }

            Console.WriteLine("\n0. 나가기\n");
            SceneManager.instance.Menu(ShopItemBuy, actions.ToArray());
        }

        public void BuyItem(int _id){

            var playerItems = GameManager.instance.player.items;
            bool exists = playerItems.Any(item => item.id == items[_id].id);

            if(exists)
            {
                Console.WriteLine($"이미 구매한 아이템입니다");
            }
            else
            {
                if(items[_id].gold <= GameManager.instance.player.gold){
                    Console.WriteLine($"구매를 완료했습니다");
                    GameManager.instance.player.gold -= items[_id].gold;
                    GameManager.instance.player.items.Add(items[_id]);

                } else {
                    Console.WriteLine($"Gold 가 부족합니다.");
                }
            }
            Thread.Sleep(1000);
            SceneManager.instance.GoMenu(ShopItemBuy);
        }
        
        public void ShopItemSell()
        {
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.instance.player.gold} G\n");

            List<Action> actions = new List<Action>();
            actions.Add(ShopMain); // 0번째는 나가기

            Console.WriteLine("[아이템 목록]");
            var sellItems = GameManager.instance.player.items;
            for(int i = 0; i < sellItems.Count; i++){
                Console.Write($"- {i+1} ");
                sellItems[i].ItemException();
                Console.WriteLine($" | {sellItems[i].gold * 85 / 100} G");

                int temp = i; // i값 변하기 때문에 지역변수로 캐싱해서 값 잡아둠
                actions.Add(()=>SellItem(temp));
            }

            Console.WriteLine("\n0. 나가기\n");
            SceneManager.instance.Menu(ShopItemSell, actions.ToArray());
        }

        public void SellItem(int _i){
            var player = GameManager.instance.player;
            int gainGold = player.items[_i].gold * 85 / 100;

            Console.WriteLine($"{player.items[_i].name}이 판매되었습니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} (+{gainGold})");

            if(player.items[_i].isEquip){
                player.items[_i].RemoveEquipment(GameManager.instance.player);
            }

            player.gold += gainGold;
            player.items.RemoveAt(_i);
            Thread.Sleep(1000);
            SceneManager.instance.GoMenu(ShopItemSell);
        }
    }
}