namespace MoonBark.Core.Types;

using System;

/// <summary>
/// Core vector3 type for 3D operations.
/// Engine-agnostic representation of a 3D vector for cross-platform compatibility.
/// </summary>
public readonly struct CoreVector3 : IEquatable<CoreVector3>
{
    public readonly float X;
    public readonly float Y;
    public readonly float Z;

    public static readonly CoreVector3 Zero = new(0, 0, 0);
    public static readonly CoreVector3 One = new(1, 1, 1);
    public static readonly CoreVector3 Up = new(0, 1, 0);
    public static readonly CoreVector3 Down = new(0, -1, 0);
    public static readonly CoreVector3 Left = new(-1, 0, 0);
    public static readonly CoreVector3 Right = new(1, 0, 0);
    public static readonly CoreVector3 Forward = new(0, 0, -1);
    public static readonly CoreVector3 Back = new(0, 0, 1);

    public CoreVector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public float LengthSquared => X * X + Y * Y + Z * Z;
    public float Length => MathF.Sqrt(LengthSquared);

    public bool Equals(CoreVector3 other) => 
        Math.Abs(X - other.X) < 1e-6f &&
        Math.Abs(Y - other.Y) < 1e-6f &&
        Math.Abs(Z - other.Z) < 1e-6f;

    public override bool Equals(object? obj) => obj is CoreVector3 other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    public static bool operator ==(CoreVector3 left, CoreVector3 right) => left.Equals(right);
    public static bool operator !=(CoreVector3 left, CoreVector3 right) => !left.Equals(right);

    public static CoreVector3 operator +(CoreVector3 left, CoreVector3 right) => 
        new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    public static CoreVector3 operator -(CoreVector3 left, CoreVector3 right) => 
        new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    public static CoreVector3 operator *(CoreVector3 vector, float scalar) => 
        new(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);

    public static CoreVector3 operator *(float scalar, CoreVector3 vector) => 
        vector * scalar;

    public static CoreVector3 operator /(CoreVector3 vector, float scalar) => 
        new(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);

    public CoreVector3 Normalized()
    {
        var length = Length;
        return length > 0f ? this / length : Zero;
    }

    public float Dot(CoreVector3 other) => X * other.X + Y * other.Y + Z * other.Z;

    public CoreVector3 Cross(CoreVector3 other) => 
        new(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y * other.X);

    public override string ToString() => $"CoreVector3({X:F3}, {Y:F3}, {Z:F3})";
}
