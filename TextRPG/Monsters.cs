using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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