using System.Collections.Generic;
using System.Linq;
using MoonBark.Core.Types;
using Xunit;

namespace MoonBark.Core.Primitives.Tests;

public class CoreVector2ITests
{
    // ManhattanDistanceTo
    [Fact]
    public void ManhattanDistanceTo_SamePosition_ReturnsZero()
    {
        var p = new CoreVector2I(3, 4);
        Assert.Equal(0, p.ManhattanDistanceTo(p));
    }

    [Fact]
    public void ManhattanDistanceTo_OrthogonalNeighbors_ReturnsOne()
    {
        var p = new CoreVector2I(0, 0);
        Assert.Equal(1, p.ManhattanDistanceTo(new CoreVector2I(1, 0)));
        Assert.Equal(1, p.ManhattanDistanceTo(new CoreVector2I(0, 1)));
        Assert.Equal(1, p.ManhattanDistanceTo(new CoreVector2I(-1, 0)));
        Assert.Equal(1, p.ManhattanDistanceTo(new CoreVector2I(0, -1)));
    }

    [Fact]
    public void ManhattanDistanceTo_Diagonal_ReturnsTwo()
    {
        var p = new CoreVector2I(0, 0);
        Assert.Equal(2, p.ManhattanDistanceTo(new CoreVector2I(1, 1)));
    }

    // ChebyshevDistanceTo
    [Fact]
    public void ChebyshevDistanceTo_Diagonal_ReturnsOne()
    {
        var p = new CoreVector2I(0, 0);
        Assert.Equal(1, p.ChebyshevDistanceTo(new CoreVector2I(1, 1)));
    }

    [Fact]
    public void ChebyshevDistanceTo_Orthogonal_ReturnsOne()
    {
        var p = new CoreVector2I(0, 0);
        Assert.Equal(1, p.ChebyshevDistanceTo(new CoreVector2I(1, 0)));
    }

    // GetDirectionTo
    [Fact]
    public void GetDirectionTo_East_ReturnsRight()
    {
        var from = new CoreVector2I(0, 0);
        Assert.Equal(CoreVector2I.Right, from.GetDirectionTo(new CoreVector2I(5, 0)));
    }

    [Fact]
    public void GetDirectionTo_NorthEast_ReturnsUpRight()
    {
        var from = new CoreVector2I(0, 0);
        Assert.Equal(new CoreVector2I(1, 1), from.GetDirectionTo(new CoreVector2I(5, 5)));
    }

    [Fact]
    public void GetDirectionTo_SamePosition_ReturnsZero()
    {
        var p = new CoreVector2I(3, 3);
        Assert.Equal(CoreVector2I.Zero, p.GetDirectionTo(p));
    }

    // MoveInDirection
    [Fact]
    public void MoveInDirection_OneStep_ReturnsMoved()
    {
        var p = new CoreVector2I(1, 1);
        Assert.Equal(new CoreVector2I(2, 2), p.MoveInDirection(CoreVector2I.One));
        Assert.Equal(new CoreVector2I(2, 1), p.MoveInDirection(CoreVector2I.Right));
        Assert.Equal(new CoreVector2I(1, 2), p.MoveInDirection(CoreVector2I.Up));
    }

    [Fact]
    public void MoveInDirection_TwoSteps_ReturnsDoubled()
    {
        var p = new CoreVector2I(1, 1);
        Assert.Equal(new CoreVector2I(3, 3), p.MoveInDirection(CoreVector2I.One, 2));
        Assert.Equal(new CoreVector2I(3, 1), p.MoveInDirection(CoreVector2I.Right, 2));
    }

    // Clamp
    [Fact]
    public void Clamp_WithinBounds_ReturnsOriginal()
    {
        var p = new CoreVector2I(5, 5);
        var min = new CoreVector2I(0, 0);
        var max = new CoreVector2I(10, 10);
        Assert.Equal(p, p.Clamp(min, max));
    }

