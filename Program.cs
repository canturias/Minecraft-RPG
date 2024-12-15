using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Mobs;
using Names;

class Program {
    static void Main() {
        Console.Title = "Minecraft RPG";
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Minecraft RPG – Score: 0\n");
        Console.ResetColor();

        Player player = new Player();
        string? username = null;
        while (string.IsNullOrEmpty(username) || username!.Length < 3 || !Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))  {
            Console.Write("Enter your name: ");
            username = Console.ReadLine();
            if (username!.Length < 2) {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Minecraft RPG ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("(Usernames must be at least 2 characters long.)\n\n");
                Console.ResetColor();
            }
            else if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$")) {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Minecraft RPG ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("(Usernames must only contain letters and numbers.)\n\n");
                Console.ResetColor();
            }
        } player.Name = username;
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Minecraft RPG – Score: 0\n");
        Console.ResetColor();
        player.DisplayStats();

        Random random = new Random();
        bool isPassive = false;
        Wait("to roam around the forest");

        int score = 0;
        while(player.Health > 0) {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Minecraft RPG – Score: {score}\n");
            Console.ResetColor();
            player.DisplayStats();

            string name = Name.ValidNames[random.Next(Name.ValidNames.Length)];
            int health, damage;

            if(!isPassive) {
                string type = Mob.ValidHostileTypes[random.Next(Mob.ValidHostileTypes.Length)];
                switch (type) {
                    case "Zombie":
                        health = random.Next(18, 23);
                        damage = random.Next(2, 5);
                        break;
                    case "Skeleton":
                        health = random.Next(18, 23);
                        damage = random.Next(0, 5);
                        break;
                    case "Spider":
                        health = random.Next(14, 18);
                        damage = random.Next(4, 7);
                        break;
                    default:
                        health = 15;
                        damage = 1;
                        break;
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Minecraft RPG – Score: {score}\n");
                Console.ResetColor();
                
                player.DisplayStats();

                Thread.Sleep(400);
                Console.Write(". ");
                Thread.Sleep(400);
                Console.Write(". ");
                Thread.Sleep(400);
                Console.Write(". ");
                Thread.Sleep(400);
                Console.WriteLine("A monster appears!\n");
                Thread.Sleep(800);
                Hostile hostile = new Hostile(name, type, health, damage);
                hostile.DisplayInfo();

                Console.WriteLine("Press 'A' to attack normally.");
                Console.WriteLine("Press 'C' to attempt a critcal attack.\n");
                
                int attempts = 0;

                while (hostile.HealthPoints > 0 && player.Health > 0) {
                    ConsoleKeyInfo input = Console.ReadKey(true);
                    int criticalBonus = 0;
                    if (input.Key == ConsoleKey.C) {
                        Console.Write("Answer this question right to get the critical bonus: ");
                        Random critical = new Random();

                        int num1 = critical.Next(1, 101);
                        int num2 = critical.Next(1, 101);
                        char[] operators = {'+', '-', '*'};

                        char operatorChar = operators[critical.Next(operators.Length)];
                        int correctAnswer;

                        if (operatorChar == '+') {
                            correctAnswer = num1 + num2;
                        }
                        else if (operatorChar == '-') {
                            correctAnswer = num1 - num2;
                        }
                        else {
                            correctAnswer = num1 * num2;
                        }

                        Console.Write($"{num1} {operatorChar} {num2} = ");
                        string? userAnswer = Console.ReadLine();

                        if (int.TryParse(userAnswer, out int parsedAnswer)) {
                            if (parsedAnswer == correctAnswer) {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Correct! You've earned the critical bonus!\n");
                                Console.ResetColor();
                                criticalBonus = 1;
                            }
                            else {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Incorrect! The correct answer was {correctAnswer}.\n");
                                Console.ResetColor();
                                criticalBonus = 2;
                            }
                        }
                        else {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Invalid input! The correct answer was {correctAnswer}!\n");
                            Console.ResetColor();
                            criticalBonus = 2;
                        }  
                    }

                    int playerDamage = player.Attack();

                    Thread.Sleep(400);
                    Console.Write($"Attacking {hostile.Name} ");
                    Thread.Sleep(400);
                    Console.Write(". ");
                    Thread.Sleep(400);
                    Console.Write(". ");
                    Thread.Sleep(400);
                    Console.WriteLine(". ");

                    if (criticalBonus == 1) {
                        Console.WriteLine($"You attack {hostile.Name} the {hostile.Type} dealing {playerDamage} HP of damage + a 6 HP critical bonus!");
                        hostile.HealthPoints -= playerDamage + 6;
                    }
                    else if (criticalBonus == 2) {
                        Console.WriteLine($"You attack {hostile.Name} the {hostile.Type} dealing {playerDamage} HP of damage - a 3 HP deduction for answering incorrectly!");
                        hostile.HealthPoints -= playerDamage - 3;
                    }
                    else {
                        Console.WriteLine($"You attack {hostile.Name} the {hostile.Type} dealing {playerDamage} HP of damage!");
                        hostile.HealthPoints -= playerDamage;
                    }
                    
                    if (hostile.HealthPoints > 0) {
                        Console.WriteLine();
                        Thread.Sleep(1000);
                        Console.Write($"{hostile.Name} is attacking you ");
                        Thread.Sleep(400);
                        Console.Write(". ");
                        Thread.Sleep(400);
                        Console.Write(". ");
                        Thread.Sleep(400);
                        Console.WriteLine(". ");
                        
                        Console.WriteLine($"{hostile.Name} the {hostile.Type} attacks you dealing {hostile.Damage} HP of damage!");
                        player.Health -= hostile.Damage;
                        attempts++;

                        Console.Write("\nPress ENTER to continue . . .");
                        Console.ReadKey(true);

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Minecraft RPG – Score: {score}\n");
                        Console.ResetColor();
                        
                        player.DisplayStats();
                        hostile.DisplayInfo();

                        Console.WriteLine("Press 'A' to attack normally.");
                        Console.WriteLine("Press 'C' to attempt a critcal attack.\n");
                    }
                    else {
                        if (attempts == 2) {
                            Console.WriteLine($"\nYou defeated {hostile.Name} the {hostile.Type} in three hits! You have gained {hostile.HealthDrops} + a 6 HP bonus!\n");
                            player.Health += hostile.HealthDrops + 6;
                        }
                        else {
                            Console.WriteLine($"\nYou defeated {hostile.Name} the {hostile.Type}! You have gained {hostile.HealthDrops} HP!\n");
                            player.Health += hostile.HealthDrops;
                        }
                        score++;
                        Wait("to roam around the forest");
                    }
                }
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
                Thread.Sleep(400);
                Console.Write(". ");
                Thread.Sleep(400);
                Console.Write(". ");
                Thread.Sleep(400);
                Console.Write(". ");
                Thread.Sleep(400);
                Console.WriteLine("An animal appears!\n");
                Thread.Sleep(800);

                Passive passive = new Passive(name, type, health);
                passive.DisplayInfo();
                Console.WriteLine("Press 'A' to attack normally.");
                Console.ReadKey(true);

                int attempts = 0;
                while (passive.HealthPoints > 0) {
                    int playerDamage = player.Attack();

                    Thread.Sleep(400);
                    Console.Write($"\nAttacking {passive.Name} ");
                    Thread.Sleep(400);
                    Console.Write(". ");
                    Thread.Sleep(400);
                    Console.Write(". ");
                    Thread.Sleep(400);
                    Console.WriteLine(". ");

                    Console.WriteLine($"\nYou attack {passive.Name} the {passive.Type} dealing {playerDamage + 3} HP of damage!");
                    passive.HealthPoints -= playerDamage + 3;

                    if (passive.HealthPoints <= 0) {
                        if (attempts == 0) {
                            Console.WriteLine($"\nYou killed {passive.Name} the {passive.Type} in one hit! You have gained {passive.HealthDrops} HP + a 5 HP bonus!\n");
                            player.Health += passive.HealthDrops + 5;
                            Wait("to roam around the forest");
                        }

                        else if (attempts == 1) {
                            Console.WriteLine($"\nYou killed {passive.Name} the {passive.Type} in two hits! You have gained {passive.HealthDrops} HP + a 4 HP bonus!\n");
                            player.Health += passive.HealthDrops + 4;
                            Wait("to roam around the forest");
                        }
                        else {
                            Console.WriteLine($"\nYou killed {passive.Name} the {passive.Type}! You have gained {passive.HealthDrops} HP!\n");
                            player.Health += passive.HealthDrops;
                            Wait("to roam around the forest");
                        }

                        Console.Clear();
                        Console.WriteLine($"Minecraft RPG – Score: {score}\n");
                        player.DisplayStats();
                    }
                    else {
                        attempts++;
                        Console.Write("\nPress ENTER to continue . . .");
                        Console.ReadKey(true);
                        Console.Clear();
                        Console.WriteLine($"Minecraft RPG – Score: {score}\n");
                        player.DisplayStats();
                        passive.DisplayInfo();
                        Console.WriteLine("Press 'A' to attack normally.");
                        Console.ReadKey(true);
                    }  
                }
            }
            isPassive = random.Next(3) == 0;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Game Over! You have been defeated.");
        Console.Write("Final score: " + score);
        Console.ReadKey();
    }

    static void Wait(string message) {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($"Press ENTER to {message} . . .");
        Console.ResetColor();
        Console.ReadKey(true);
    }
}