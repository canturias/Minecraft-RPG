using System;
using System.Collections.Generic;

namespace Minecraft_Terminal_Edition {
    class Program {
        private int Score;
        static void Wait() {
            Console.Write("Press any key to continue . . .");
            Console.ReadKey(true);
        }

        static void Main(string[] args) {
            Console.Title = "Minecraft: Terminal Edition";

            int a = 0;
            List<Mob> mobs = new List<Mob>();

            Random random = new Random();
            Console.WriteLine("Press enter . . .");
            Console.ReadKey(true);
            Console.Clear();

            bool isItem = false;
            bool isPassive = false;
            while (true) {
                if (isItem) {
                    Console.WriteLine("You found an item!\n");
                }
                else {
                    string name = "Juan";
                    int health, damage, heal;

                    if (!isPassive) {
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
                        heal = health / 3 + 1;
                        mobs.Add(new Hostile(name, type, health, heal, damage));
                    }
                    else {
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
                        heal = health / 2 + 1;
                        mobs.Add(new Passive(name, type, health, heal));
                    }
                    
                    mobs[a].DisplayInfo();
                    a++;
                }
                isItem = random.Next(7) == 0;
                isPassive = random.Next(3) == 0;
                Console.ReadKey(true);
            }
        }
    }
}