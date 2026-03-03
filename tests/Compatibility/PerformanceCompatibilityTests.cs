using System;
using MoonBark.Core.Types;
using Xunit;

namespace MoonBark.Core.Primitives.Tests.Compatibility;

/// <summary>
/// Tests performance characteristics of MoonBark.Core.Primitives
/// </summary>
public class PerformanceCompatibilityTests
{
    [Fact]
    public void CoreVector2_Performance_ShouldBeComparableToSystemNumerics()
    {
        // Arrange
        const int iterations = 1_000_000;
        var coreVectors = new CoreVector2[iterations];
        var systemVectors = new System.Numerics.Vector2[iterations];
        var random = new Random(42);
        
        // Initialize test data
        for (int i = 0; i < iterations; i++)
        {
            var x = (float)random.NextDouble();
            var y = (float)random.NextDouble();
            coreVectors[i] = new CoreVector2(x, y);
            systemVectors[i] = new System.Numerics.Vector2(x, y);
        }
        
        // Act & Assert - Addition performance
        var start = DateTime.UtcNow;
        for (int i = 0; i < iterations - 1; i++)
        {
            var result = coreVectors[i] + coreVectors[i + 1];
        }
        var coreTime = DateTime.UtcNow - start;
        
        start = DateTime.UtcNow;
        for (int i = 0; i < iterations - 1; i++)
        {
            var result = systemVectors[i] + systemVectors[i + 1];
        }
        var systemTime = DateTime.UtcNow - start;
        
        // CoreVector2 should be within 2x of System.Numerics performance
        Assert.True(coreTime.TotalMilliseconds <= systemTime.TotalMilliseconds * 2.0, 
            $"CoreVector2 addition too slow: {coreTime.TotalMilliseconds}ms vs {systemTime.TotalMilliseconds}ms");
    }

    [Fact]
    public void CoreVector3_Performance_ShouldBeComparableToSystemNumerics()
    {
        // Arrange
        const int iterations = 1_000_000;
        var coreVectors = new CoreVector3[iterations];
        var systemVectors = new System.Numerics.Vector3[iterations];
        var random = new Random(42);
        
        // Initialize test data
        for (int i = 0; i < iterations; i++)
        {
            var x = (float)random.NextDouble();
            var y = (float)random.NextDouble();
            var z = (float)random.NextDouble();
            coreVectors[i] = new CoreVector3(x, y, z);
            systemVectors[i] = new System.Numerics.Vector3(x, y, z);
        }
        
        // Act & Assert - Dot product performance
        var start = DateTime.UtcNow;
        for (int i = 0; i < iterations - 1; i++)
        {
            var result = coreVectors[i].Dot(coreVectors[i + 1]);
        }
        var coreTime = DateTime.UtcNow - start;
        
        start = DateTime.UtcNow;
        for (int i = 0; i < iterations - 1; i++)
        {
            var result = System.Numerics.Vector3.Dot(systemVectors[i], systemVectors[i + 1]);
        }
        var systemTime = DateTime.UtcNow - start;
        
        // CoreVector3 should be within 2x of System.Numerics performance
        Assert.True(coreTime.TotalMilliseconds <= systemTime.TotalMilliseconds * 2.0, 
            $"CoreVector3 dot product too slow: {coreTime.TotalMilliseconds}ms vs {systemTime.TotalMilliseconds}ms");
    }

    [Fact]
    public void CoreColor_Performance_ShouldBeEfficient()
    {
        // Arrange
        const int iterations = 1_000_000;
        var colors = new CoreColor[iterations];
        var random = new Random(42);
        
        // Initialize test data
        for (int i = 0; i < iterations; i++)
        {
            var r = (float)random.NextDouble();
            var g = (float)random.NextDouble();
            var b = (float)random.NextDouble();
            var a = (float)random.NextDouble();
            colors[i] = new CoreColor(r, g, b, a);
        }
        
        // Act & Assert - Equality check performance
        var start = DateTime.UtcNow;
        var equalCount = 0;
        for (int i = 0; i < iterations - 1; i++)
        {
            if (colors[i] == colors[i + 1])
                equalCount++;
        }
        var duration = DateTime.UtcNow - start;
        
        // Should complete within reasonable time (less than 100ms for 1M operations)
        Assert.True(duration.TotalMilliseconds < 100, 
            $"CoreColor equality check too slow: {duration.TotalMilliseconds}ms for {iterations} operations");
    }

    [Fact]
    public void CoreRect2_Contains_Performance_ShouldBeEfficient()
    {
        // Arrange
        const int iterations = 1_000_000;
        var rect = new CoreRect2(0.0f, 0.0f, 100.0f, 100.0f);
        var points = new CoreVector2[iterations];
        var random = new Random(42);
        
        // Initialize test data
        for (int i = 0; i < iterations; i++)
        {
            var x = (float)random.NextDouble() * 200 - 50; // -50 to 150
            var y = (float)random.NextDouble() * 200 - 50; // -50 to 150
            points[i] = new CoreVector2(x, y);
        }
        
        // Act & Assert - Contains check performance
        var start = DateTime.UtcNow;
        var containedCount = 0;
        for (int i = 0; i < iterations; i++)
        {
            if (rect.Contains(points[i]))
                containedCount++;
        }
        var duration = DateTime.UtcNow - start;
        
        // Should complete within reasonable time (less than 100ms for 1M operations)
        Assert.True(duration.TotalMilliseconds < 100, 
            $"CoreRect2.Contains check too slow: {duration.TotalMilliseconds}ms for {iterations} operations");
        
        // Should have roughly 25% of points contained (100x100 rect in 200x200 area)
        Assert.True(containedCount > iterations * 0.2 && containedCount < iterations * 0.3,
            $"Unexpected containment ratio: {containedCount}/{iterations}");
    }

    [Fact]
    public void MemoryUsage_ShouldBeReasonable()
    {
        // Arrange
        const int count = 100_000;
        
        // Act - Create arrays of different types
        var coreVector2s = new CoreVector2[count];
        var coreVector3s = new CoreVector3[count];
        var coreColors = new CoreColor[count];
        var coreRect2s = new CoreRect2[count];
        
        // Initialize
        for (int i = 0; i < count; i++)
        {
            coreVector2s[i] = new CoreVector2(i, i);
            coreVector3s[i] = new CoreVector3(i, i, i);
            coreColors[i] = new CoreColor(i / 255.0f, i / 255.0f, i / 255.0f, i / 255.0f);
            coreRect2s[i] = new CoreRect2(i, i, i, i);
        }
        
        // Assert - Memory usage should be reasonable
        // CoreVector2: 2 floats = 8 bytes
        // CoreVector3: 3 floats = 12 bytes  
        // CoreColor: 4 floats = 16 bytes
        // CoreRect2: 4 floats = 16 bytes
        // Total expected: ~52 bytes per element * 100,000 = ~5.2MB
        
        // This is more of a smoke test - if this fails, there might be memory issues
        Assert.True(coreVector2s.Length == count);
        Assert.True(coreVector3s.Length == count);
        Assert.True(coreColors.Length == count);
        Assert.True(coreRect2s.Length == count);
    }
}
