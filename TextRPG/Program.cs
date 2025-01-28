using System.ComponentModel;

namespace TextRPG
{
    class Program
    {
        static void Main(string[] args){
            SceneManager.instance.nextScene = GameManager.instance.GameMain;
            while(true){
                Console.Clear();

                // 게임 시작 화면
                SceneManager.instance.nextScene();
            }

        }
    }
}