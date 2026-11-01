using ProgressionEngine.Core;

var cases = new (string Name, Action Check)[]
{
    ("initial upgrade cost", () => Equal(100L, ProgressionCalculator.UpgradeCost(0))),
    ("upgrade cost grows", () => Equal(225L, ProgressionCalculator.UpgradeCost(2))),
    ("fractional cost rounds up", () => Equal(2L, ProgressionCalculator.UpgradeCost(1, 1, 1.5m))),
    ("reward without rebirth", () => Equal(200L, ProgressionCalculator.EarnedCoins(100, 0))),
    ("rebirth bonus", () => Equal(250L, ProgressionCalculator.EarnedCoins(100, 1))),
    ("fractional reward rounds down", () => Equal(2L, ProgressionCalculator.EarnedCoins(1.9, 0, 1.5m))),
    ("affordability exact balance", () => True(ProgressionCalculator.CanAfford(100, 100))),
    ("affordability insufficient", () => True(!ProgressionCalculator.CanAfford(99, 100))),
    ("reject negative level", () => Throws<ArgumentOutOfRangeException>(() => ProgressionCalculator.UpgradeCost(-1))),
    ("reject negative distance", () => Throws<ArgumentOutOfRangeException>(() => ProgressionCalculator.EarnedCoins(-1, 0))),
    ("reject NaN", () => Throws<ArgumentOutOfRangeException>(() => ProgressionCalculator.EarnedCoins(double.NaN, 0))),
    ("reject negative rebirths", () => Throws<ArgumentOutOfRangeException>(() => ProgressionCalculator.EarnedCoins(1, -1))),
    ("reject cost overflow", () => Throws<OverflowException>(() => ProgressionCalculator.UpgradeCost(100))),
};
int failed = 0;
foreach (var (name, check) in cases)
{
    try { check(); Console.WriteLine($"PASS {name}"); }
    catch (Exception ex) { failed++; Console.Error.WriteLine($"FAIL {name}: {ex.Message}"); }
}
Console.WriteLine($"{cases.Length - failed}/{cases.Length} tests passed.");
return failed == 0 ? 0 : 1;

static void Equal(long expected, long actual)
{
    if (expected != actual) throw new Exception($"expected {expected}, got {actual}");
}
static void True(bool value)
{
    if (!value) throw new Exception("assertion failed");
}
static void Throws<T>(Action action) where T : Exception
{
    try { action(); }
    catch (T) { return; }
    throw new Exception($"expected {typeof(T).Name}");
}
