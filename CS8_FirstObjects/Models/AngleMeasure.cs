namespace CS8_FirstObjects.Models;


/// <summary>
/// Readonly data type that represents an angular measurement
/// Includes the Units so that you can take that into account!
/// </summary>
/// <param name="Theta">Value of this Angle</param>
/// <param name="Unit">Radians or Degrees?</param>
public readonly record struct AngleMeasure(double Theta = 0, AngularUnit Unit = AngularUnit.Radians)
{
    public AngleMeasure ToUnit(AngularUnit newUnit)
    {
        if (newUnit == Unit) // requested unit is already here.
            return this;

        // Pattern Matching using `switch`!
        // The grey text will go away once you implement the two variant code paths.
        return new AngleMeasure(Unit: newUnit, Theta: newUnit switch
        {
            //Im really confused by this --gracie
            // if the newUnit is Radians...
            AngularUnit.Radians => Theta * (180 / Math.PI),
            // if the newUnit is Degrees...
            AngularUnit.Degrees => Theta * (Math.PI / 180),
            // otherwise... (someone gave you bad data)
            _ => throw new InvalidOperationException("Unknown AngularUnit")
        });
    }

    /// <summary>
    /// Override the default ToString conversion for this type.
    /// TODO:  Extra challenge - If the Unit is Radians,
    /// factor out a PI and print the symbol as part of the String.
    /// (Use Pattern Matching!) 
    /// </summary>
    /// <returns></returns>
    public override string ToString() => 
        $"{Theta:0.00}{Unit.Symbol()}";

    /// <summary>
    /// Add two angles together...
    /// The resulting angle will be in the same units as `a`
    /// regardless of the units of `b`
    /// </summary>
    /// <param name="a">left side of +</param>
    /// <param name="b">right side of +</param>
    /// <returns>sum</returns>
    public static AngleMeasure operator +(AngleMeasure a, AngleMeasure b)
    {
        // Here's a pattern that you can use with records where you only need
        // change \some\ of the values.  
        if(a.Unit == b.Unit) 
            return a with { Theta = a.Theta + b.Theta };
        
        // if the units are not the same, then we have to convert one!
        return a with { Theta = a.Theta + b.ToUnit(a.Unit).Theta };
    }

    /// <summary>
    /// subtract b from a...
    /// The resulting angle will be in the same units as `a`
    /// regardless of the units of `b`
    /// </summary>
    /// <param name="a">left side of -</param>
    /// <param name="b">right side of -</param>
    /// <returns>difference</returns>
    public static AngleMeasure operator -(AngleMeasure a, AngleMeasure b)
    {
        if(a.Unit == b.Unit) 
            return a with { Theta = a.Theta - b.Theta };
        
        // if the units are not the same, then we have to convert one!
        return a with { Theta = a.Theta - b.ToUnit(a.Unit).Theta };
    }
       
}

/// <summary>
/// Indicates the Units used in a given angle.
/// </summary>
public enum AngularUnit
{
    Radians,
    Degrees
}

/// <summary>
/// Extensions for the Angle things
/// </summary>
public static class AngleMeasureExtensions
{
    /// <summary>
    /// Convert the Enumeration value to String for printing.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static string Symbol(this AngularUnit unit) =>
        unit switch
        {
            AngularUnit.Radians => " Rad",
            AngularUnit.Degrees => "\u00b0",
            _ => ""
        };

    /// <summary>
    /// Swap Radians for Degrees and vice versa.
    /// all invalid units become Radians.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static AngularUnit Swap(this AngularUnit unit) =>
        unit switch
        {
            AngularUnit.Radians => AngularUnit.Degrees,
            AngularUnit.Degrees => AngularUnit.Radians,
            _ => AngularUnit.Radians
        };
}