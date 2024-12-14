using System;
using System.Collections.Generic;

class Hero {
    public string Name;
    public int Health;
    public int Damage;
}

class Marksman : Hero {
    public int Range;
    public double CriticalChance;
}

class Tank : Hero {
    public int Armour;
    public int Stun;
}

class Program {
    static void Main(string[] args) {
        Marksman hero001 = new Marksman();
        hero001.Name = "Layla";
        hero001.Health = 2000;
        hero001.Damage = 100;
        hero001.Range = 250;
        hero001.CriticalChance = 0.2;
        
        Console.WriteLine(hero001.Name);
        Console.WriteLine(hero001.Health);
        Console.WriteLine(hero001.Damage);
        Console.WriteLine(hero001.Range);
        Console.WriteLine(hero001.CriticalChance);
    }
}
