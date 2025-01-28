namespace TextRPG
{
    class GameManager : Helper.Singleton<GameManager>
    {
        public void GameMain(){
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            
            Console.WriteLine("1. 상태보기\n2. 인벤토리\n3. 상점\n");
            SceneManager.instance.Menu(GameMain, null, PlayerStats ,GameMain ,GameMain);
        }
        
        public void PlayerStats(){
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            // TODO 상태 보기 만들기
            
            Console.WriteLine("0. 나가기\n");
            SceneManager.instance.Menu(PlayerStats, GameMain);
        }
    }
}