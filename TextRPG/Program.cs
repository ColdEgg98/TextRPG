using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Xml.Linq;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;



public class WriteManager
{
    public void ColoredLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}

public interface ICharacter
{
    string Name { get; set; }
    int CurrentHealth { get; set; }
    float Attack { get; set; }
    bool IsDead { get; set; }
    void TakeDamage(int damage);
}

[JsonDerivedType(typeof(IronArmor), "IronArmor")]
[JsonDerivedType(typeof(OldSword), "OldSword")]
[JsonDerivedType(typeof(Spear), "Spear")]
[JsonDerivedType(typeof(SunMoonSword), "SunMoonSword")]
[JsonDerivedType(typeof(Healingpotion), "Healingpotion")]
public abstract class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public float Price { get; set; }
    public int Value { get; set; }
    public bool isEquip { get; set; }
    public bool isOwned { get; set; }

    public abstract void Use(Warrior warrior);
    public abstract void UnUse(Warrior warrior);
}

public class IronArmor : Item
{
    public IronArmor()
    {
        Name = "무쇠 갑옷";
        Type = "방어력";
        Description = "무쇠로 만들어져 튼튼한 갑옷입니다.";
        isEquip = false;
        isOwned = false;
        Price = 800;
        Value = 5;
    }
    public override void Use(Warrior warrior)
    {
        warrior.Defense += Value;
    }
    public override void UnUse(Warrior warrior)
    {
        warrior.Defense -= Value;
    }
}

public class OldSword : Item
{
    public OldSword()
    {
        Name = "낡은 검";
        Type = "공격력";
        Description = "쉽게 볼 수 있는 낡은 검입니다.";
        isEquip = false;
        isOwned = false;
        Value = 2;
        Price = 500;
    }
    public override void Use(Warrior warrior)
    {
        warrior.Attack += Value;
    }
    public override void UnUse(Warrior warrior)
    {
        warrior.Attack -= Value;
    }
}

public class Spear : Item
{
    public Spear()
    {
        Name = "스파르탄의 창";
        Type = "공격력";
        Description = "과거의 스파르탄이 사용한 창입니다.";
        isEquip = false;
        isOwned = false;
        Value = 7;
        Price = 3000;
    }
    public override void Use(Warrior warrior)
    {
        warrior.Attack += Value;
    }
    public override void UnUse(Warrior warrior)
    {
        warrior.Attack -= Value;
    }
}

public class SunMoonSword : Item
{
    public SunMoonSword()
    {
        Name = "태양과 달의 검";
        Type = "공격력";
        Description = "태양과 달의 힘을 결합하여 검의 형상에 응축한 양손검.";
        isEquip = false;
        isOwned = false;
        Value = 30;
        Price = 900000;
    }
    public override void Use(Warrior warrior)
    {
        warrior.Attack += Value;
    }
    public override void UnUse(Warrior warrior)
    {
        warrior.Attack -= Value;
    }
}
public class Healingpotion: Item
{
    public Healingpotion()
    {
        Name = "힐링 포션";
        Type = "체력 회복";
        Description = "사용하면 체력 30을 회복합니다.";
        Value = 30;
        Price = 400;
    }
    public override void Use(Warrior warrior)
    {
        if (warrior.CurrentHealth + Value > warrior.MaxHealth)
            warrior.CurrentHealth = warrior.MaxHealth;
        else
            warrior.CurrentHealth += Value;
    }
    public override void UnUse(Warrior warrior) { }
}
public class Warrior : ICharacter
{
    public int level { get; set; }
    public int experience { get; set; }
    public string Name { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public string Class { get; set; }
    public float Gold { get; set; }
    public List<Item> Inventory { get; set; }


    public void TakeDamage(int damage)
    {
        WriteManager WM = new WriteManager();

        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            WM.ColoredLine("\n체력이 0이 되어 사망하였습니다...\n", ConsoleColor.DarkGray);
            IsDead = true;
            Thread.Sleep(1000);
        }
    }

