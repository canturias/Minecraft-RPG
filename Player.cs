public class Player {
    public string? Name { get; set; }
    public int Health { get; set; } = 20;

    public void DisplayStats() {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{Name} the Warrior\n");
        Console.ResetColor();
        Console.Write($"Health: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{Health} HP\n");
        Console.WriteLine();
        Console.ResetColor();
    }

    public int Attack() {
        Random random = new Random();
        return random.Next(2, 6);
    }
}