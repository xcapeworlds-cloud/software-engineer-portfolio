using System.Globalization;
using ProgressionEngine.Core;

Console.WriteLine("Progression Engine — C#/.NET 8 demo");
Console.WriteLine("Enter distance traveled, rebirth count, and upgrade level.");
try
{
    Console.Write("Distance: ");
    double distance = double.Parse(Console.ReadLine() ?? "", CultureInfo.InvariantCulture);
    Console.Write("Rebirths: ");
    int rebirths = int.Parse(Console.ReadLine() ?? "", CultureInfo.InvariantCulture);
    Console.Write("Current upgrade level: ");
    int level = int.Parse(Console.ReadLine() ?? "", CultureInfo.InvariantCulture);
    long reward = ProgressionCalculator.EarnedCoins(distance, rebirths);
    long cost = ProgressionCalculator.UpgradeCost(level);
    Console.WriteLine($"Reward: {reward:N0} coins");
    Console.WriteLine($"Next upgrade: {cost:N0} coins");
    Console.WriteLine($"Can buy from this reward: {ProgressionCalculator.CanAfford(reward, cost)}");
}
catch (Exception ex) when (ex is FormatException or OverflowException or ArgumentOutOfRangeException)
{
    Console.Error.WriteLine($"Invalid input: {ex.Message}");
    Environment.ExitCode = 1;
}
