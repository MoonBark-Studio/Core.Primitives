using System;

namespace GridPlacement.Core.Types;

/// <summary>
/// Enhanced 2D integer vector type for grid operations in MoonBark ecosystem.
/// Provides consistent integer grid coordinates across Godot and Unity projects.
/// </summary>
public readonly struct CoreVector2I : IEquatable<CoreVector2I>
{
    public readonly int X;
    public readonly int Y;

    public CoreVector2I(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static readonly CoreVector2I Zero = new(0, 0);
    public static readonly CoreVector2I One = new(1, 1);
    public static readonly CoreVector2I Up = new(0, 1);
    public static readonly CoreVector2I Down = new(0, -1);
    public static readonly CoreVector2I Left = new(-1, 0);
    public static readonly CoreVector2I Right = new(1, 0);

    public bool Equals(CoreVector2I other) => X == other.X && Y == other.Y;

    public override bool Equals(object? obj) => obj is CoreVector2I other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public static bool operator ==(CoreVector2I left, CoreVector2I right) => left.Equals(right);
    public static bool operator !=(CoreVector2I left, CoreVector2I right) => !left.Equals(right);

    public static CoreVector2I operator +(CoreVector2I left, CoreVector2I right) => 
        new(left.X + right.X, left.Y + right.Y);

    public static CoreVector2I operator -(CoreVector2I left, CoreVector2I right) => 
        new(left.X - right.X, left.Y - right.Y);

    public static CoreVector2I operator *(CoreVector2I vector, int scalar) => 
        new(vector.X * scalar, vector.Y * scalar);

    public static CoreVector2I operator *(int scalar, CoreVector2I vector) => 
        vector * scalar;

    public static CoreVector2I operator /(CoreVector2I vector, int scalar) => 
        new(vector.X / scalar, vector.Y / scalar);

    public int DistanceSquared(CoreVector2I other) => 
        (X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y);

    public float Distance(CoreVector2I other) => MathF.Sqrt(DistanceSquared(other));

    public CoreVector2 ToFloat() => new(X, Y);

    public override string ToString() => $"CoreVector2I({X}, {Y})";
}
