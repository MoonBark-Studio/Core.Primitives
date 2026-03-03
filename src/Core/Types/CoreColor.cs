using System;

namespace MoonBark.Core.Types;

/// <summary>
/// Enhanced color type with additional utilities for MoonBark ecosystem.
/// Provides consistent color representation across Godot and Unity projects.
/// </summary>
public readonly struct CoreColor : IEquatable<CoreColor>
{
    public readonly float R;
    public readonly float G;
    public readonly float B;
    public readonly float A;

    public CoreColor(float r, float g, float b, float a = 1.0f)
    {
        R = Math.Clamp(r, 0f, 1f);
        G = Math.Clamp(g, 0f, 1f);
        B = Math.Clamp(b, 0f, 1f);
        A = Math.Clamp(a, 0f, 1f);
    }

    public static readonly CoreColor White = new(1f, 1f, 1f);
    public static readonly CoreColor Black = new(0f, 0f, 0f);
    public static readonly CoreColor Red = new(1f, 0f, 0f);
    public static readonly CoreColor Green = new(0f, 1f, 0f);
    public static readonly CoreColor Blue = new(0f, 0f, 1f);
    public static readonly CoreColor Yellow = new(1f, 1f, 0f);
    public static readonly CoreColor Transparent = new(0f, 0f, 0f, 0f);

    public bool Equals(CoreColor other) => 
        Math.Abs(R - other.R) < 1e-6f &&
        Math.Abs(G - other.G) < 1e-6f &&
        Math.Abs(B - other.B) < 1e-6f &&
        Math.Abs(A - other.A) < 1e-6f;

    public override bool Equals(object? obj) => obj is CoreColor other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(R, G, B, A);

    public static bool operator ==(CoreColor left, CoreColor right) => left.Equals(right);
    public static bool operator !=(CoreColor left, CoreColor right) => !left.Equals(right);

    public override string ToString() => $"CoreColor({R:F3}, {G:F3}, {B:F3}, {A:F3})";
}

/// <summary>
/// Static factory class for common CoreColor instances.
/// </summary>
public static class CoreColors
{
    public static CoreColor White => CoreColor.White;
    public static CoreColor Black => CoreColor.Black;
    public static CoreColor Red => CoreColor.Red;
    public static CoreColor Green => CoreColor.Green;
    public static CoreColor Blue => CoreColor.Blue;
    public static CoreColor Yellow => CoreColor.Yellow;
    public static CoreColor Transparent => CoreColor.Transparent;
}
