using CS8_FirstObjects.Models;

namespace CS8_FirstObjects.UnitTests;

public class ComplexNumber
    : Vector2D
{
    private ComplexNumber() {}

    public new static ComplexNumber FromRectangular(double x, double y)
        => new ComplexNumber() { X = x, Y = y };

    public new static ComplexNumber FromPolar(double r, AngleMeasure angle)
        => FromRectangular(r*Math.Cos(angle.ToUnit(AngularUnit.Radians).Theta), r*Math.Sin(angle.ToUnit(AngularUnit.Radians).Theta));
    
    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b) 
        => FromRectangular(a.X + b.X, a.Y + b.Y);
    
    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        => FromRectangular(a.X - b.X,a.Y - b.Y );
    
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        =>  FromPolar(a.Magnitude + b.Magnitude, a.Angle + b.Angle);
    
    
}

public static class ComplexExtensions
{
    public static ComplexNumber
        ToComplex(this Vector2D v)
        => ComplexNumber.FromRectangular(v.X, v.Y);
}