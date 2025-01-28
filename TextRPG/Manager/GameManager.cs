namespace TextRPG
{
    class GameManager : Helper.Singleton<GameManager>
    {
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

            Console.WriteLine("1. 상태보기\n2. 인벤토리\n3. 상점\n");
            SceneManager.instance.Menu(GameMain, null, PlayerStats, PlayerInventory, ShopManager.instance.ShopMain);
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
            }


            Console.WriteLine("\n\n1. 장착 관리\n0. 나가기\n");
            SceneManager.instance.Menu(PlayerInventory, GameMain, PlayerEquipment);
        }

        public void PlayerEquipment()
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            // TODO 장착 관리
            Console.WriteLine("[아이템 목록]");
            List<Action> actions = new List<Action>();
            actions.Add(PlayerInventory); // 0번째는 나가기

            for(int i = 0; i < player.items.Count; i++){
                Console.Write($"- {i+1} ");
                if(player.items[i].isEquip){
                    Console.Write($"[E]");
                }
                player.items[i].ItemException();


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

    }
}