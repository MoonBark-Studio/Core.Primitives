using MoonBark.Core.Types;
using Xunit;

namespace MoonBark.Core.Primitives.Tests;

public class CoreVector2Tests
{
    [Fact]
    public void GetDirectionTo_SamePosition_ReturnsZero()
    {
        var p = new CoreVector2(3f, 4f);
        Assert.Equal(CoreVector2.Zero, p.GetDirectionTo(p));
    }

    [Fact]
    public void GetDirectionTo_East_ReturnsNormalizedRight()
    {
        var from = new CoreVector2(0f, 0f);
        var dir = from.GetDirectionTo(new CoreVector2(5f, 0f));
        Assert.Equal(1f, dir.X, 5);
        Assert.Equal(0f, dir.Y, 5);
    }

    [Fact]
    public void GetDirectionTo_Diagonal_ReturnsNormalized()
    {
        var from = new CoreVector2(0f, 0f);
        var dir = from.GetDirectionTo(new CoreVector2(3f, 4f));
        Assert.Equal(0.6f, dir.X, 1);
        Assert.Equal(0.8f, dir.Y, 1);
    }

    [Fact]
    public void MoveInDirection_OneStep_ReturnsMoved()
    {
        var p = new CoreVector2(1f, 1f);
        Assert.Equal(new CoreVector2(2f, 2f), p.MoveInDirection(CoreVector2.One));
    }

    [Fact]
    public void MoveInDirection_TwoSteps_ReturnsDoubled()
    {
        var p = new CoreVector2(1f, 1f);
        Assert.Equal(new CoreVector2(3f, 1f), p.MoveInDirection(CoreVector2.Right, 2f));
    }

    [Fact]
    public void Clamp_WithinBounds_ReturnsOriginal()
    {
        var p = new CoreVector2(5f, 5f);
        var min = new CoreVector2(0f, 0f);
        var max = new CoreVector2(10f, 10f);
        Assert.Equal(p, p.Clamp(min, max));
    }

    [Fact]
    public void Clamp_OutsideBounds_ClampsToBounds()
    {
        var p = new CoreVector2(-1f, 15f);
        var min = new CoreVector2(0f, 0f);
        var max = new CoreVector2(10f, 10f);
        Assert.Equal(new CoreVector2(0f, 10f), p.Clamp(min, max));
    }
}