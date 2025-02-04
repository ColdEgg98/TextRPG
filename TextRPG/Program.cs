//2.과제 요구사항:
//-**`ICharacter`**라는 인터페이스를 정의하세요. 이 인터페이스는 다음의 프로퍼티를 가져야 합니다:
//        -**`Name`**: 캐릭터의 이름
//        - **`Health`**: 캐릭터의 현재 체력
//        - **`Attack`**: 캐릭터의 공격력
//        - **`IsDead`**: 캐릭터의 생사 상태
//        그리고 다음의 메서드를 가져야 합니다:
//        -**`TakeDamage(int damage)`**: 캐릭터가 데미지를 받아 체력이 감소하는 메서드
//        - **`Warrior`**는 플레이어의 캐릭터를 나타내며, **`Monster`**는 몬스터를 나타냅니다.
//    - **`ICharacter`** 인터페이스를 구현하는 **`Warrior`**와 **`Monster`**라는 두 개의 클래스를 만들어주세요.
//        - **`Monster`** 클래스에서 파생된 **`Goblin`**과 **`Dragon`**이라는 두 개의 클래스를 추가로 만들어주세요.
//    - **`IItem`**이라는 인터페이스를 정의하세요. 이 인터페이스는 다음의 프로퍼티를 가져야 합니다:
//        -**`Name`**: 아이템의 이름
//        그리고 다음의 메서드를 가져야 합니다:
//        -**`Use(Warrior warrior)`**: 아이템을 사용하는 메서드, 이 메서드는 **`Warrior`** 객체를 파라미터로 받습니다.
//    - **`IItem`** 인터페이스를 구현하는 **`HealthPotion`**과 **`StrengthPotion`**이라는 두 개의 클래스를 만들어주세요.
//    - **`Stage`**라는 클래스를 만들어 주세요. 이 클래스는 플레이어와 몬스터, 그리고 보상 아이템들을 멤버 변수로 가지며, **`Start`**라는 메서드를 통해 스테이지를 시작하게 됩니다.
//        - 스테이지가 시작되면, 플레이어와 몬스터가 교대로 턴을 진행합니다.
//        - 플레이어나 몬스터 중 하나가 죽으면 스테이지가 종료되고, 그 결과를 출력해줍니다.
//        - 스테이지가 끝날 때, 플레이어가 살아있다면 보상 아이템 중 하나를 선택하여 사용할 수 있습니다.

using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Xml.Linq;
using System.ComponentModel;

public interface ICharacter
{
    string Name { get; set; }
    int Health { get; set; }
    int Attack { get; set; }
    bool IsDead { get; set; }
    void TakeDamage(int damage);
}

public interface IItem
{
    string Name { get; set; }
    string Description { get; set; }
    int Price { get; set; }

    bool isEquip { get; set; }

    void Use(Warrior warrior);
    void UnUse(Warrior warrior);
}

public class IronArmor : IItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public bool isEquip { get; set; }
    public IronArmor()
    {
        Name = "무쇠 갑옷";
        Description = "무쇠로 만들어져 튼튼한 갑옷입니다.";
        isEquip = false;
        Price = 800;
    }
    public void Use(Warrior warrior)
    {
        warrior.Defense += 5;
    }
    public void UnUse(Warrior warrior)
    {
        warrior.Defense -= 5;
    }
}

public class OldSword : IItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public bool isEquip { get; set; }
    public OldSword()
    {
        Name = "낡은 검";
        Description = "쉽게 볼 수 있는 낡은 검입니다.";
        isEquip = false;
        Price = 500;
    }
    public void Use(Warrior warrior)
    {
        warrior.Attack += 2;
    }
    public void UnUse(Warrior warrior)
    {
        warrior.Attack -= 2;
    }
}

public class Spear : IItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public bool isEquip { get; set; }
    public Spear()
    {
        Name = "스파르탄의 창";
        Description = "과거의 스파르탄이 사용한 창입니다.";
        isEquip = false;
        Price = 3000;
    }
    public void Use(Warrior warrior)
    {
        warrior.Attack += 7;
    }
    public void UnUse(Warrior warrior)
    {
        warrior.Attack -= 7;
    }
}

