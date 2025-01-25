// See https://aka.ms/new-console-template for more information

using CS8_FirstObjects.Models;
using CS8_FirstObjects.UnitTests;

Console.WriteLine("Test some Angles!");
var angles = AngleTests.GenerateRandomAngleMeasures();
AngleTests.PrintAngles(angles);

Console.WriteLine("Test some Vectors!");
var vectors = Vector2DTests.GenerateRandomVectors();
Vector2DTests.TestPrinting(vectors);