    public Warrior()
    {
        Name = "Name";
        level = 1;
        experience = 0;
        Attack = 20;
        Defense = 10;
        MaxHealth = 100;
        CurrentHealth = 50;
        Class = "전사";
        Gold = 1000000;
        Inventory = new List<Item>();
    }
    public void expUP()
    {
        WriteManager WM = new WriteManager();

        experience++;
        int levelGap = level - experience;
        if (levelGap == 0)
        {
            level++;
            Attack += 0.5f;
            Defense += 1;
            MaxHealth += 10;
            CurrentHealth += 10;

            experience = 0;

            WM.ColoredLine("\n   레벨업!\n", ConsoleColor.DarkYellow);
        }
    } // 경험치 획득

    public void ShowInfo() // 1번을 입력해서 상태 보기로 들어왔을 때
    {
        WriteManager WM = new WriteManager();

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.Write(
                $"Lv. 0{level}\n" +
                $"Chad: {Class}\n체력 : ");
            WM.ColoredLine($"{CurrentHealth}/{MaxHealth}", ConsoleColor.DarkRed);
            Console.Write($"공격력: ");
            WM.ColoredLine($"{Attack}", ConsoleColor.DarkMagenta);
            Console.Write("방어력 : ");
            WM.ColoredLine($"{Defense}", ConsoleColor.Cyan);
            Console.Write("Gold : ");
            WM.ColoredLine($"{Gold}\n", ConsoleColor.DarkYellow);

            Console.Write("\n원하시는 행동을 입력해주세요.\n\n0. 나가기\n\n>> ");
            if ("0" == Console.ReadLine())
                return;
        }
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
    } // 인벤토리에 아이템 추가

    public void UseItem(Item item)
    {
        item.Use(this);
        item.isEquip = false;
        Inventory.Remove(item);
    }

    public void ShowInventory() // 2번을 입력해서 인벤토리로 들어왔을 때
    {
        WriteManager WM = new WriteManager();

        while (true)
        {
            Console.Clear();
            WM.ColoredLine("인벤토리", ConsoleColor.Yellow);
            Console.WriteLine("인벤토리의 정보가 표시됩니다.\n");

            if (Inventory.Count == 0)
            {
                WM.ColoredLine("인벤토리가 비어있습니다.", ConsoleColor.DarkGray);
            }
            else
            {
                for (int i = 0; i < Inventory.Count; i++) // 인벤토리에 있는 아이템 목록 출력
                {
                    if (Inventory[i].isEquip == false)
                        Console.WriteLine($"{i + 1}. {Inventory[i].Name}");
                    else if (Inventory[i].isEquip == true)
                        Console.WriteLine($"[E]{i + 1}. {Inventory[i].Name}");
                }
            }

            Console.SetCursorPosition(3, 10);
            Console.Write("원하시는 행동을 입력해주세요.\n\n아이템 번호 입력 → 착용/해제\n0. 나가기\n\n>> ");

            int input = int.Parse(Console.ReadLine());

            if (input == 0)
                break;

            // 아이템 장착
            if (input <= Inventory.Count && Inventory[input - 1].isEquip == false)
            {
                Inventory[input - 1].Use(this);
                string itemType = Inventory[input - 1].Type;

                for (int i = 0; i < Inventory.Count; i++) // 겹치는 타입의 아이템 해제
                {
                    if (Inventory[i].isEquip == true && itemType == Inventory[i].Type)
                    {
                        Inventory[i].isEquip = false;
                        Inventory[i].UnUse(this);
                    }
                }
                Inventory[input - 1].isEquip = true;

                if (itemType == "체력 회복")
                {
                    UseItem(Inventory[input - 1]);
                }
            }
            // 아이템 해제
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

    public void ShowShop(List<Item> items) // 3번을 입력해서 상점으로 들어왔을 때
    {
        WriteManager WM = new WriteManager();

        while (true)
        {
            Console.Clear();
            WM.ColoredLine("상점", ConsoleColor.Yellow);
            Console.Write($"필요한 아이템을 구입할 수 있습니다.\n  현재 보유금 : ");
            WM.ColoredLine($"{Gold}\n\n", ConsoleColor.DarkYellow);

            // 아이템 목록 출력
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isOwned == false)
                    Console.WriteLine($"{i + 1}. {items[i].Name} | {items[i].Type} + {items[i].Value} | {items[i].Description} | {items[i].Price} G");
                else if (items[i].isOwned == true)
                    WM.ColoredLine($"{i + 1}. {items[i].Name} | {items[i].Type} + {items[i].Value} | {items[i].Description} | 구매 완료", ConsoleColor.DarkGray);
            }

            Console.SetCursorPosition(3, 10);
            Console.Write("\n원하시는 행동을 입력해주세요." +
                "\n\n1. 아이템 구입\n2. 아이템 판매" +
                "\n0. 나가기\n\n>> ");

            int input = int.Parse(Console.ReadLine());

            if (input == 0)
                break;

            switch (input)
            {
                case 1: // 아이템 구입
                    Console.Write("\n\n번호를 입력해 아이템을 구입합니다.\n\n>> ");
                    input = int.Parse(Console.ReadLine());

                    if (input <= items.Count)
                    {
                        // 아이템 구입 로직
                        if (Gold >= items[input - 1].Price)
                        {
                            Gold -= items[input - 1].Price;
                            AddItem(items[input - 1]);
                            items[input - 1].isOwned = true;
                            Console.WriteLine($"{items[input - 1].Name}을 구입하였습니다.");
                            Thread.Sleep(600);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("골드가 부족합니다.");
                            Thread.Sleep(600);
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("상점 구매에서 나갑니다.");
                        Thread.Sleep(600);
                        break;
                    }

                case 2: // 아이템 판매

                    // 인벤토리 목록 출력
                    if (Inventory.Count == 0)
                    {
                        WM.ColoredLine("인벤토리가 비어있습니다.", ConsoleColor.DarkGray);
                    }
                    else
                    {
                        for (int i = 0; i < Inventory.Count; i++) // 인벤토리에 있는 아이템 목록 출력
                        {
                            if (Inventory[i].isEquip == false)
                                Console.WriteLine($"{i + 1}. {Inventory[i].Name}");
                            else if (Inventory[i].isEquip == true)
                                Console.WriteLine($"[E]{i + 1}. {Inventory[i].Name}");
                        }
                    }

                    Console.Write("\n\n번호를 입력해 아이템을 판매합니다.\n\n>> ");
                    input = int.Parse(Console.ReadLine());
                    if (input <= Inventory.Count)
                    {
                        Gold += Inventory[input - 1].Price * 0.85f;
                        items[items.Count - 1].isOwned = false;
                        Inventory.RemoveAt(input - 1);
                        Console.WriteLine("아이템을 판매하였습니다.");
                        Thread.Sleep(600);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("상점 판매에서 나갑니다.");
                        Thread.Sleep(600);
                        break;
                    }
            }
        }
    }

    public void GoToDungeon() //4번 입력해서 던전으로 들어왔을 때
    {
        WriteManager WM = new WriteManager();

        Console.Clear();

        // 출력부
        WM.ColoredLine("\n  던전", ConsoleColor.DarkRed);
        Console.WriteLine("  경험치와 돈을 얻을 수 있는 던전입니다.");
        WM.ColoredLine($"  현재 공격력 : {Attack}, 방어력 : {Defense}", ConsoleColor.DarkGray);

        Console.SetCursorPosition(3, 5);
        WM.ColoredLine("1. 쉬움 던전 (적정 방어력 5 이상)", ConsoleColor.DarkGreen);
        Console.SetCursorPosition(3, 6);
        WM.ColoredLine("2. 보통 던전 (적정 방어력 15 이상)", ConsoleColor.DarkYellow);
        Console.SetCursorPosition(3, 7);
        WM.ColoredLine("3. 쉬움 던전 (적정 방어력 25 이상)", ConsoleColor.DarkRed);

        Console.SetCursorPosition(3, 9);
        WM.ColoredLine("4. 내 스테이터스 확인", ConsoleColor.DarkGray);
        Console.SetCursorPosition(3, 10);
        WM.ColoredLine("0. 나가기", ConsoleColor.DarkGray);

        // 선택 입력 받기
        int input = int.Parse(Console.ReadLine());

        // 나가기
        if (input == 0)
            return;

        // 던전 입장
        string difficulty = "";
        Random rand = new Random(); // 성공 굴림

        Console.Clear();

        // 공통 출력부
        switch (input)
        {
            case 1:
                difficulty = "쉬움";
                break;
            case 2:
                difficulty = "보통";
                break;
            case 3:
                difficulty = "어려움";
                break;
        }
        string difStr = difficulty + " 난이도로 던전에 입장합니다.";
        Console.SetCursorPosition(3, 5);
        Console.WriteLine(difStr);
        Console.SetCursorPosition(3, 10);
        Console.Write("□□□□□□□□□□□□□□□□□□□□");

        // 게이지 연출
        Console.SetCursorPosition(3, 10);
        for (int i = 0; i < 10; i++)
        {
            Console.Write("■");
            Thread.Sleep(80);
        }

        // 쉬움        
        if (difficulty == "쉬움" && Defense < 5) // 적정 방어력 이하일 때, 40% 확률로 실패
        {
            if (100 > rand.Next(0, 41)) // 성공 굴림 (60%)
                DungeonWin(5);
            else
                DungeonDefeat();
        } else if (difficulty == "쉬움" && Defense >= 5) // 적정 방어력 이상일 때
        {
            DungeonWin(5);
        }

        // 보통  
        if (difficulty == "보통" && Defense < 15) // 적정 방어력 이하일 때, 40% 확률로 실패
        {
            if (100 > rand.Next(0, 41)) // 성공 굴림 (60%)
                DungeonWin(15);
            else
                DungeonDefeat();
        }
        else if (difficulty == "보통" && Defense >= 15) // 적정 방어력 이상일 때
        {
            DungeonWin(15);
        }
        // 어려움  
        if (difficulty == "어려움" && Defense < 25) // 적정 방어력 이하일 때, 40% 확률로 실패
        {
            if (100 > rand.Next(0, 41)) // 성공 굴림 (60%)
                DungeonWin(15);
            else
                DungeonDefeat();
        }
        else if (difficulty == "어려움" && Defense >= 25) // 적정 방어력 이상일 때
        {
            DungeonWin(25);
        }
        // 내 스테이터스 보기
        else if (input == 4)
        {
            ShowInfo();
        }
    }

    public void DungeonWin(int properDefense) // 던전 클리어시 출력, 골드&경험치 획득
    {
        WriteManager WM = new WriteManager();

        Random bounsPer = new Random(); // 보너스
        float bouns = bounsPer.Next((int)Attack, (int)Attack * 2 + 1) / 100.0f;

        int Damage = CalculateDamage(properDefense); // 요구 방어력에 따른 대미지 계산

        Console.SetCursorPosition(23, 10);
        for (int i = 0; i < 10; i++)
        {
            Console.Write("■");
            Thread.Sleep(80);
        }
        Gold += 1000 + 1000 * bouns;

        Console.SetCursorPosition(3, 12);
        Console.WriteLine("던전 성공!");
        WM.ColoredLine($"     1000G + {1000 * bouns}G를 획득하였습니다.", ConsoleColor.DarkYellow);
        WM.ColoredLine($"     경험치를 획득했습니다!", ConsoleColor.DarkYellow);
        WM.ColoredLine($"     HP - {Damage}", ConsoleColor.DarkGray);

        expUP(); // 경험치 획득

        TakeDamage(Damage); // 체력 감소

        Console.WriteLine("\n\n아무키나 입력하면 돌아갑니다.");
        Console.ReadLine();

    }

    public int CalculateDamage(int properDefense) // 대미지 계산
    {
        Random rand = new Random();
        int Damage = rand.Next(20, 36);

        // 요구 방어력보다 방어력이 높을 때
        if (Defense >= properDefense)
        {
            Damage -= (int)Defense - properDefense;
            return Damage;
        }
        else // 방어력이 낮을 때
        {
            Damage += properDefense - (int)Defense;
            return Damage;
        }
    }

    public void DungeonDefeat() // 던전 실패시 출력, 체력 감소
    {
        WriteManager WM = new WriteManager();
        int Damage = MaxHealth / 2;

        Console.SetCursorPosition(3, 12);
        Console.WriteLine("던전 실패...");
        WM.ColoredLine($"체력 -50%({MaxHealth / 2}) 감소했습니다.)", ConsoleColor.DarkGray);

        TakeDamage(MaxHealth / 2);

        Console.WriteLine("아무키나 누르면 돌아갑니다.");
        Console.ReadLine();
        return;
    }

    public void Rest() // 5번 입력해서 여관으로 들어왔을 때
    {
        WriteManager WM = new WriteManager();

        while (true)
        {
            Console.Clear();
            WM.ColoredLine("여관", ConsoleColor.Yellow);
            Console.WriteLine("500G로 50HP를 채울 수 있습니다.");

            WM.ColoredLine($"현재 생명력 : {CurrentHealth}/{MaxHealth}", ConsoleColor.Red);

            Console.SetCursorPosition(3, 10);
            Console.Write("\n원하시는 행동을 입력해주세요.\n\n   1. 회복하기\n   0. 나가기\n\n>> ");
            if ("0" == Console.ReadLine())
                break;
            if (Gold >= 500)
            {
                Gold -= 500;
                if (CurrentHealth + 50 > MaxHealth)
                    CurrentHealth = MaxHealth;
                else
                    CurrentHealth += 50;
                Console.WriteLine("회복되었습니다.");
                Thread.Sleep(700);
            }
            else
            {
                Console.WriteLine("골드가 부족합니다.");
                Thread.Sleep(700);
            }
        }
    }

    public void SaveProgress()
    {
        try
        {
            // JSON 직렬화 옵션 설정 (들여쓰기 등 가독성 향상)
            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new JsonStringEnumConverter());

            // 객체를 JSON 문자열로 직렬화
            string jsonString = JsonSerializer.Serialize(this, options);

            // 파일에 JSON 문자열 저장
            File.WriteAllText("progress.json", jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine($"저장 중 오류 발생 : {e.Message}");
        }
    }

    public static Warrior LoadProgress()
    {
        if (File.Exists("progress.json"))
        {
            // 파일에서 JSON 문자열 읽기
            string jsonString = File.ReadAllText("progress.json");

            // JSON 문자열을 객체로 역직렬화
            Warrior loadedWarrior = JsonSerializer.Deserialize<Warrior>(jsonString);

            return loadedWarrior;
        }
        else
        {
            return null; // 저장된 진행 상황이 없으면 null 반환
        }
    }
}