    [Fact]
    public void Clamp_OutsideBounds_ClampsToBounds()
    {
        var p = new CoreVector2I(-1, 15);
        var min = new CoreVector2I(0, 0);
        var max = new CoreVector2I(10, 10);
        Assert.Equal(new CoreVector2I(0, 10), p.Clamp(min, max));
    }

    // IsWithinRegion
    [Fact]
    public void IsWithinRegion_Inside_ReturnsTrue()
    {
        var p = new CoreVector2I(5, 5);
        Assert.True(p.IsWithinRegion(new CoreVector2I(0, 0), new CoreVector2I(10, 10)));
    }

    [Fact]
    public void IsWithinRegion_OnEdge_ReturnsTrue()
    {
        var p = new CoreVector2I(0, 0);
        Assert.True(p.IsWithinRegion(new CoreVector2I(0, 0), new CoreVector2I(10, 10)));
    }

    [Fact]
    public void IsWithinRegion_Outside_ReturnsFalse()
    {
        var p = new CoreVector2I(15, 5);
        Assert.False(p.IsWithinRegion(new CoreVector2I(0, 0), new CoreVector2I(10, 10)));
    }

    // GetOrthogonalPositions
    [Fact]
    public void GetOrthogonalPositions_ReturnsFourNeighbors()
    {
        var p = new CoreVector2I(0, 0);
        var neighbors = p.GetOrthogonalPositions();
        Assert.Equal(4, neighbors.Count);
        Assert.Contains(new CoreVector2I(0, 1), neighbors);
        Assert.Contains(new CoreVector2I(1, 0), neighbors);
        Assert.Contains(new CoreVector2I(0, -1), neighbors);
        Assert.Contains(new CoreVector2I(-1, 0), neighbors);
    }

    // GetAdjacentPositions
    [Fact]
    public void GetAdjacentPositions_ReturnsEightNeighbors()
    {
        var p = new CoreVector2I(0, 0);
        var neighbors = p.GetAdjacentPositions();
        Assert.Equal(8, neighbors.Count);
    }

    // GetLineTo
    [Fact]
    public void GetLineTo_SamePoint_ReturnsOnePoint()
    {
        var p = new CoreVector2I(5, 5);
        var line = p.GetLineTo(p).ToList();
        Assert.Single(line);
        Assert.Equal(p, line[0]);
    }

    [Fact]
    public void GetLineTo_HorizontalLine_ReturnsCorrectPoints()
    {
        var line = new CoreVector2I(0, 0).GetLineTo(new CoreVector2I(3, 0)).ToList();
        Assert.Equal(4, line.Count);
        Assert.Equal(new CoreVector2I(0, 0), line[0]);
        Assert.Equal(new CoreVector2I(3, 0), line[^1]);
    }

    // GetPositionsInRadius
    [Fact]
    public void GetPositionsInRadius_ExcludeCenter_ReturnsOnlyRing()
    {
        var p = new CoreVector2I(0, 0);
        var positions = p.GetPositionsInRadius(1, includeCenter: false).ToList();
        Assert.DoesNotContain(p, positions);
        Assert.All(positions, pos => Assert.True(pos.ManhattanDistanceTo(p) >= 1));
    }

    [Fact]
    public void GetPositionsInRadius_IncludeCenter_ReturnsCenterPlusRing()
    {
        var p = new CoreVector2I(0, 0);
        var positions = p.GetPositionsInRadius(1, includeCenter: true).ToList();
        Assert.Contains(p, positions);
    }

    // GetPositionsInRectangle
    [Fact]
    public void GetPositionsInRectangle_ExcludeCenter_ReturnsNoCenter()
    {
        var p = new CoreVector2I(0, 0);
        var positions = p.GetPositionsInRectangle(new CoreVector2I(3, 3), includeCenter: false).ToList();
        Assert.DoesNotContain(p, positions);
    }
}