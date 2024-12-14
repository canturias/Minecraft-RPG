using System;

namespace Mobs {
    abstract class Mob {
        public string MobID { get; protected set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public int HealthPoints { get; set; }
        public int HealthDrops { get; protected set; }

        public static readonly string[] ValidHostileTypes = { "Zombie", "Skeleton", "Spider" };
        public static readonly string[] ValidPassiveTypes = { "Cow", "Pig", "Chicken" };

        protected Mob(string name, string type, int healthPoints) {
            MobID = "";
            Name = name;
            if (!ValidHostileTypes.Contains(type) && !ValidPassiveTypes.Contains(type)) {
                throw new ArgumentException("The type is invalid.", nameof(type));
            } Type = type;
            if (healthPoints < 0) {
                throw new ArgumentException("The health can't be negative.", nameof(healthPoints));
            } HealthPoints = healthPoints;
            HealthDrops = healthPoints;
        }

        public virtual void DisplayInfo() {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{Name} the {Type}\n");
            Console.ResetColor();
            Console.Write($"Health: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{HealthPoints} HP\n");
            Console.ResetColor();
        }
    }

    class Hostile : Mob {
        private static int nextMobID = 1;
        public int Damage { get; private set; }

        public Hostile(string name, string type, int healthPoints, int damage)
        :base(name, type, healthPoints) {
            MobID = $"H-0{nextMobID}";
            HealthDrops /= 3;
            Damage = damage;
            nextMobID++;
        }

        public override void DisplayInfo() {
            base.DisplayInfo();
            Console.WriteLine();
        }
    }

    class Passive : Mob {
        private static int nextMobID = 1;

        public Passive(string name, string type, int healthPoints)
        :base(name, type, healthPoints) {
            MobID = $"P-0{nextMobID}";
            HealthDrops /= 2;
            nextMobID++;
        }

        public override void DisplayInfo() {
            base.DisplayInfo();
            Console.WriteLine();
        }
    }
}
