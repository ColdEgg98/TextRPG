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
internal class Program
{
    static void Main(string[] args)
    {
        WriteManager WM = new WriteManager();

        // 플레이어 생성 혹은 불러오기
        Player player = new Player();
        Player loadedPlayer = Player.LoadProgress();

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
                player = new Player();
                player.Name = name;
            }
        }

        // 콘솔창을 종료해도 저장
        AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, e) => player.SaveProgress());

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

            string inputStr = Console.ReadLine();
            if (!int.TryParse(inputStr, out int input))
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                Thread.Sleep(600);
                continue;
            }

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
