using System;
using System.Collections.Generic;

class Mob {
    public string Name { get; private set; }
    public string Type { get; private set; }
    public int HealthPoints { get; private set; }

    public Mob(string name, string type, int healthPoints) {
        Name = name;
        Type = type;
        HealthPoints = healthPoints;
    }
    
    public virtual void DisplayInfo() {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Type: " + Type);
        Console.WriteLine("Health Points: " + HealthPoints + " HP");
    }
}

class Hostile : Mob {
    public int Damage { get; private set; }
    
    public Hostile(string name, string type, int healthPoints, int damage)
    :base(name, type, healthPoints) {
        Damage = damage;    
    }
    
    public override void DisplayInfo() {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("(New Hostile)");
        Console.ResetColor();
        base.DisplayInfo();
        Console.WriteLine("Damage: " + Damage  + " ATK");
        Console.WriteLine();
    }

}

class Passive : Mob {
    public int HealthDrops { get; private set; }

    public Passive(string name, string type, int healthPoints, int healthDrops)
    :base(name, type, healthPoints) {
        HealthDrops = healthDrops;    
    }
    
    public override void DisplayInfo() {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("(New Passive)");
        Console.ResetColor();
        base.DisplayInfo();
        Console.WriteLine("Health Drops: " + HealthDrops + " HP");
        Console.WriteLine();
    }
}

class Neutral : Mob {
    public int Damage { get; private set; }
    public bool IsAggressive { get; private set;}

    public Neutral(string name, string type, int healthPoints, int damage, bool isAggressive)
    :base(name, type, healthPoints) {
        Damage = damage;
        IsAggressive = isAggressive;
    }
    
    public override void DisplayInfo() {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("(New Neutral)");
        Console.ResetColor();
        base.DisplayInfo();
        Console.WriteLine("Damage: " + Damage + " ATK");
        Console.WriteLine("Aggressive: " + IsAggressive);
        Console.WriteLine();
    }
}

class Program {
    static void Main(string[] args) {
        Console.Title = "Minecraft: Terminal Edition (Beta)";

        Mob test001 = new Hostile(
            "Chris",
            "Creeper",
            20,
            100
        );

        Mob test002 = new Passive(
            "Jerome",
            "Chicken",
            4,
            2
        );

        Mob test003 = new Neutral(
            "Darryl",
            "Enderman",
            40,
            4,
            false
        );
        
        test001.DisplayInfo();
        test002.DisplayInfo();
        test003.DisplayInfo();

        Console.ReadKey();
    }
}