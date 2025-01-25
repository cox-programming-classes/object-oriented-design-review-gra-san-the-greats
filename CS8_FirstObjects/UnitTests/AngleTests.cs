using System.Diagnostics;
using CS8_FirstObjects.Models;

namespace CS8_FirstObjects.UnitTests;

public static class AngleTests
{
    // A seeded Random generator will always generate the same random sequence
    // this is useful for testing!
    private static readonly Random _random = new(12345);
    
    /// <summary>
    ///  Generate some random angles
    /// </summary>
    /// <param name="quantity"></param>
    /// <returns></returns>
    /// <exception cref="UnreachableException"></exception>
    public static List<AngleMeasure> GenerateRandomAngleMeasures(int quantity = 10)
    {
        var list = new List<AngleMeasure>();
        for (var i = 0; i < quantity; i++)
        {
            var unit = (AngularUnit)_random.Next(2); // pick a random unit.
            
            list.Add(unit switch
            {
                AngularUnit.Degrees => new(_random.NextDouble()*360, unit),
                AngularUnit.Radians => new(_random.NextDouble()* 2 * Math.PI, unit),
                _ => throw new UnreachableException("If this happens, something went wrong in the Random class lol.")
            });
        }
        return list;
    }

    public static void PrintAngles(List<AngleMeasure> angles)
    {
        angles.ForEach(angle => Console.WriteLine(angle));
    }

    public static void TestArithmetic(int repeats = 10)
    {
        for (var i = 0; i < repeats; i++)
        {
            var(a, b) = (GenerateRandomAngleMeasures(1)[0], GenerateRandomAngleMeasures(1)[0]);
            Console.WriteLine($"{a}+{b} = {a + b}");
            Console.WriteLine($"{a}-{b} = {a - b}");
            Console.WriteLine("_________________________________");
        }
    }

    public static void TestConversions(List<AngleMeasure> angles)
    {
        angles.ForEach(angle => Console.WriteLine($"{angle} => {angle.ToUnit(angle.Unit.Swap())}"));
    }
}