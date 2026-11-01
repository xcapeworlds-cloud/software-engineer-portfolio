namespace ProgressionEngine.Core;

/// <summary>Pure, deterministic progression rules independent of Unity.</summary>
public static class ProgressionCalculator
{
    public static long UpgradeCost(int level, long baseCost = 100, decimal multiplier = 1.5m)
    {
        if (level < 0) throw new ArgumentOutOfRangeException(nameof(level));
        if (baseCost <= 0) throw new ArgumentOutOfRangeException(nameof(baseCost));
        if (multiplier < 1m) throw new ArgumentOutOfRangeException(nameof(multiplier));
        decimal cost = baseCost;
        checked
        {
            for (int i = 0; i < level; i++) cost *= multiplier;
            return decimal.ToInt64(decimal.Ceiling(cost));
        }
    }

    public static long EarnedCoins(double distance, int rebirths, decimal coinsPerUnit = 2m)
    {
        if (double.IsNaN(distance) || double.IsInfinity(distance) || distance < 0)
            throw new ArgumentOutOfRangeException(nameof(distance));
        if (rebirths < 0) throw new ArgumentOutOfRangeException(nameof(rebirths));
        if (coinsPerUnit < 0) throw new ArgumentOutOfRangeException(nameof(coinsPerUnit));
        checked
        {
            decimal reward = (decimal)distance * coinsPerUnit * (1m + rebirths * 0.25m);
            return decimal.ToInt64(decimal.Floor(reward));
        }
    }

    public static bool CanAfford(long coins, long cost) => coins >= 0 && cost >= 0 && coins >= cost;
}
