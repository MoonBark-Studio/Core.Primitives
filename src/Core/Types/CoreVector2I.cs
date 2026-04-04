using System;
using System.Collections.Generic;

namespace MoonBark.Core.Types;

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

    // --- Grid positioning instance methods ---

    public int ManhattanDistanceTo(CoreVector2I other)
        => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    public int ChebyshevDistanceTo(CoreVector2I other)
        => Math.Max(Math.Abs(X - other.X), Math.Abs(Y - other.Y));

    public CoreVector2I GetDirectionTo(CoreVector2I target)
        => new(
            target.X == X ? 0 : (target.X > X ? 1 : -1),
            target.Y == Y ? 0 : (target.Y > Y ? 1 : -1));

    public CoreVector2I MoveInDirection(CoreVector2I direction, int distance = 1)
        => new(X + direction.X * distance, Y + direction.Y * distance);

    public CoreVector2I Clamp(CoreVector2I min, CoreVector2I max)
        => new(Math.Max(min.X, Math.Min(max.X, X)), Math.Max(min.Y, Math.Min(max.Y, Y)));

    public bool IsWithinRegion(CoreVector2I min, CoreVector2I max)
        => X >= min.X && X <= max.X && Y >= min.Y && Y <= max.Y;

    public List<CoreVector2I> GetOrthogonalPositions()
        => [new CoreVector2I(X + 1, Y), new CoreVector2I(X - 1, Y),
            new CoreVector2I(X, Y + 1), new CoreVector2I(X, Y - 1)];

    public List<CoreVector2I> GetAdjacentPositions()
    {
        var positions = new List<CoreVector2I>(8);
        for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
                if (dx != 0 || dy != 0)
                    positions.Add(new CoreVector2I(X + dx, Y + dy));
        return positions;
    }

    public IEnumerable<CoreVector2I> GetLineTo(CoreVector2I target)
    {
        var dx = Math.Abs(target.X - X);
        var dy = Math.Abs(target.Y - Y);
        var sx = X < target.X ? 1 : -1;
        var sy = Y < target.Y ? 1 : -1;
        var err = dx - dy;
        var currentX = X;
        var currentY = Y;
        while (true)
        {
            yield return new CoreVector2I(currentX, currentY);
            if (currentX == target.X && currentY == target.Y) yield break;
            var e2 = 2 * err;
            if (e2 > -dy) { err -= dy; currentX += sx; }
            if (e2 < dx) { err += dx; currentY += sy; }
        }
    }

    public IEnumerable<CoreVector2I> GetPositionsInRadius(int radius, bool includeCenter = false)
    {
        for (var x = X - radius; x <= X + radius; x++)
            for (var y = Y - radius; y <= Y + radius; y++)
                if ((includeCenter || x != X || y != Y)
                    && Math.Max(Math.Abs(x - X), Math.Abs(y - Y)) <= radius)
                    yield return new CoreVector2I(x, y);
    }

    public IEnumerable<CoreVector2I> GetPositionsInRectangle(CoreVector2I size, bool includeCenter = false)
    {
        var halfW = size.X / 2;
        var halfH = size.Y / 2;
        for (var x = X - halfW; x <= X + halfW; x++)
            for (var y = Y - halfH; y <= Y + halfH; y++)
                if (includeCenter || x != X || y != Y)
                    yield return new CoreVector2I(x, y);
    }
}
