using System;
using System.Collections.Generic;

using Mobs;
using Names;

class Program {
    static void Main() {
        Console.Title = "Minecraft RPG (Beta)";
        List<Mob> Mobs = new List<Mob>();
        Random random = new Random();
        bool isPassive = false;
        Console.WriteLine("Minecraft RPG (Beta)\n");
        Wait("create a new random object");

        while(true) {
            Console.Clear();
            Console.WriteLine("Minecraft RPG (Beta)\n");

            string name = Name.ValidNames[random.Next(Name.ValidNames.Length)];
            int health, damage;

            if(!isPassive) {
                string type = Mob.ValidHostileTypes[random.Next(Mob.ValidHostileTypes.Length)];
                switch (type) {
                    case "Zombie":
                        health = random.Next(18, 23);
                        damage = random.Next(1, 4);
                        break;
                    case "Skeleton":
                        health = random.Next(18, 23);
                        damage = random.Next(0, 4);
                        break;
                    case "Spider":
                        health = random.Next(14, 18);
                        damage = random.Next(2, 5);
                        break;
                    default:
                        health = 15;
                        damage = 1;
                        break;
                }
                Mobs.Add(new Hostile(name, type, health, damage));
            } else {
                string type = Mob.ValidPassiveTypes[random.Next(Mob.ValidPassiveTypes.Length)];
                switch (type) {
                    case "Pig":
                        health = random.Next(10, 13);
                        break;
                    case "Chicken":
                        health = random.Next(4, 7);
                        break;
                    case "Cow":
                        health = random.Next(10, 13);
                        break;
                    default:
                        health = 5;
                        break;
                }
                Mobs.Add(new Passive(name, type, health));
            }
            isPassive = random.Next(3) == 0;
        
            for(int i = 0; i < Mobs.Count; i++) {
               Mobs[i].DisplayInfo();
            }

            Wait("create a new random object");
        }
    }

    static void Wait(string message) {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($"Press any key to {message} . . .");
        Console.ResetColor();
        Console.ReadKey(true);
    }
}