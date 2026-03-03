namespace MoonBark.Core.Types;

/// <summary>
/// 2D rectangle struct for POCS implementations (float coordinates)
/// Engine-agnostic rectangle type for cross-platform compatibility.
/// </summary>
public readonly struct CoreRect2 : IEquatable<CoreRect2>
{
    /// <summary>
    /// Gets the rectangle position (top-left corner).
    /// </summary>
    public CoreVector2 Position { get; }

    /// <summary>
    /// Gets the rectangle size.
    /// </summary>
    public CoreVector2 Size { get; }
    
    /// <summary>
    /// Gets the X coordinate of the rectangle position.
    /// </summary>
    public float X => Position.X;

    /// <summary>
    /// Gets the Y coordinate of the rectangle position.
    /// </summary>
    public float Y => Position.Y;

    /// <summary>
    /// Gets the rectangle width.
    /// </summary>
    public float Width => Size.X;

    /// <summary>
    /// Gets the rectangle height.
    /// </summary>
    public float Height => Size.Y;
    
    /// <summary>
    /// Gets the end position (Position + Size).
    /// </summary>
    public CoreVector2 End => new(Position.X + Size.X, Position.Y + Size.Y);
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CoreRect2"/> struct.
    /// </summary>
    /// <param name="position">The rectangle position (top-left).</param>
    /// <param name="size">The rectangle size.</param>
    public CoreRect2(CoreVector2 position, CoreVector2 size)
    {
        Position = position;
        Size = size;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CoreRect2"/> struct.
    /// </summary>
    /// <param name="x">The X coordinate of the position.</param>
    /// <param name="y">The Y coordinate of the position.</param>
    /// <param name="width">The rectangle width.</param>
    /// <param name="height">The rectangle height.</param>
    public CoreRect2(float x, float y, float width, float height)
    {
        Position = new CoreVector2(x, y);
        Size = new CoreVector2(width, height);
    }
    
    /// <summary>
    /// Returns whether the rectangle contains the given point.
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns><c>true</c> if contained; otherwise <c>false</c>.</returns>
    public bool Contains(CoreVector2 point) =>
        point.X >= Position.X && point.X < End.X &&
        point.Y >= Position.Y && point.Y < End.Y;
    
    /// <summary>Alias for Contains - matches Godot's API</summary>
    public bool HasPoint(CoreVector2 point) => Contains(point);
        
    /// <summary>
    /// Returns whether this rectangle intersects another rectangle.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns><c>true</c> if they intersect; otherwise <c>false</c>.</returns>
    public bool Intersects(CoreRect2 other) =>
        Position.X < other.End.X && End.X > other.Position.X &&
        Position.Y < other.End.Y && End.Y > other.Position.Y;
    
    /// <summary>
    /// Determines whether two rectangles are equal.
    /// </summary>
    public static bool operator ==(CoreRect2 a, CoreRect2 b) => a.Position == b.Position && a.Size == b.Size;

    /// <summary>
    /// Determines whether two rectangles are not equal.
    /// </summary>
    public static bool operator !=(CoreRect2 a, CoreRect2 b) => !(a == b);
    
    /// <inheritdoc />
    public bool Equals(CoreRect2 other) => this == other;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is CoreRect2 other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Position, Size);

    /// <inheritdoc />
    public override string ToString() => $"CoreRect2({Position}, {Size})";
}
