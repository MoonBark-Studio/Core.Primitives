using System;

namespace MoonBark.Core.Types;

/// <summary>
/// Enhanced 2D vector type with extended functionality for MoonBark ecosystem.
/// Provides consistent vector representation across Godot and Unity projects.
/// </summary>
public readonly struct CoreVector2 : IEquatable<CoreVector2>
{
    public readonly float X;
    public readonly float Y;

    public CoreVector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static readonly CoreVector2 Zero = new(0f, 0f);
    public static readonly CoreVector2 One = new(1f, 1f);
    public static readonly CoreVector2 Up = new(0f, 1f);
    public static readonly CoreVector2 Down = new(0f, -1f);
    public static readonly CoreVector2 Left = new(-1f, 0f);
    public static readonly CoreVector2 Right = new(1f, 0f);

    public float LengthSquared => X * X + Y * Y;
    public float Length => MathF.Sqrt(LengthSquared);

    public bool Equals(CoreVector2 other) => 
        Math.Abs(X - other.X) < 1e-6f &&
        Math.Abs(Y - other.Y) < 1e-6f;

    public override bool Equals(object? obj) => obj is CoreVector2 other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public static bool operator ==(CoreVector2 left, CoreVector2 right) => left.Equals(right);
    public static bool operator !=(CoreVector2 left, CoreVector2 right) => !left.Equals(right);

    public static CoreVector2 operator +(CoreVector2 left, CoreVector2 right) => 
        new(left.X + right.X, left.Y + right.Y);

    public static CoreVector2 operator -(CoreVector2 left, CoreVector2 right) => 
        new(left.X - right.X, left.Y - right.Y);

    public static CoreVector2 operator *(CoreVector2 vector, float scalar) => 
        new(vector.X * scalar, vector.Y * scalar);

    public static CoreVector2 operator *(float scalar, CoreVector2 vector) => 
        vector * scalar;

    public static CoreVector2 operator /(CoreVector2 vector, float scalar) => 
        new(vector.X / scalar, vector.Y / scalar);

    public CoreVector2 Normalized()
    {
        var length = Length;
        return length > 0f ? this / length : Zero;
    }

    public float Dot(CoreVector2 other) => X * other.X + Y * other.Y;

    public CoreVector2 GetDirectionTo(CoreVector2 target)
    {
        float dx = target.X - X;
        float dy = target.Y - Y;
        float len = MathF.Sqrt(dx * dx + dy * dy);
        return len > 1e-6f ? new CoreVector2(dx / len, dy / len) : Zero;
    }

    public CoreVector2 MoveInDirection(CoreVector2 direction)
        => new(X + direction.X, Y + direction.Y);

    public CoreVector2 MoveInDirection(CoreVector2 direction, float steps)
        => new(X + direction.X * steps, Y + direction.Y * steps);

    public CoreVector2 Clamp(CoreVector2 min, CoreVector2 max)
        => new(
            Math.Max(min.X, Math.Min(max.X, X)),
            Math.Max(min.Y, Math.Min(max.Y, Y)));

    public override string ToString() => $"CoreVector2({X:F3}, {Y:F3})";
}