public class Warrior : ICharacter
{
    public int level { get; set; }
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Health { get; set; }
    public bool IsDead { get; set; }
    public string Class { get; set; }
    public int Gold { get; set; }


    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    public List<IItem> Inventory { get; set; }

    public Warrior(string name)
    {
        Name = name;
        level = 1;
        Attack = 20;
        Defense = 10;
        Health = 50;
        Class = "전사";
        Gold = 1000000;
        Inventory = new List<IItem>();
    }

    public void ShowInfo() // 1번을 입력해서 상태 보기로 들어왔을 때
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine(
                $"Lv. 0{level}\n" +
                $"Chad: {Class}\n" +
                $"체력 : {Health}\n" +
                $"공격력: {Attack}\n" +
                $"방어력 : {Defense}\n" +
                $"Gold : {Gold} G\n");

            Console.Write("\n원하시는 행동을 입력해주세요.\n\n0. 나가기\n\n>> ");
            if ("0" == Console.ReadLine())
                break;
        }
    }

    public void AddItem(IItem item)
    {
        Inventory.Add(item);
    }

    public void ShowInventory() // 2번을 입력해서 인벤토리로 들어왔을 때
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("인벤토리의 정보가 표시됩니다.\n");

            if (Inventory.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어있습니다.");
            }
            else
            {
                for (int i = 0; i < Inventory.Count; i++)
                {
                    if(Inventory[i].isEquip == false)
                        Console.WriteLine($"{i + 1}. {Inventory[i].Name}");
                    else if (Inventory[i].isEquip == true)
                        Console.WriteLine($"[E]{i + 1}. {Inventory[i].Name}");
                }
            }

            Console.Write("\n원하시는 행동을 입력해주세요.\n\n아이템 번호 입력 → 착용/해제\n0. 나가기\n\n>> ");

            int input = int.Parse(Console.ReadLine());

            if (input == 0)
                break;

            if (input <= Inventory.Count && Inventory[input-1].isEquip == false)
            {
                Inventory[input - 1].Use(this);
                Inventory[input - 1].isEquip = true;
            }
            else if (input <= Inventory.Count && Inventory[input - 1].isEquip == true)
            {
                Inventory[input - 1].UnUse(this);
                Inventory[input - 1].isEquip = false;
            }
            else if (input > Inventory.Count)
            {
                continue;
            }
        }
    }

    public void ShowShop() // 3번을 입력해서 상점으로 들어왔을 때
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 구입할 수 있습니다.\n");

            // ShopItemList 작성해서 아이템 리스트를 보여주기

            Console.Write("\n원하시는 행동을 입력해주세요." +
                "\n\n1. 아이템 구입" +
                "\n\n0. 나가기\n\n>> ");



            if ("1" == Console.ReadLine())
            {
                // 아이템 구입 로직 작성
                break;

            }
            else if ("0" == Console.ReadLine())
                break;
        }
    }
}

class Monster : ICharacter
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool IsDead { get; set; }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            IsDead = true;
        }
    }
}

class Goblin : Monster
{
    public Goblin()
    {
        Name = "Goblin";
        Health = 100;
        Attack = 10;
        IsDead = false;
    }
}

class Dragon : Monster
{
    public Dragon()
    {
        Name = "Dragon";
        Health = 200;
        Attack = 20;
        IsDead = false;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("텍스트 RPG 게임에 오신 것을 환영합니다.\n이름을 입력해주세요.\n\n");
        string name = Console.ReadLine();
        Warrior player = new Warrior(name);

        List<IItem> shopitems = new List<IItem>();
        shopitems.Add(new IronArmor());
        shopitems.Add(new OldSword());
        shopitems.Add(new Spear());

        player.AddItem(new IronArmor());

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"스파르타 던전에 오신 '{name}'님 환영합니다.\n이 곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n\n원하시는 행동을 입력해주세요. ");

            int input;
            input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    player.ShowInfo();
                    continue;
                case 2:
                    player.ShowInventory();
                    continue;
                case 3:
                    Console.WriteLine("상점");
                    continue;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
}
