using System;
using MoonBark.Core.Types;
using Xunit;

namespace MoonBark.Core.Primitives.Tests.Compatibility;

/// <summary>
/// Tests compatibility patterns for Unity engine integration
/// </summary>
public class UnityPatternTests
{
    [Fact]
    public void UnityVector2_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Unity
        // Actual Unity types would be: UnityEngine.Vector2
        
        // Arrange - Simulate Unity Vector2
        var unityX = 1.5f;
        var unityY = 2.5f;
        
        // Act - Convert from Unity pattern
        var coreVector = new CoreVector2(unityX, unityY);
        
        // Assert - Verify conversion
        Assert.Equal(1.5f, coreVector.X);
        Assert.Equal(2.5f, coreVector.Y);
        
        // Act - Convert to Unity pattern
        var convertedX = coreVector.X;
        var convertedY = coreVector.Y;
        
        // Assert - Verify round-trip
        Assert.Equal(unityX, convertedX);
        Assert.Equal(unityY, convertedY);
    }

    [Fact]
    public void UnityVector3_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Unity
        // Actual Unity types would be: UnityEngine.Vector3
        
        // Arrange - Simulate Unity Vector3
        var unityX = 1.0f;
        var unityY = 2.0f;
        var unityZ = 3.0f;
        
        // Act - Convert from Unity pattern
        var coreVector = new CoreVector3(unityX, unityY, unityZ);
        
        // Assert - Verify conversion
        Assert.Equal(1.0f, coreVector.X);
        Assert.Equal(2.0f, coreVector.Y);
        Assert.Equal(3.0f, coreVector.Z);
        
        // Act - Convert to Unity pattern
        var convertedX = coreVector.X;
        var convertedY = coreVector.Y;
        var convertedZ = coreVector.Z;
        
        // Assert - Verify round-trip
        Assert.Equal(unityX, convertedX);
        Assert.Equal(unityY, convertedY);
        Assert.Equal(unityZ, convertedZ);
    }

    [Fact]
    public void UnityColor_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Unity
        // Actual Unity types would be: UnityEngine.Color
        
        // Arrange - Simulate Unity Color (RGBA 0-1 range)
        var unityR = 0.8f;
        var unityG = 0.6f;
        var unityB = 0.4f;
        var unityA = 1.0f;
        
        // Act - Convert from Unity pattern
        var coreColor = new CoreColor(unityR, unityG, unityB, unityA);
        
        // Assert - Verify conversion
        Assert.Equal(0.8f, coreColor.R);
        Assert.Equal(0.6f, coreColor.G);
        Assert.Equal(0.4f, coreColor.B);
        Assert.Equal(1.0f, coreColor.A);
        
        // Act - Convert to Unity pattern
        var convertedR = coreColor.R;
        var convertedG = coreColor.G;
        var convertedB = coreColor.B;
        var convertedA = coreColor.A;
        
        // Assert - Verify round-trip
        Assert.Equal(unityR, convertedR);
        Assert.Equal(unityG, convertedG);
        Assert.Equal(unityB, convertedB);
        Assert.Equal(unityA, convertedA);
    }

    [Fact]
    public void UnityTransform_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Unity
        // Actual Unity types would be: UnityEngine.Transform, UnityEngine.Vector3, UnityEngine.Quaternion
        
        // Arrange - Simulate Unity Transform components
        var positionX = 1.0f;
        var positionY = 2.0f;
        var positionZ = 3.0f;
        var rotationX = 0.0f;  // Unity uses Quaternion, but we'll simulate Euler angles
        var rotationY = 90.0f;
        var rotationZ = 0.0f;
        var scaleX = 1.0f;
        var scaleY = 1.0f;
        var scaleZ = 1.0f;
        
        // Act - Convert from Unity pattern
        var coreTransform = new CoreTransform(
            new CoreVector3(positionX, positionY, positionZ),
            new CoreVector3(rotationX, rotationY, rotationZ),
            new CoreVector3(scaleX, scaleY, scaleZ)
        );
        
        // Assert - Verify conversion
        Assert.Equal(1.0f, coreTransform.Position.X);
        Assert.Equal(2.0f, coreTransform.Position.Y);
        Assert.Equal(3.0f, coreTransform.Position.Z);
        Assert.Equal(0.0f, coreTransform.Rotation.X);
        Assert.Equal(90.0f, coreTransform.Rotation.Y);
        Assert.Equal(0.0f, coreTransform.Rotation.Z);
        Assert.Equal(1.0f, coreTransform.Scale.X);
        Assert.Equal(1.0f, coreTransform.Scale.Y);
        Assert.Equal(1.0f, coreTransform.Scale.Z);
        
        // Act - Convert to Unity pattern
        var convertedPosX = coreTransform.Position.X;
        var convertedPosY = coreTransform.Position.Y;
        var convertedPosZ = coreTransform.Position.Z;
        var convertedRotX = coreTransform.Rotation.X;
        var convertedRotY = coreTransform.Rotation.Y;
        var convertedRotZ = coreTransform.Rotation.Z;
        var convertedScaleX = coreTransform.Scale.X;
        var convertedScaleY = coreTransform.Scale.Y;
        var convertedScaleZ = coreTransform.Scale.Z;
        
        // Assert - Verify round-trip
        Assert.Equal(positionX, convertedPosX);
        Assert.Equal(positionY, convertedPosY);
        Assert.Equal(positionZ, convertedPosZ);
        Assert.Equal(rotationX, convertedRotX);
        Assert.Equal(rotationY, convertedRotY);
        Assert.Equal(rotationZ, convertedRotZ);
        Assert.Equal(scaleX, convertedScaleX);
        Assert.Equal(scaleY, convertedScaleY);
        Assert.Equal(scaleZ, convertedScaleZ);
    }

    [Fact]
    public void UnityRect_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Unity
        // Actual Unity types would be: UnityEngine.Rect
        
        // Arrange - Simulate Unity Rect
        var unityX = 10.0f;
        var unityY = 20.0f;
        var unityWidth = 100.0f;
        var unityHeight = 50.0f;
        
        // Act - Convert from Unity pattern
        var coreRect = new CoreRect2(unityX, unityY, unityWidth, unityHeight);
        
        // Assert - Verify conversion
        Assert.Equal(10.0f, coreRect.X);
        Assert.Equal(20.0f, coreRect.Y);
        Assert.Equal(100.0f, coreRect.Width);
        Assert.Equal(50.0f, coreRect.Height);
        
        // Act - Convert to Unity pattern
        var convertedX = coreRect.X;
        var convertedY = coreRect.Y;
        var convertedWidth = coreRect.Width;
        var convertedHeight = coreRect.Height;
        
        // Assert - Verify round-trip
        Assert.Equal(unityX, convertedX);
        Assert.Equal(unityY, convertedY);
        Assert.Equal(unityWidth, convertedWidth);
        Assert.Equal(unityHeight, convertedHeight);
    }

    [Fact]
    public void UnityVector2Int_ConversionPattern_ShouldWork()
    {
        // This test demonstrates the expected conversion pattern for Unity
        // Actual Unity types would be: UnityEngine.Vector2Int
        
        // Arrange - Simulate Unity Vector2Int
        var unityX = 10;
        var unityY = 20;
        
        // Act - Convert from Unity pattern
        var coreVector = new CoreVector2I(unityX, unityY);
        
        // Assert - Verify conversion
        Assert.Equal(10, coreVector.X);
        Assert.Equal(20, coreVector.Y);
        
        // Act - Convert to Unity pattern
        var convertedX = coreVector.X;
        var convertedY = coreVector.Y;
        
        // Assert - Verify round-trip
        Assert.Equal(unityX, convertedX);
        Assert.Equal(unityY, convertedY);
    }

    [Fact]
    public void UnityMathOperations_ShouldBeConsistent()
    {
        // Test that CoreVector2 math operations match Unity's expected behavior
        
        // Arrange
        var v1 = new CoreVector2(1.0f, 2.0f);
        var v2 = new CoreVector2(3.0f, 4.0f);
        
        // Act & Assert - Addition (Unity: v1 + v2)
        var add = v1 + v2;
        Assert.Equal(4.0f, add.X);  // 1 + 3
        Assert.Equal(6.0f, add.Y);  // 2 + 4
        
        // Act & Assert - Subtraction (Unity: v1 - v2)
        var sub = v2 - v1;
        Assert.Equal(2.0f, sub.X);  // 3 - 1
        Assert.Equal(2.0f, sub.Y);  // 4 - 2
        
        // Act & Assert - Scalar multiplication (Unity: v1 * scalar)
        var mul = v1 * 2.0f;
        Assert.Equal(2.0f, mul.X);  // 1 * 2
        Assert.Equal(4.0f, mul.Y);  // 2 * 2
        
        // Act & Assert - Dot product (Unity: Vector3.Dot(v1, v2))
        var dot = v1.Dot(v2);
        Assert.Equal(1.0f * 3.0f + 2.0f * 4.0f, dot);  // 1*3 + 2*4 = 11
        
        // Act & Assert - Magnitude (Unity: v1.magnitude)
        var magnitude = v1.Length;
        Assert.Equal(MathF.Sqrt(1.0f * 1.0f + 2.0f * 2.0f), magnitude);  // sqrt(1 + 4) = sqrt(5)
        
        // Act & Assert - Normalized (Unity: v1.normalized)
        var normalized = v1.Normalized();
        Assert.Equal(1.0f, normalized.Length, 0.001f);  // Should be unit length
    }
}
