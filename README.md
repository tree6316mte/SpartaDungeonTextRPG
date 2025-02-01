# 💻 SpartaDungeonTextRPG 환경설정

<div align="center">

[![Visual Studio Code](https://custom-icon-badges.demolab.com/badge/Visual%20Studio%20Code-0078d7.svg?logo=vsc&logoColor=white)](#)
[![C#](https://custom-icon-badges.demolab.com/badge/C%23%2013.0-%23239120.svg?logo=cshrp&logoColor=white)](#)
[![.NET](https://img.shields.io/badge/.NET%209.0-512BD4?logo=dotnet&logoColor=fff)](#)
[![Console](https://custom-icon-badges.demolab.com/badge/Console-444444.svg?&logoColor=gray)](#)


<br><br>

</div>

# 🔧 환경 설정

#### **[VSCode에서 솔루션에 프로젝트 추가]**  
1. 솔루션 폴더에서 `dotnet new sln -n <솔루션 이름>` 입력하여 솔루션 생성
2. 솔루션 폴더 내부에 프로젝트로 사용 할 폴더 생성
3. console창에서 프로젝트 폴더에 `dotnet new console` 입력하여 새 프로젝트 생성
4. console창에서 솔루션 폴더로 돌아 간 뒤 `dotnet sln <솔루션 폴더\\솔루션 이름.sln> add <프로젝트 폴더\\프로젝트 이름.csproj>` 입력하여 솔루션에 추가  

<br>

#### **[VSCode에서 빌드 후 실행]**
1. 프로젝트 폴더에서 `dotnet build` 입력하여 빌드
2. 프로젝트 폴더 내부에 있는 `bin\Debug\net9.0\` 폴더로 들어가서 `프로젝트.exe` 파일 실행 


<br><br>

# 🔨 구현된 기능

#### **[💿필수 기능]**  
> 게임 시작 화면  
> 상태보기  
> 인벤토리  
> 장착 관리  
> 상점  
> 아이템 구매

<br>

#### **[📀도전 기능]**  
> 아이템 추가 - 나만의 새로운 아이템을 추가 (난이도 - ☆☆☆☆☆)  
> 휴식기능 추가 (난이도 - ★☆☆☆☆)  
> 판매하기 기능 추가 기능 추가 (난이도 - ★☆☆☆☆)  
> 장착 개선 (난이도 - ★★☆☆☆)  
> 레벨업 기능 추가 (난이도 - ★★☆☆☆)  
> 던전입장 기능 추가 (난이도 - ★★★☆☆)
> 게임 저장하기 추가 (난이도 - ★★★★☆)

<br><br>

# 📂 폴더 구조
```bash
📦TextRPG  
 ┣ 📂Class  
 ┃ ┣ 📂Character  
 ┃ ┃ ┗ 📜Player.cs  
 ┃ ┣ 📂Item  
 ┃ ┃ ┣ 📜BronzeAx.cs  
 ┃ ┃ ┣ 📜IronArmor.cs  
 ┃ ┃ ┣ 📜NoviceArmor.cs  
 ┃ ┃ ┣ 📜OldSword.cs  
 ┃ ┃ ┣ 📜SpartaArmor.cs  
 ┃ ┃ ┣ 📜SpartaMace.cs  
 ┃ ┃ ┗ 📜SpartaSpear.cs  
 ┃ ┣ 📜Character.cs  
 ┃ ┗ 📜Item.cs  
 ┣ 📂Helper  
 ┃ ┗ 📜Singleton.cs  
 ┣ 📂Manager  
 ┃ ┣ 📜GameManager.cs  
 ┃ ┣ 📜SaveLoadManager.cs  
 ┃ ┣ 📜SceneManager.cs  
 ┃ ┗ 📜ShopManager.cs  
 ┗ 📜Program.cs  
```

# ✨ 핵심 기능
### 📜 1. SceneManager.cs
```C#
class SceneManager : Helper.Singleton<SceneManager>
{
    public Action? nextScene;

    // origin, 0 ~ ...
    public void Menu(Action _origin, params Action?[] _actions){

        Console.Write("원하시는 행동을 입력해주세요\n>>");
        int.TryParse(Console.ReadLine(), out int input);

        Console.WriteLine($"입력 된 행동>> {input}");

        // 입력 범위 값이 이 안에 없으면 오류와 함께 다시 origin 시작
        if(input < 0 || _actions.Length <= input || _actions[input] == null){
            Console.WriteLine($"\n잘못된 입력입니다. (입력된 값 : {input})\n");
            Thread.Sleep(1000);
            nextScene = _origin;
        } else {
            nextScene = _actions[input];
        }
    }

    public void GoMenu(Action _action){
        nextScene = _action;
    }
}
```
 싱글톤으로 만든 `SceneManager`클래스의 `Menu(Action _origin, params Action?[] _actions)`함수를 이용하여 매개변수로 Action을 받아서 입력 범위 값이 아니라면 다시 자기자신의 `_origin`함수로 돌아가고 아니면 입력된 값의 `0 ~ ?`값에 해당하는 `_actions[?]`함수로 이동 할 수 있습니다.  
  
매개변수로 `null`을 넣으면 잘못된 입력으로 넘어가기 때문에 `0`에 아무값도 넣고 싶지 않다면 `null`을 넣어서 유동적으로 사용 할 수 있습니다.

바로 씬을 이동하고 싶으면 `GoMenu(Action _action)`함수를 사용합니다.

### 📜 2. Program.cs > Main
```C#
class Program
{
    static void Main(string[] args){
        SceneManager.instance.nextScene = GameManager.instance.WriteName;
        while(true){
            Console.Clear();

            // 게임 시작 화면
            SceneManager.instance.nextScene();
        }
    }
}
```
메인을 위와 같이 만들어서 `SceneManager`클래스의 `nextScene`멤버변수의 함수를 실행시켜 메모리 스택이 쌓이는 것을 방지하였습니다.

### 3. SceneManager.instance 사용법
```C#
Console.WriteLine("0. 나가기\n");
SceneManager.instance.Menu(현재 함수명, 실행 할 함수명);
```
`Menu`를 사용하여 0번째의 경우 `현재 함수명` 다음에 바로 `실행 할 함수명`을 입력하면 됩니다.

```C#
Console.WriteLine("1. 나가기\n");
SceneManager.instance.Menu(현재 함수명, null, 실행 할 함수명);
```
만약 0번째를 비워두고 싶다면 `null` 이후에 `실행 할 함수명`을 입력하면 됩니다.

```C#
SceneManager.instance.GoMenu(바로 이동 할 함수명);
```
기다림 없이 바로 이동 하게 하려면`GoMenu`를 사용하면 됩니다.
