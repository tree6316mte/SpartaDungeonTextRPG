namespace TextRPG
{
    class GameManager : Helper.Singleton<GameManager>
    {
        public const int RestCost = 500;

        private Player? m_player;
        public Player player
        {
            get
            {
                if (m_player == null)
                {
                    m_player = new Player();
                }
                return m_player;
            }
        }

        public List<((string name, int armor) info, int gold)>? m_dungeon;
        public List<((string name, int armor) info, int gold)> dungeon
        {
            get
            {
                if (m_dungeon == null)
                {
                    m_dungeon = new List<((string, int),int)>();
                    m_dungeon.Add((("쉬운 던전", 5), 1000));
                    m_dungeon.Add((("일반 던전", 11), 1700));
                    m_dungeon.Add((("어려운 던전", 17), 2500));
                }
                return m_dungeon;
            }
        }
        
        public void GameSave()
        {
            SaveLoadManager.instance.SaveToJson(m_player);

            Console.WriteLine("게임 저장 완료!");


            Console.WriteLine("0. 완료\n");
            SceneManager.instance.Menu(GameSave, GameMain);
        }

        public void GameLoad()
        {
            m_player = SaveLoadManager.instance.LoadFromJson<Player>();

            Console.WriteLine("게임 불러오기 완료!");

            if(m_player == null){
                Console.WriteLine("저장된 데이터가 없어 불러오지 못했습니다.");

                Console.WriteLine("0. 완료\n");
                SceneManager.instance.Menu(GameLoad, GameMain);
            } else {
                Console.WriteLine("0. 완료\n");
                SceneManager.instance.Menu(GameLoad, GameMain);
            }
        }

        public void WriteName()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.\n");

            string name = Console.ReadLine() ?? "Chad";

            player.name = name;

            Console.WriteLine($"\n입력하신 이름은 {player.name} 입니다.\n");

            Console.WriteLine("1. 저장\n2. 취소\n");
            SceneManager.instance.Menu(ChoseJob, null, ChoseJob, WriteName);
        }

        public void ChoseJob()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 직업을 선택해주세요.\n");

            Console.WriteLine("1. 전사\n2. 도적\n");
            SceneManager.instance.Menu(ChoseJob, null, () => CompleteJob(CharacterJob.Worrier), () => CompleteJob(CharacterJob.Thief));
        }

        public void CompleteJob(CharacterJob _job)
        {
            switch (_job)
            {
                case CharacterJob.None:
                    player.job = "무직";
                    break;
                case CharacterJob.Worrier:
                    player.job = "전사";
                    break;
                case CharacterJob.Thief:
                    player.job = "도적";
                    break;
            }

            SceneManager.instance.GoMenu(GameMain);
        }

        public void GameMain()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기\n6. 게임저장\n7. 불러오기");
            SceneManager.instance.Menu(GameMain, null, PlayerStats, PlayerInventory, ShopManager.instance.ShopMain, DungeonMenu, RestArea, GameSave, GameLoad);
        }

        public void PlayerStats()
        {
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            // player 상태 보기
            player.Information();

            Console.WriteLine("0. 나가기\n");
            SceneManager.instance.Menu(PlayerStats, GameMain);
        }

        public void PlayerInventory()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            // player 상태 보기
            Console.WriteLine("[아이템 목록]");
            for(int i = 0; i < player.items.Count; i++){
                Console.Write($"- ");
                if(player.items[i].isEquip){
                    Console.Write($"[E]");
                }
                player.items[i].ItemException();
                Console.WriteLine("");
            }


            Console.WriteLine("\n\n1. 장착 관리\n0. 나가기\n");
            SceneManager.instance.Menu(PlayerInventory, GameMain, PlayerEquipment);
        }

        public void PlayerEquipment()
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");
            List<Action> actions = new List<Action>();
            actions.Add(PlayerInventory); // 0번째는 나가기

            for(int i = 0; i < player.items.Count; i++){
                Console.Write($"- {i+1} ");
                if(player.items[i].isEquip){
                    Console.Write($"[E]");
                }
                player.items[i].ItemException();
                Console.WriteLine("");


                int temp = i; // i값 변하기 때문에 지역변수로 캐싱해서 값 잡아둠
                actions.Add(()=>ItemEquipment(temp));
            }


            Console.WriteLine("\n\n0. 나가기\n");
            SceneManager.instance.Menu(PlayerEquipment, actions.ToArray());
        }
        public void ItemEquipment(int i)
        {
            if(!player.items[i].isEquip){
                Console.WriteLine($"{player.items[i].name} 장착 완료!");
                player.items[i].AddEquipment(player);
            } else {
                Console.WriteLine($"{player.items[i].name} 장착 해제!");
                player.items[i].RemoveEquipment(player);
            }
            Thread.Sleep(1000);
            SceneManager.instance.GoMenu(PlayerEquipment);
        }
        
        public void DungeonMenu()
        {
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            List<Action> actions = new List<Action>();
            actions.Add(GameMain);
            for(int i = 0; i < dungeon.Count; i++){
                Console.WriteLine($"{i+1}. {dungeon[i].info.name} | 방어력 {dungeon[i].info.armor} 이상 권장");

                int temp = i; // i값 변하기 때문에 지역변수로 캐싱해서 값 잡아둠
                actions.Add(()=>GoDungeon(temp));
            }
            Console.WriteLine("0. 나가기\n");

            SceneManager.instance.Menu(DungeonMenu, actions.ToArray());
        }

        public void GoDungeon(int _order){
            if(player.healthCurrent <= 0){
                Console.WriteLine("체력이 부족합니다...");
                SceneManager.instance.GoMenu(DungeonMenu);
                return;
            }


            bool isFail = false;
            int healthDecrease = 0; // 플레이어 health 감소량
            Random rand = new Random();

            if (player.armorCurrent < dungeon[_order].info.armor){
                isFail = rand.Next(0,10) < 4; // 40% 확률로 실패
            }
            
            if(isFail){
                healthDecrease = player.health / 2; // 실패하면 플레이어 health의 절반

                Console.WriteLine("던전실패...");
                Console.WriteLine("망했습니다...!!");
                Console.WriteLine($"{dungeon[_order].info.name}을 클리어 하지 못했습니다.\n");

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {player.healthCurrent} -> {player.healthCurrent - healthDecrease}");
                player.healthCurrent -= healthDecrease;

            } else {
                healthDecrease = 20 - (int)player.armorCurrent + (int)dungeon[_order].info.armor + rand.Next(0,16); // 20(-2) ~ 35(-2) 랜덤 

                healthDecrease = healthDecrease > 0 ? healthDecrease : 0;
                
                Console.WriteLine("던전클리어");
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{dungeon[_order].info.name}을 클리어 하였습니다.\n");

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {player.healthCurrent} -> {player.healthCurrent - healthDecrease}");
                player.healthCurrent -= healthDecrease;

                int gainGold = dungeon[_order].gold + dungeon[_order].gold * rand.Next((int)player.attackCurrent, (int)(player.attackCurrent*2f)) / 100; // 공격력 ~ 공격력x2
                Console.WriteLine($"Gold {player.gold} G -> {player.gold + gainGold} G");
                player.gold += gainGold;

                Console.WriteLine($"Level {player.level} -> {player.level+1} \n");
                player.LevelUp();
            }

            Console.WriteLine("0. 나가기\n");
            SceneManager.instance.Menu(DungeonMenu, DungeonMenu);
        }

        public void RestArea(){
            Console.WriteLine($"{RestCost}G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.gold})\n");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("\n1. 휴식하기\n0. 나가기\n");
            SceneManager.instance.Menu(RestArea, GameMain, PlayerRest); 
        }

        public void PlayerRest(){
            if(RestCost <= player.gold){
                player.gold -= RestCost;
                Console.WriteLine("휴식을 완료했습니다.");
                player.Healing(100);
            }
            else {
                Console.WriteLine("Gold 가 부족합니다.");
            }

            Thread.Sleep(1000);
            SceneManager.instance.GoMenu(RestArea);
        }


    }
}