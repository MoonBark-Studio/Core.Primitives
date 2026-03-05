namespace MoonBark.Core.Types;

using System;

/// <summary>
/// Core transform type for spatial transformations.
/// Engine-agnostic representation of a transform (position, rotation, scale) for cross-platform compatibility.
/// </summary>
public readonly struct CoreTransform : IEquatable<CoreTransform>
{
    public readonly CoreVector3 Position;
    /// <summary>
    /// Rotation in Euler angles (degrees).
    /// </summary>
    public readonly CoreVector3 Rotation;
    public readonly CoreVector3 Scale;

    public static readonly CoreTransform Identity = new(CoreVector3.Zero, CoreVector3.Zero, CoreVector3.One);

    public CoreTransform(CoreVector3 position, CoreVector3 rotation, CoreVector3 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    public bool Equals(CoreTransform other) =>
        Position.Equals(other.Position) && 
        Rotation.Equals(other.Rotation) && 
        Scale.Equals(other.Scale);

    public override bool Equals(object? obj) => obj is CoreTransform other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Position, Rotation, Scale);

    public static bool operator ==(CoreTransform left, CoreTransform right) => left.Equals(right);
    public static bool operator !=(CoreTransform left, CoreTransform right) => !left.Equals(right);

    public override string ToString() => $"CoreTransform(Pos:{Position}, Rot:{Rotation}, Scale:{Scale})";
}
