using System;
using System.Collections.Generic;

namespace Minecraft_Terminal_Edition {
    abstract class Mob {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public int Health { get; private set; }
        public int Heal { get; private set; }

        public static readonly string[] ValidHostileTypes = { "Zombie", "Skeleton", "Spider" };
        public static readonly string[] ValidPassiveTypes = { "Pig", "Chicken", "Cow" };

        public Mob(string name, string type, int health, int heal) {
            Name = name;
            if (!ValidHostileTypes.Contains(type) && !ValidPassiveTypes.Contains(type)) {
                throw new ArgumentException("The type is invalid.", nameof(type));
            }
            Type = type;
            if (health < 0) {
                throw new ArgumentException("The health cannot be negative.", nameof(health));
            }
            Health = health;
            if (heal < 0) {
                throw new ArgumentException("The heal amount cannot be negative.", nameof(health));
            }
            Heal = heal;
        }

        public virtual void DisplayInfo() {
            Console.ResetColor();
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Type: {Type}");
            Console.Write($"Health: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Health} HP");
            Console.ResetColor();
            Console.Write("Heal Amount: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Heal} HP");
            Console.ResetColor();
        }
    }

    class Hostile : Mob {
        public int Damage { get; private set; }

        public Hostile(string name, string type, int health, int heal, int damage) : base(name, type, health, heal) {
            if (damage < 0) {
                throw new ArgumentException("The damage cannot be negative.", nameof(damage));
            }
            Damage = damage;
        }

        public void Attack() {}

        public override void DisplayInfo() {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[New Hostile]");
            base.DisplayInfo();
            Console.Write($"Damage: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Damage} ATK\n");
            Console.ResetColor();
        }
    }

    class Passive : Mob {
        public Passive(string name, string type, int health, int heal) : base(name, type, health, heal) {}

        public override void DisplayInfo() {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("[New Passive]");
            base.DisplayInfo();
            Console.WriteLine();
        }
    }
}