using System;
using MoonBark.Core.Types;
using Xunit;

namespace MoonBark.Core.Primitives.Tests.Compatibility;

/// <summary>
/// Tests cross-engine compatibility and mathematical operations
/// </summary>
public class CrossEngineCompatibilityTests
{
    [Fact]
    public void CoreVector2_MathOperations_ShouldBeConsistent()
    {
        // Arrange
        var v1 = new CoreVector2(1.0f, 2.0f);
        var v2 = new CoreVector2(3.0f, 4.0f);
        
        // Act & Assert - Addition
        var add = v1 + v2;
        Assert.Equal(4.0f, add.X);
        Assert.Equal(6.0f, add.Y);
        
        // Act & Assert - Subtraction
        var sub = v2 - v1;
        Assert.Equal(2.0f, sub.X);
        Assert.Equal(2.0f, sub.Y);
        
        // Act & Assert - Scalar multiplication
        var mul = v1 * 2.0f;
        Assert.Equal(2.0f, mul.X);
        Assert.Equal(4.0f, mul.Y);
        
        // Act & Assert - Dot product
        var dot = v1.Dot(v2);
        Assert.Equal(1.0f * 3.0f + 2.0f * 4.0f, dot);
        
        // Act & Assert - Length
        var length = v1.Length;
        Assert.Equal(MathF.Sqrt(1.0f * 1.0f + 2.0f * 2.0f), length);
        
        // Act & Assert - Normalization
        var normalized = v1.Normalized();
        Assert.Equal(1.0f, normalized.Length, 0.001f);
    }

    [Fact]
    public void CoreVector3_MathOperations_ShouldBeConsistent()
    {
        // Arrange
        var v1 = new CoreVector3(1.0f, 2.0f, 3.0f);
        var v2 = new CoreVector3(4.0f, 5.0f, 6.0f);
        
        // Act & Assert - Addition
        var add = v1 + v2;
        Assert.Equal(5.0f, add.X);
        Assert.Equal(7.0f, add.Y);
        Assert.Equal(9.0f, add.Z);
        
        // Act & Assert - Cross product
        var cross = v1.Cross(v2);
        var expectedCrossX = v1.Y * v2.Z - v1.Z * v2.Y;
        var expectedCrossY = v1.Z * v2.X - v1.X * v2.Z;
        var expectedCrossZ = v1.X * v2.Y - v1.Y * v2.X;
        Assert.Equal(expectedCrossX, cross.X);
        Assert.Equal(expectedCrossY, cross.Y);
        Assert.Equal(expectedCrossZ, cross.Z);
        
        // Act & Assert - Dot product
        var dot = v1.Dot(v2);
        Assert.Equal(1.0f * 4.0f + 2.0f * 5.0f + 3.0f * 6.0f, dot);
        
        // Act & Assert - Length
        var length = v1.Length;
        Assert.Equal(MathF.Sqrt(1.0f * 1.0f + 2.0f * 2.0f + 3.0f * 3.0f), length);
    }

    [Fact]
    public void CoreRect2_Contains_ShouldWorkCorrectly()
    {
        // Arrange
        var rect = new CoreRect2(0.0f, 0.0f, 10.0f, 10.0f);
        
        // Act & Assert - Point inside
        Assert.True(rect.Contains(new CoreVector2(5.0f, 5.0f)));
        Assert.True(rect.Contains(new CoreVector2(0.0f, 0.0f)));
        Assert.True(rect.Contains(new CoreVector2(9.9f, 9.9f)));
        
        // Act & Assert - Point outside
        Assert.False(rect.Contains(new CoreVector2(-1.0f, 5.0f)));
        Assert.False(rect.Contains(new CoreVector2(5.0f, -1.0f)));
        Assert.False(rect.Contains(new CoreVector2(10.0f, 5.0f)));
        Assert.False(rect.Contains(new CoreVector2(5.0f, 10.0f)));
    }

    [Fact]
    public void CoreRect2I_Contains_ShouldWorkCorrectly()
    {
        // Arrange
        var rect = new CoreRect2I(0, 0, 10, 10);
        
        // Act & Assert - Point inside
        Assert.True(rect.Contains(new CoreVector2I(5, 5)));
        Assert.True(rect.Contains(new CoreVector2I(0, 0)));
        Assert.True(rect.Contains(new CoreVector2I(9, 9)));
        
        // Act & Assert - Point outside
        Assert.False(rect.Contains(new CoreVector2I(-1, 5)));
        Assert.False(rect.Contains(new CoreVector2I(5, -1)));
        Assert.False(rect.Contains(new CoreVector2I(10, 5)));
        Assert.False(rect.Contains(new CoreVector2I(5, 10)));
    }

