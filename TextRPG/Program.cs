using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Xml.Linq;
using System.ComponentModel;

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
    int Attack { get; set; }
    bool IsDead { get; set; }
    void TakeDamage(int damage);
}

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

public class Warrior : ICharacter
{
    public int level { get; set; }
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public string Class { get; set; }
    public float Gold { get; set; }


    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            IsDead = true;
        }
    }

    public List<Item> Inventory { get; set; }

    public Warrior(string name)
    {
        Name = name;
        level = 1;
        Attack = 20;
        Defense = 10;
        MaxHealth = 100;
        CurrentHealth = 50;
        Class = "전사";
        Gold = 1000000;
        Inventory = new List<Item>();
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
                $"체력 : {CurrentHealth}/{MaxHealth}\n" +
                $"공격력: {Attack}\n" +
                $"방어력 : {Defense}\n" +
                $"Gold : {Gold} G\n");

            Console.Write("\n원하시는 행동을 입력해주세요.\n\n0. 나가기\n\n>> ");
            if ("0" == Console.ReadLine())
                break;
        }
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
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
                    if(Inventory[i].isEquip == false)
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
            if (input <= Inventory.Count && Inventory[input-1].isEquip == false)
            {
                Inventory[input - 1].Use(this);
                Inventory[input - 1].isEquip = true;
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

    public void Rest() // 4번 입력해서 여관으로 들어왔을 때
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
}

class Monster : ICharacter
{
    public string Name { get; set; }
    public int CurrentHealth { get; set; }
    public int Attack { get; set; }
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
        Console.WriteLine("텍스트 RPG 게임에 오신 것을 환영합니다.\n이름을 입력해주세요.\n\n");

        // 플레이어 정보
        string name = Console.ReadLine();
        Warrior player = new Warrior(name);

        List<Item> shopitems = new List<Item>();
        shopitems.Add(new IronArmor());
        shopitems.Add(new OldSword());
        shopitems.Add(new Spear());
        shopitems.Add(new SunMoonSword());

        int input;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"스파르타 던전에 오신 '{name}'님 환영합니다.\n이 곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 휴식 \n원하시는 행동을 입력해주세요. ");


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
