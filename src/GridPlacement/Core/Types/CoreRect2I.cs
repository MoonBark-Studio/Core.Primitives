namespace MoonBark.Core.Types;

/// <summary>
/// Integer-based 2D rectangle for area operations
/// Engine-agnostic equivalent of Godot's Rect2I for cross-platform compatibility.
/// </summary>
public readonly struct CoreRect2I : IEquatable<CoreRect2I>
{
    /// <summary>
    /// Gets the rectangle position (top-left corner).
    /// </summary>
    public readonly CoreVector2I Position;

    /// <summary>
    /// Gets the rectangle size.
    /// </summary>
    public readonly CoreVector2I Size;

    /// <summary>
    /// Initializes a new instance of the <see cref="CoreRect2I"/> struct.
    /// </summary>
    /// <param name="position">The rectangle position (top-left).</param>
    /// <param name="size">The rectangle size.</param>
    public CoreRect2I(CoreVector2I position, CoreVector2I size)
    {
        Position = position;
        Size = size;
    }

    /// <summary>
    /// Creates a CoreRect2I from individual x, y, width, height values.
    /// </summary>
    public CoreRect2I(int x, int y, int width, int height)
    {
        Position = new CoreVector2I(x, y);
        Size = new CoreVector2I(width, height);
    }

    /// <summary>
    /// Gets an empty rectangle at the origin.
    /// </summary>
    public static CoreRect2I Zero => new(CoreVector2I.Zero, CoreVector2I.Zero);
        
    /// <summary>
    /// Gets the minimum corner of the rectangle.
    /// </summary>
    public CoreVector2I Min => Position;

    /// <summary>
    /// Gets the maximum corner of the rectangle (Position + Size).
    /// </summary>
    public CoreVector2I Max => Position + Size;
        
    /// <summary>
    /// Returns whether the rectangle contains the given point.
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns><c>true</c> if contained; otherwise <c>false</c>.</returns>
    public bool Contains(CoreVector2I point) => 
        point.X >= Position.X && point.X < Position.X + Size.X &&
        point.Y >= Position.Y && point.Y < Position.Y + Size.Y;

    /// <inheritdoc />
    public bool Equals(CoreRect2I other) => Position.Equals(other.Position) && Size.Equals(other.Size);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is CoreRect2I other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Position, Size);

    /// <inheritdoc />
    public override string ToString() => $"CoreRect2I({Position}, {Size})";
}
