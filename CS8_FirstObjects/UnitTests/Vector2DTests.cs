using CS8_FirstObjects.Models;

namespace CS8_FirstObjects.UnitTests;

public static class Vector2DTests
{
    private static readonly Random _random = new(12345);

    public static List<Vector2D> GenerateRandomVectors(int repeats = 10)
    {
        List<Vector2D> tests = new();
        for (int i = 0; i < repeats; i++)
        {
            bool polar = _random.Next(2) == 0;
            if (polar)
            {
                try
                {
                    var angle = AngleTests.GenerateRandomAngleMeasures(1)[0];
                    tests.Add(Vector2D.FromPolar(20*_random.NextDouble() - 10.0, angle));
                    continue;
                }
                catch
                {
                    // Ignore the NotImplemented Exception.
                }
            }
            
            tests.Add(Vector2D.FromRectangular(20*_random.NextDouble() - 10.0, 20*_random.NextDouble() - 10.0));
        }
        
        return tests;
    }

    public static void TestPrinting(List<Vector2D> vectors)
    {
        List<string> formats = 
            [
                "<x, y>", 
                "<{x:0.00}, {y:0.00}>", 
                /* TODO: Add more formats as you complete parts of the Vector2D class */ 
            ];

        foreach (var vector in vectors)
        {
            Console.WriteLine($"Default: {vector}");
            foreach(var format in formats)
                Console.WriteLine($"{format} => {vector.ToString(format)}");
            Console.WriteLine("_________________________________________");
        }
    }

    /// <summary>
    /// TODO:  Test all the arithmetic operations you have written!
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public static void TestArithmeticOperators()
    {
        throw new NotImplementedException();
    }
    
}