class Monster : ICharacter
{
    public string Name { get; set; }
    public int CurrentHealth { get; set; }
    public float Attack { get; set; }
    public bool IsDead { get; set; }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
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
        CurrentHealth = 100;
        Attack = 10;
        IsDead = false;
    }
}

class Dragon : Monster
{
    public Dragon()
    {
        Name = "Dragon";
        CurrentHealth = 200;
        Attack = 20;
        IsDead = false;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        WriteManager WM = new WriteManager();

        Warrior player = new Warrior();
        Warrior loadedPlayer = Warrior.LoadProgress();

        int input;

        if (loadedPlayer != null)
        {
            player = loadedPlayer;
            WM.ColoredLine("이전에 저장한 진행 상황을 불러왔습니다.", ConsoleColor.DarkYellow);
            Thread.Sleep(1000);
        } else
        {
            Console.WriteLine("텍스트 RPG 게임에 오신 것을 환영합니다.\n이름을 입력해주세요.\n\n");
            string name = null;
            while (string.IsNullOrEmpty(name))
            {
                Console.Clear();
                Console.WriteLine("텍스트 RPG 게임에 오신 것을 환영합니다.\n이름을 입력해주세요.\n\n");
                name = Console.ReadLine();
                player = new Warrior();
                player.Name = name;
            }
        }

        // 상점 아이템 목록
        List<Item> shopitems = new List<Item>();
        shopitems.Add(new IronArmor());
        shopitems.Add(new OldSword());
        shopitems.Add(new Spear());
        shopitems.Add(new SunMoonSword());
        shopitems.Add(new Healingpotion());

        while (true)
        {
            Console.Clear();
            WM.ColoredLine($"스파르타 던전에 오신 '{player.Name}'님 환영합니다.\n이 곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n", ConsoleColor.White);
            WM.ColoredLine($"1. 상태 보기", ConsoleColor.DarkGreen);
            WM.ColoredLine($"2. 인벤토리", ConsoleColor.DarkGreen);
            WM.ColoredLine($"3. 상점", ConsoleColor.DarkGreen);
            WM.ColoredLine($"4. 던전 입장", ConsoleColor.DarkRed);
            WM.ColoredLine($"5. 휴식", ConsoleColor.DarkGreen);

            player.SaveProgress();

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
                    player.ShowShop(shopitems);
                    continue;
                case 4:
                    player.GoToDungeon();
                    break;
                case 5:
                    player.Rest();
                    continue;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    Thread.Sleep(600);
                    break;
            }
        }
    }
}
