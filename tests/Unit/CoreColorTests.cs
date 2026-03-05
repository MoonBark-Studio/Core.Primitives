using System;
using MoonBark.Core.Types;
using Xunit;

namespace MoonBark.Core.Primitives.Tests.Unit;

public class CoreColorTests
{
    [Fact]
    public void Constructor_ShouldClampValues()
    {
        // Act
        var color = new CoreColor(1.5f, -0.5f, 0.5f, 2.0f);
        
        // Assert
        Assert.Equal(1.0f, color.R);
        Assert.Equal(0.0f, color.G);
        Assert.Equal(0.5f, color.B);
        Assert.Equal(1.0f, color.A);
    }

    [Fact]
    public void Equality_ShouldWorkWithinTolerance()
    {
        // Arrange
        var color1 = new CoreColor(0.1f, 0.2f, 0.3f, 0.4f);
        var color2 = new CoreColor(0.1000001f, 0.1999999f, 0.3f, 0.4f);
        
        // Assert
        Assert.Equal(color1, color2);
        Assert.True(color1 == color2);
    }

    [Fact]
    public void Inequality_ShouldWork()
    {
        // Arrange
        var color1 = new CoreColor(1f, 0f, 0f);
        var color2 = new CoreColor(0f, 1f, 0f);
        
        // Assert
        Assert.NotEqual(color1, color2);
        Assert.True(color1 != color2);
    }

    [Fact]
    public void StaticColors_ShouldBeCorrect()
    {
        Assert.Equal(new CoreColor(1f, 1f, 1f), CoreColor.White);
        Assert.Equal(new CoreColor(0f, 0f, 0f), CoreColor.Black);
        Assert.Equal(new CoreColor(1f, 0f, 0f), CoreColor.Red);
        Assert.Equal(new CoreColor(0f, 1f, 0f), CoreColor.Green);
        Assert.Equal(new CoreColor(0f, 0f, 1f), CoreColor.Blue);
    }
}
