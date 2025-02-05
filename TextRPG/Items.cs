using System.Text.Json.Serialization;

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

    public abstract void Use(Player warrior);
    public abstract void UnUse(Player warrior);
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
    public override void Use(Player warrior)
    {
        warrior.Defense += Value;
    }
    public override void UnUse(Player warrior)
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
    public override void Use(Player warrior)
    {
        warrior.Attack += Value;
    }
    public override void UnUse(Player warrior)
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
    public override void Use(Player warrior)
    {
        warrior.Attack += Value;
    }
    public override void UnUse(Player warrior)
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
    public override void Use(Player warrior)
    {
        warrior.Attack += Value;
    }
    public override void UnUse(Player warrior)
    {
        warrior.Attack -= Value;
    }
}
public class Healingpotion : Item
{
    public Healingpotion()
    {
        Name = "힐링 포션";
        Type = "체력 회복";
        Description = "사용하면 체력 30을 회복합니다.";
        Value = 30;
        Price = 400;
    }
    public override void Use(Player warrior)
    {
        if (warrior.CurrentHealth + Value > warrior.MaxHealth)
            warrior.CurrentHealth = warrior.MaxHealth;
        else
            warrior.CurrentHealth += Value;
    }
    public override void UnUse(Player warrior) { }
}
