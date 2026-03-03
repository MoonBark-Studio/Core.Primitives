using System;
using MoonBark.Core.Types;
using Xunit;

namespace MoonBark.Core.Primitives.Tests.Compatibility;

/// <summary>
/// Tests compatibility patterns for Godot engine integration
/// </summary>
public class GodotPatternTests
{
    [Fact]
    public void GodotVector2_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Godot
        // Actual Godot types would be: Godot.Vector2
        
        // Arrange - Simulate Godot Vector2
        var godotX = 1.5f;
        var godotY = 2.5f;
        
        // Act - Convert from Godot pattern
        var coreVector = new CoreVector2(godotX, godotY);
        
        // Assert - Verify conversion
        Assert.Equal(1.5f, coreVector.X);
        Assert.Equal(2.5f, coreVector.Y);
        
        // Act - Convert to Godot pattern
        var convertedX = coreVector.X;
        var convertedY = coreVector.Y;
        
        // Assert - Verify round-trip
        Assert.Equal(godotX, convertedX);
        Assert.Equal(godotY, convertedY);
    }

    [Fact]
    public void GodotVector3_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Godot
        // Actual Godot types would be: Godot.Vector3
        
        // Arrange - Simulate Godot Vector3
        var godotX = 1.0f;
        var godotY = 2.0f;
        var godotZ = 3.0f;
        
        // Act - Convert from Godot pattern
        var coreVector = new CoreVector3(godotX, godotY, godotZ);
        
        // Assert - Verify conversion
        Assert.Equal(1.0f, coreVector.X);
        Assert.Equal(2.0f, coreVector.Y);
        Assert.Equal(3.0f, coreVector.Z);
        
        // Act - Convert to Godot pattern
        var convertedX = coreVector.X;
        var convertedY = coreVector.Y;
        var convertedZ = coreVector.Z;
        
        // Assert - Verify round-trip
        Assert.Equal(godotX, convertedX);
        Assert.Equal(godotY, convertedY);
        Assert.Equal(godotZ, convertedZ);
    }

    [Fact]
    public void GodotColor_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Godot
        // Actual Godot types would be: Godot.Color
        
        // Arrange - Simulate Godot Color (RGBA 0-1 range)
        var godotR = 0.8f;
        var godotG = 0.6f;
        var godotB = 0.4f;
        var godotA = 1.0f;
        
        // Act - Convert from Godot pattern
        var coreColor = new CoreColor(godotR, godotG, godotB, godotA);
        
        // Assert - Verify conversion
        Assert.Equal(0.8f, coreColor.R);
        Assert.Equal(0.6f, coreColor.G);
        Assert.Equal(0.4f, coreColor.B);
        Assert.Equal(1.0f, coreColor.A);
        
        // Act - Convert to Godot pattern
        var convertedR = coreColor.R;
        var convertedG = coreColor.G;
        var convertedB = coreColor.B;
        var convertedA = coreColor.A;
        
        // Assert - Verify round-trip
        Assert.Equal(godotR, convertedR);
        Assert.Equal(godotG, convertedG);
        Assert.Equal(godotB, convertedB);
        Assert.Equal(godotA, convertedA);
    }

    [Fact]
    public void GodotTransform3D_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Godot
        // Actual Godot types would be: Godot.Transform3D, Godot.Vector3, Godot.Basis
        
        // Arrange - Simulate Godot Transform3D components
        var originX = 1.0f;
        var originY = 2.0f;
        var originZ = 3.0f;
        
        // Act - Convert from Godot pattern (simplified - Godot Transform3D is complex)
        var coreTransform = new CoreTransform(
            new CoreVector3(originX, originY, originZ),
            CoreVector3.Zero,  // Godot Transform3D uses Basis, not direct rotation
            CoreVector3.One   // Godot Transform3D uses Basis, not direct scale
        );
        
        // Assert - Verify conversion
        Assert.Equal(1.0f, coreTransform.Position.X);
        Assert.Equal(2.0f, coreTransform.Position.Y);
        Assert.Equal(3.0f, coreTransform.Position.Z);
        Assert.Equal(0.0f, coreTransform.Rotation.X);
        Assert.Equal(0.0f, coreTransform.Rotation.Y);
        Assert.Equal(0.0f, coreTransform.Rotation.Z);
        Assert.Equal(1.0f, coreTransform.Scale.X);
        Assert.Equal(1.0f, coreTransform.Scale.Y);
        Assert.Equal(1.0f, coreTransform.Scale.Z);
        
        // Act - Convert to Godot pattern
        var convertedOriginX = coreTransform.Position.X;
        var convertedOriginY = coreTransform.Position.Y;
        var convertedOriginZ = coreTransform.Position.Z;
        
        // Assert - Verify round-trip
        Assert.Equal(originX, convertedOriginX);
        Assert.Equal(originY, convertedOriginY);
        Assert.Equal(originZ, convertedOriginZ);
    }

    [Fact]
    public void GodotRect2_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Godot
        // Actual Godot types would be: Godot.Rect2
        
        // Arrange - Simulate Godot Rect2
        var godotX = 10.0f;
        var godotY = 20.0f;
        var godotWidth = 100.0f;
        var godotHeight = 50.0f;
        
        // Act - Convert from Godot pattern
        var coreRect = new CoreRect2(godotX, godotY, godotWidth, godotHeight);
        
        // Assert - Verify conversion
        Assert.Equal(10.0f, coreRect.X);
        Assert.Equal(20.0f, coreRect.Y);
        Assert.Equal(100.0f, coreRect.Width);
        Assert.Equal(50.0f, coreRect.Height);
        
        // Act - Convert to Godot pattern
        var convertedX = coreRect.X;
        var convertedY = coreRect.Y;
        var convertedWidth = coreRect.Width;
        var convertedHeight = coreRect.Height;
        
        // Assert - Verify round-trip
        Assert.Equal(godotX, convertedX);
        Assert.Equal(godotY, convertedY);
        Assert.Equal(godotWidth, convertedWidth);
        Assert.Equal(godotHeight, convertedHeight);
    }

    [Fact]
    public void GodotVector2I_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Godot
        // Actual Godot types would be: Godot.Vector2i
        
        // Arrange - Simulate Godot Vector2i
        var godotX = 10;
        var godotY = 20;
        
        // Act - Convert from Godot pattern
        var coreVector = new CoreVector2I(godotX, godotY);
        
        // Assert - Verify conversion
        Assert.Equal(10, coreVector.X);
        Assert.Equal(20, coreVector.Y);
        
        // Act - Convert to Godot pattern
        var convertedX = coreVector.X;
        var convertedY = coreVector.Y;
        
        // Assert - Verify round-trip
        Assert.Equal(godotX, convertedX);
        Assert.Equal(godotY, convertedY);
    }

    [Fact]
    public void GodotRect2I_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Godot
        // Actual Godot types would be: Godot.Rect2i
        
        // Arrange - Simulate Godot Rect2i
        var godotX = 5;
        var godotY = 10;
        var godotWidth = 30;
        var godotHeight = 40;
        
        // Act - Convert from Godot pattern
        var coreRect = new CoreRect2I(godotX, godotY, godotWidth, godotHeight);
        
        // Assert - Verify conversion
        Assert.Equal(5, coreRect.Position.X);
        Assert.Equal(10, coreRect.Position.Y);
        Assert.Equal(30, coreRect.Size.X);
        Assert.Equal(40, coreRect.Size.Y);
        
        // Act - Convert to Godot pattern
        var convertedX = coreRect.Position.X;
        var convertedY = coreRect.Position.Y;
        var convertedWidth = coreRect.Size.X;
        var convertedHeight = coreRect.Size.Y;
        
        // Assert - Verify round-trip
        Assert.Equal(godotX, convertedX);
        Assert.Equal(godotY, convertedY);
        Assert.Equal(godotWidth, convertedWidth);
        Assert.Equal(godotHeight, convertedHeight);
    }

    [Fact]
    public void GodotMathOperations_ShouldBeConsistent()
    {
        // Test that CoreVector2 math operations match Godot's expected behavior
        
        // Arrange
        var v1 = new CoreVector2(1.0f, 2.0f);
        var v2 = new CoreVector2(3.0f, 4.0f);
        
        // Act & Assert - Addition (Godot: v1 + v2)
        var add = v1 + v2;
        Assert.Equal(4.0f, add.X);  // 1 + 3
        Assert.Equal(6.0f, add.Y);  // 2 + 4
        
        // Act & Assert - Subtraction (Godot: v1 - v2)
        var sub = v2 - v1;
        Assert.Equal(2.0f, sub.X);  // 3 - 1
        Assert.Equal(2.0f, sub.Y);  // 4 - 2
        
        // Act & Assert - Scalar multiplication (Godot: v1 * scalar)
        var mul = v1 * 2.0f;
        Assert.Equal(2.0f, mul.X);  // 1 * 2
        Assert.Equal(4.0f, mul.Y);  // 2 * 2
        
        // Act & Assert - Dot product (Godot: v1.Dot(v2))
        var dot = v1.Dot(v2);
        Assert.Equal(1.0f * 3.0f + 2.0f * 4.0f, dot);  // 1*3 + 2*4 = 11
        
        // Act & Assert - Length (Godot: v1.Length())
        var length = v1.Length;
        Assert.Equal(MathF.Sqrt(1.0f * 1.0f + 2.0f * 2.0f), length);  // sqrt(1 + 4) = sqrt(5)
        
        // Act & Assert - Normalized (Godot: v1.Normalized())
        var normalized = v1.Normalized();
        Assert.Equal(1.0f, normalized.Length, 0.001f);  // Should be unit length
    }

    [Fact]
    public void GodotColorConstants_ShouldMatch()
    {
        // Test that CoreColors match Godot's color constants
        
        // Act & Assert - White (Godot: Colors.WHITE)
        Assert.Equal(1.0f, CoreColors.White.R);
        Assert.Equal(1.0f, CoreColors.White.G);
        Assert.Equal(1.0f, CoreColors.White.B);
        Assert.Equal(1.0f, CoreColors.White.A);
        
        // Act & Assert - Black (Godot: Colors.BLACK)
        Assert.Equal(0.0f, CoreColors.Black.R);
        Assert.Equal(0.0f, CoreColors.Black.G);
        Assert.Equal(0.0f, CoreColors.Black.B);
        Assert.Equal(1.0f, CoreColors.Black.A);
        
        // Act & Assert - Red (Godot: Colors.RED)
        Assert.Equal(1.0f, CoreColors.Red.R);
        Assert.Equal(0.0f, CoreColors.Red.G);
        Assert.Equal(0.0f, CoreColors.Red.B);
        Assert.Equal(1.0f, CoreColors.Red.A);
        
        // Act & Assert - Transparent (Godot: Colors.TRANSPARENT)
        Assert.Equal(0.0f, CoreColors.Transparent.A);
    }

    [Fact]
    public void GodotRectOperations_ShouldBeConsistent()
    {
        // Test that CoreRect2 operations match Godot's expected behavior
        
        // Arrange
        var rect = new CoreRect2(0.0f, 0.0f, 10.0f, 10.0f);
        
        // Act & Assert - Contains point (Godot: rect.HasPoint(point))
        Assert.True(rect.Contains(new CoreVector2(5.0f, 5.0f)));
        Assert.True(rect.Contains(new CoreVector2(0.0f, 0.0f)));
        Assert.True(rect.Contains(new CoreVector2(9.9f, 9.9f)));
        
        // Act & Assert - Does not contain point
        Assert.False(rect.Contains(new CoreVector2(-1.0f, 5.0f)));
        Assert.False(rect.Contains(new CoreVector2(5.0f, -1.0f)));
        Assert.False(rect.Contains(new CoreVector2(10.0f, 5.0f)));
        Assert.False(rect.Contains(new CoreVector2(5.0f, 10.0f)));
        
        // Act & Assert - Intersection (Godot: rect.Intersects(other))
        var intersecting = new CoreRect2(5.0f, 5.0f, 10.0f, 10.0f);
        Assert.True(rect.Intersects(intersecting));
        
        var nonIntersecting = new CoreRect2(15.0f, 15.0f, 10.0f, 10.0f);
        Assert.False(rect.Intersects(nonIntersecting));
    }
}