    [Fact]
    public void CoreRect2_Intersects_ShouldWorkCorrectly()
    {
        // Arrange
        var rect1 = new CoreRect2(0.0f, 0.0f, 10.0f, 10.0f);
        
        // Act & Assert - Intersecting rectangles
        var intersecting1 = new CoreRect2(5.0f, 5.0f, 10.0f, 10.0f);
        Assert.True(rect1.Intersects(intersecting1));
        
        var intersecting2 = new CoreRect2(-5.0f, -5.0f, 10.0f, 10.0f);
        Assert.True(rect1.Intersects(intersecting2));
        
        // Act & Assert - Non-intersecting rectangles
        var nonIntersecting1 = new CoreRect2(15.0f, 15.0f, 10.0f, 10.0f);
        Assert.False(rect1.Intersects(nonIntersecting1));
        
        var nonIntersecting2 = new CoreRect2(-15.0f, -15.0f, 10.0f, 10.0f);
        Assert.False(rect1.Intersects(nonIntersecting2));
    }

    [Fact]
    public void CoreColor_Clamping_ShouldWorkCorrectly()
    {
        // Act & Assert - Values within range
        var validColor = new CoreColor(0.5f, 0.5f, 0.5f, 0.5f);
        Assert.Equal(0.5f, validColor.R);
        Assert.Equal(0.5f, validColor.G);
        Assert.Equal(0.5f, validColor.B);
        Assert.Equal(0.5f, validColor.A);
        
        // Act & Assert - Values above range should be clamped
        var highColor = new CoreColor(1.5f, 1.5f, 1.5f, 1.5f);
        Assert.Equal(1.0f, highColor.R);
        Assert.Equal(1.0f, highColor.G);
        Assert.Equal(1.0f, highColor.B);
        Assert.Equal(1.0f, highColor.A);
        
        // Act & Assert - Values below range should be clamped
        var lowColor = new CoreColor(-0.5f, -0.5f, -0.5f, -0.5f);
        Assert.Equal(0.0f, lowColor.R);
        Assert.Equal(0.0f, lowColor.G);
        Assert.Equal(0.0f, lowColor.B);
        Assert.Equal(0.0f, lowColor.A);
    }

    [Fact]
    public void CoreTransform_Identity_ShouldBeCorrect()
    {
        // Arrange & Act
        var identity = CoreTransform.Identity;
        
        // Assert
        Assert.Equal(CoreVector3.Zero, identity.Position);
        Assert.Equal(CoreVector3.Zero, identity.Rotation);
        Assert.Equal(CoreVector3.One, identity.Scale);
    }

    [Fact]
    public void CoreVector2I_ToFloat_ShouldConvertCorrectly()
    {
        // Arrange
        var intVector = new CoreVector2I(10, 20);
        
        // Act
        var floatVector = intVector.ToFloat();
        
        // Assert
        Assert.Equal(10.0f, floatVector.X);
        Assert.Equal(20.0f, floatVector.Y);
    }

    [Fact]
    public void CoreVector2I_Distance_ShouldCalculateCorrectly()
    {
        // Arrange
        var v1 = new CoreVector2I(0, 0);
        var v2 = new CoreVector2I(3, 4);
        
        // Act
        var distance = v1.Distance(v2);
        var distanceSquared = v1.DistanceSquared(v2);
        
        // Assert
        Assert.Equal(5.0f, distance, 0.001f); // 3-4-5 triangle
        Assert.Equal(25, distanceSquared); // 3^2 + 4^2 = 25
    }

    [Fact]
    public void CoreColors_Factory_ShouldProvideStandardColors()
    {
        // Act & Assert
        Assert.Equal(1.0f, CoreColors.White.R);
        Assert.Equal(1.0f, CoreColors.White.G);
        Assert.Equal(1.0f, CoreColors.White.B);
        Assert.Equal(1.0f, CoreColors.White.A);
        
        Assert.Equal(0.0f, CoreColors.Black.R);
        Assert.Equal(0.0f, CoreColors.Black.G);
        Assert.Equal(0.0f, CoreColors.Black.B);
        Assert.Equal(1.0f, CoreColors.Black.A);
        
        Assert.Equal(1.0f, CoreColors.Red.R);
        Assert.Equal(0.0f, CoreColors.Red.G);
        Assert.Equal(0.0f, CoreColors.Red.B);
        Assert.Equal(1.0f, CoreColors.Red.A);
        
        Assert.Equal(0.0f, CoreColors.Transparent.A);
    }
}
