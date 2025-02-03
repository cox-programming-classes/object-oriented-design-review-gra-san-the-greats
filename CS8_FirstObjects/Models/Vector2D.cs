using System.Numerics;
using System.Text;

namespace CS8_FirstObjects.Models;

/// <summary>
/// Represents a 2D Vector for doing MATH
/// </summary>
public class Vector2D
{
    #region Properties
    /// <summary>
    /// X Coordinate of this Vector
    /// </summary>
    public required double X { get; init; }
    
    /// <summary>
    /// Y Coordinate of this Vector
    /// </summary>
    public required double Y { get; init; }
    
    /// <summary>
    /// Computed Magnitude of this vector.
    /// </summary>
    public double Magnitude => Math.Sqrt(X * X + Y * Y);
    
    /// <summary>
    /// Compute the Angle of this vector!
    /// </summary>
    public AngleMeasure Angle => new(Math.Atan2(Y, X));
    #endregion
    
    #region Constructors
    /// <summary>
    /// The Constructor is a special method that is use exclusively in
    /// initializing a `new` instance of the Type.
    ///
    /// In this case, it is `private` because I (the developer) am requiring you
    /// to use the two variations of the Factory methods.
    /// </summary>
    private Vector2D() { }

    #endregion

    /************************************************************************
     * Factory Methods are used in lieu of Constructors in some circumstances
     * In this case, I have chosen this method because there are two distinct
     * implementations of constructing a Vector2D and I want you to be able
     * be clear in your code which version you are using~
     ************************************************************************/
    #region Factory Methods    

    /// <summary>
    /// Create a new Vector using rectangular coordinates.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>a new Vector2D object with the given coordinates</returns>
    public static Vector2D FromRectangular(double x, double y) 
        => new() { X = x, Y = y };
    
    /// <summary>
    /// Create a new Vector using polar coordinates.
    /// </summary>
    /// <param name="r">Length of this vector</param>
    /// <param name="angle">Angle measured anti-clockwise from the positive x-axis</param>
    /// <returns></returns>
    public static Vector2D FromPolar(double r, AngleMeasure angle)
        =>  Vector2D.FromRectangular(r*Math.Cos(angle.ToUnit(AngularUnit.Radians).Theta), r*Math.Sin(angle.ToUnit(AngularUnit.Radians).Theta));

    
    #endregion
    
    #region Arithmetic Operators

    /// <summary>
    /// Add Two Vectors Together
    /// </summary>
    /// <param name="a">left side of +</param>
    /// <param name="b">right side of +</param>
    /// <returns>sum</returns>
    public static Vector2D operator +(Vector2D a, Vector2D b) 
        => FromRectangular(a.X + b.X, a.Y + b.Y);
    
    /// <summary>
    /// Subtract b from a
    /// </summary>
    /// <param name="a">left side of -</param>
    /// <param name="b">right side of -</param>
    /// <returns>difference</returns>
    public static Vector2D operator -(Vector2D a, Vector2D b)
        => FromRectangular(a.X - b.X,a.Y - b.Y );
    
    /// <summary>
    /// Compute the Dot-Product (aka Inner Product) of a and b
    /// </summary>
    /// <param name="a">left side of *</param>
    /// <param name="b">right side of *</param>
    /// <returns>Dot Product</returns>
    /// <exception cref="NotImplementedException">TODO:  Implement this!</exception>
    public static double operator *(Vector2D a, Vector2D b)
        =>  a.X*b.X+ a.Y* b.Y;
    
    /// <summary>
    /// Scale the vector a by a given constant factor.
    /// </summary>
    /// <param name="factor"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Vector2D operator *(double factor, Vector2D a)
        => FromRectangular(factor * a.X, factor * a.Y);
    
    #endregion
    
    #region Other Mathematical Calculations

    /// <summary>
    /// Get a Unit Vector in the same direction as this vector.
    /// </summary>
    /// <returns>A vector with Magnitude = 1 in the same direction as this vector.</returns>
    public Vector2D UnitVector()
        => FromPolar(1, this.Angle);

    /// <summary>
    /// Rotate this vector by a given angle!
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Vector2D Rotate(AngleMeasure angle)
        => FromPolar(this.Magnitude, this.Angle + angle);

    /// <summary>
    /// Compute the projection of this vector onto another vector.
    /// (You probably have not learned this in Math class yet!)
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException">TODO: Research and Implement~</exception>
    public Vector2D ProjectOnto(Vector2D other)
        => throw new NotImplementedException(); 
    #endregion
    
    #region ToString Operations
    /// <summary>
    /// Default way to represent a Vector2D as a pair of coordinates.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"({X}, {Y})";

    /// <summary>
    /// Custom String Format for Vector2D!
    /// allows the embedding of number formatting
    ///
    /// e.g.
    ///
    /// `v.ToString("[{X:0.00}, {Y:0.00}])`
    /// will print the vector in [...] and rounded
    /// to two decimal places.
    /// 
    /// `v.ToString("(R, T)")` will print Polar coordinates.
    /// 
    /// `v.ToString("(X, Y) => (R, {T:R})")` prints
    /// Both rectangular and polar, with the angle
    /// specifically in Radians.
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ToString(string? format)
    {
        format = format?.ToUpper();
        if(string.IsNullOrWhiteSpace(format) || format == "G") return ToString();

        
        StringBuilder output = new();

        for (var i = 0; i < format.Length; i++)
        {
            var c = format[i];
            if (c != 'X' && c != 'Y' && c != 'R' && c != 'T' && c != '{')
            {   // Exit Early whenever possible!
                output.Append(c);
                continue;
            }

            if (c == '{')
            {
                var j = i + 1;
                for (; j < format.Length && format[j] != '}'; j++) ; // find the next space.

                if (j == format.Length || format[j] != '}')
                {   // formatting error...
                    output.Append(c);
                    continue;
                }
                
                var substring = format[(i + 1)..j];
                var v = substring[0];
                if (substring.Length < 3 || 
                    (v != 'X' && v != 'Y' && v != 'R' && v != 'T') ||
                    substring[1] != ':')
                {   // formatting error...
                    output.Append(c);
                    continue;
                }
                
                var subformat = substring[2..];
                var formatted = v switch
                {
                    'X' => $"{X.ToString(subformat)}",
                    'Y' => $"{Y.ToString(subformat)}",
                    'R' => $"{Magnitude.ToString(subformat)}",
                    'T' => subformat switch
                    {
                        "D" => $"{Angle.ToUnit(AngularUnit.Degrees)}",
                        "R" => $"{Angle.ToUnit(AngularUnit.Radians)}",
                        _ => $"{Angle}:{subformat}"  // invalid format.
                    },
                    _ => ""
                };
                output.Append(formatted);
                i = j;
                continue;
            }
            
            var value = 
                c switch  // grab the value of either X or Y.
                {
                    'X' => $"{X}",
                    'Y' => $"{Y}",
                    'R' => $"{Magnitude}",
                    'T' => $"{Angle}",
                    _ => ""
                };
            output.Append(value);
        }
        
        return output.ToString();
    }
    
    #endregion
}
