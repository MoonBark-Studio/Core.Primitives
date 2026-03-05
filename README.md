# MoonBark.Core.Primitives

Reusable primitive types for the MoonBark ecosystem, providing engine-agnostic implementations for consistent cross-platform development.

## Overview

This library provides core primitive types used across MoonBark plugins and projects. It offers engine-agnostic implementations of common game development types, enabling consistent APIs across Godot, Unity, and other platforms.

## Features

- **Engine-Agnostic**: Pure C# implementations without engine dependencies
- **Cross-Platform**: Consistent APIs across Godot, Unity, and other platforms
- **Type-Safe**: Strongly-typed structs for performance and safety
- **Enhanced Functionality**: Rich set of operators and utility methods
- **Immutable Design**: Immutable struct design for thread safety and performance
- **Comprehensive Equality**: Full equality and hashing support
- **Namespace Organization**: All types under `MoonBark.Core.Types` namespace

## Types Provided

### CoreColor
Enhanced color type with additional utilities:
- RGBA color representation
- Color conversion methods
- Color manipulation utilities
- Predefined color constants

### CoreVector2
2D vector type with extended functionality:
- Position and direction representation
- Vector arithmetic operations
- Distance and length calculations
- Normalization and interpolation
- Common vector operations (dot product, cross product, etc.)

### CoreVector2I
2D integer vector type for grid operations:
- Grid coordinate representation
- Integer arithmetic
- Grid-based calculations
- Tile and chunk operations

### CoreVector3
3D vector type for spatial operations:
- 3D position and direction
- 3D vector arithmetic
- Distance and length calculations
- Normalization and interpolation
- Common 3D vector operations

### CoreRect2
2D rectangle type (float coordinates):
- Axis-aligned bounding boxes
- Collision detection
- Area calculations
- Intersection and containment tests

### CoreRect2I
2D rectangle type (integer coordinates):
- Grid-based rectangles
- Tile-based collision
- Integer area calculations
- Grid intersection tests

### CoreTransform
Transform type (position, rotation, scale):
- 3D transformation representation
- Position, rotation, and scale
- Transform composition
- Local-to-world and world-to-local conversions

## Usage

### Creating Colors

```csharp
using MoonBark.Core.Types;

// Use predefined colors
var red = CoreColors.Red;
var green = CoreColors.Green;
var blue = CoreColors.Blue;

// Create custom colors
var customColor = new CoreColor(1.0f, 0.5f, 0.0f);  // RGB
var colorWithAlpha = new CoreColor(1.0f, 0.5f, 0.0f, 0.8f);  // RGBA

// From hex
var hexColor = CoreColor.FromHex("#FF5733");

// Color operations
var blended = CoreColor.Lerp(red, blue, 0.5f);
var darker = red.Darken(0.2f);
var lighter = green.Lighten(0.2f);
```

### Working with Vectors

```csharp
using MoonBark.Core.Types;

// Create vectors
var position = new CoreVector2(10.5f, 20.0f);
var gridPos = new CoreVector2I(10, 20);
var position3D = new CoreVector3(0.0f, 1.0f, 0.0f);

// Vector arithmetic
var sum = position + new CoreVector2(5.0f, 5.0f);
var difference = position - new CoreVector2(5.0f, 5.0f);
var scaled = position * 2.0f;
var normalized = position.Normalized();

// Vector operations
var length = position.Length;
var distance = position.DistanceTo(new CoreVector2(15.0f, 25.0f));
var dotProduct = CoreVector2.Dot(position, new CoreVector2(1.0f, 0.0f));
var direction = position.DirectionTo(new CoreVector2(15.0f, 25.0f));

// Interpolation
var interpolated = CoreVector2.Lerp(position, target, 0.5f);
```

### Working with Rectangles

```csharp
using MoonBark.Core.Types;

// Create rectangles
var rect = new CoreRect2(0.0f, 0.0f, 100.0f, 50.0f);
var gridRect = new CoreRect2I(0, 0, 10, 5);

// Rectangle operations
var area = rect.Area;
var center = rect.Center;
var expanded = rect.Expand(10.0f);
var shrunk = rect.Shrink(5.0f);

// Collision detection
var contains = rect.Contains(new CoreVector2(50.0f, 25.0f));
var intersects = rect.Intersects(new CoreRect2(50.0f, 0.0f, 100.0f, 50.0f));
var intersection = rect.Intersection(new CoreRect2(50.0f, 0.0f, 100.0f, 50.0f));
```

### Working with Transforms

```csharp
using MoonBark.Core.Types;

// Create transforms
var transform = new CoreTransform(
    new CoreVector3(0, 0, 0),    // position
    new CoreVector3(0, 90, 0),   // rotation (degrees)
    new CoreVector3(1, 1, 1)     // scale
);

// Transform operations
var localPoint = new CoreVector3(1, 0, 0);
var worldPoint = transform.TransformPoint(localPoint);
var localPoint2 = transform.InverseTransformPoint(worldPoint);

// Transform composition
var childTransform = new CoreTransform(
    new CoreVector3(1, 0, 0),
    CoreVector3.Zero,
    CoreVector3.One
);
var combinedTransform = transform * childTransform;
```

## Integration

### As Git Submodule

This library is designed to be used as a git submodule in MoonBark projects:

```bash
# Add as submodule
git submodule add https://github.com/MoonBark-Studio/Core.Primitives.git plugins/MoonBark.Core.Primitives

# Update submodule
git submodule update --remote plugins/MoonBark.Core.Primitives
```

### As NuGet Package

This library can be published as a NuGet package for broader distribution:

```bash
# Install via NuGet
dotnet add package MoonBark.Core.Primitives
```

### Project Reference

For local development, reference the project directly:

```xml
<ProjectReference Include="..\MoonBark.Core.Primitives\MoonBark.Core.Primitives.csproj" />
```

## Namespace Organization

All types are organized under the `MoonBark.Core.Types` namespace to avoid conflicts and provide clear organization:

```csharp
using MoonBark.Core.Types;

// Now all types are available without qualification
var position = new CoreVector2(10, 20);
var color = CoreColors.Red;
var rect = new CoreRect2(0, 0, 100, 50);
```

## Design Principles

### Engine Agnosticism

All types are implemented without engine-specific dependencies, making them usable across different game engines and platforms:

```csharp
// Works in Godot
var godotPosition = new CoreVector2(10, 20);

// Works in Unity
var unityPosition = new CoreVector2(10, 20);

// Works in any .NET application
var appPosition = new CoreVector2(10, 20);
```

### Immutable Structs

All types are immutable structs for performance and thread safety:

```csharp
var position = new CoreVector2(10, 20);
var newPosition = position + new CoreVector2(5, 5);  // Creates new instance

// Original is unchanged
Console.WriteLine(position);  // (10, 20)
Console.WriteLine(newPosition);  // (15, 25)
```

### Comprehensive Operators

Rich set of operators for intuitive usage:

```csharp
var a = new CoreVector2(1, 2);
var b = new CoreVector2(3, 4);

var sum = a + b;           // Addition
var diff = a - b;          // Subtraction
var scaled = a * 2.0f;     // Scalar multiplication
var divided = a / 2.0f;    // Scalar division
var negated = -a;          // Negation
```

## Common Patterns

### Position and Movement

```csharp
// Entity position
var position = new CoreVector2(100, 200);
var velocity = new CoreVector2(5, 3);

// Update position
position += velocity * deltaTime;

// Distance to target
var target = new CoreVector2(150, 250);
var distance = position.DistanceTo(target);

// Direction to target
var direction = position.DirectionTo(target);
```

### Collision Detection

```csharp
// Entity bounds
var entityBounds = new CoreRect2(position.X, position.Y, 32, 32);

// Check collision
var obstacleBounds = new CoreRect2(150, 250, 64, 64);
if (entityBounds.Intersects(obstacleBounds))
{
    HandleCollision();
}

// Point in rectangle check
if (entityBounds.Contains(mousePosition))
{
    HandleClick();
}
```

### Grid Operations

```csharp
// Grid coordinates
var gridPosition = new CoreVector2I(5, 3);

// Convert to world position
var cellSize = 32.0f;
var worldPosition = new CoreVector2(
    gridPosition.X * cellSize,
    gridPosition.Y * cellSize
);

// Convert world to grid
var gridX = (int)(worldPosition.X / cellSize);
var gridY = (int)(worldPosition.Y / cellSize);
var gridPos = new CoreVector2I(gridX, gridY);
```

### Color Gradients

```csharp
// Create color gradient
var startColor = CoreColors.Blue;
var endColor = CoreColors.Red;
var t = 0.5f;  // 0 to 1

var interpolatedColor = CoreColor.Lerp(startColor, endColor, t);

// Apply to health bar
var healthPercent = 0.7f;
var healthColor = CoreColor.Lerp(CoreColors.Red, CoreColors.Green, healthPercent);
```

## Performance Considerations

- **Structs**: All types are value types for stack allocation
- **Immutable**: No mutation overhead, safe for multithreading
- **No Boxing**: Generic operations avoid boxing
- **Cache-Friendly**: Compact memory layout

## Requirements

- .NET 8.0

## Dependencies

No external dependencies required.

## Project Structure

```
MoonBark.Core.Primitives/
├── src/
│   └── Core/
│       └── Types/
│           ├── CoreColor.cs
│           ├── CoreColors.cs
│           ├── CoreVector2.cs
│           ├── CoreVector2I.cs
│           ├── CoreVector3.cs
│           ├── CoreRect2.cs
│           ├── CoreRect2I.cs
│           └── CoreTransform.cs
├── tests/
│   ├── Compatibility/
│   │   ├── CrossEngineCompatibilityTests.cs
│   │   ├── GodotPatternTests.cs
│   │   └── PerformanceCompatibilityTests.cs
│   └── Unit/
│       └── CoreColorTests.cs
├── scripts/
│   ├── run-compatibility-tests.bat
│   └── run-compatibility-tests.sh
├── docs/
│   └── COMPATIBILITY_TEST_RESULTS.md
├── MoonBark.Core.Primitives.csproj
└── README.md
```

## Integration with Other Plugins

This plugin is used by:
- **GridPathfinding**: For vector and grid operations
- **EntitySensors**: For position and distance calculations
- **CommandSystem**: For position and direction types
- **Stats**: For potential stat-related types
- **All MoonBark plugins**: As a shared type library

## Compatibility

This library provides cross-platform compatibility:

- **Godot**: Compatible with Godot 4.x
- **Unity**: Compatible with Unity 2022+
- **Other**: Compatible with any .NET 8.0 application

See `docs/COMPATIBILITY_TEST_RESULTS.md` for detailed compatibility test results.

## Examples

### Entity Movement

```csharp
public class Entity
{
    public CoreVector2 Position { get; private set; }
    public CoreVector2 Velocity { get; private set; }
    
    public void Update(float deltaTime)
    {
        Position += Velocity * deltaTime;
    }
    
    public void MoveTo(CoreVector2 target, float speed)
    {
        var direction = Position.DirectionTo(target);
        Velocity = direction * speed;
    }
}
```

### Camera System

```csharp
public class Camera
{
    public CoreVector2 Position { get; private set; }
    public float Zoom { get; private set; }
    
    public CoreRect2 GetViewport()
    {
        var size = new CoreVector2(1920, 1080) / Zoom;
        return new CoreRect2(
            Position.X - size.X / 2,
            Position.Y - size.Y / 2,
            size.X,
            size.Y
        );
    }
    
    public bool IsVisible(CoreRect2 bounds)
    {
        return GetViewport().Intersects(bounds);
    }
}
```

### Color System

```csharp
public class HealthBar
{
    public void Update(float healthPercent)
    {
        // Interpolate between red (low health) and green (high health)
        var color = CoreColor.Lerp(CoreColors.Red, CoreColors.Green, healthPercent);
        SetColor(color);
    }
}
```

## License

MIT License - See LICENSE file for details.

## Contributing

This is a foundational library for the MoonBark ecosystem. Contributions should maintain engine agnosticism and cross-platform compatibility.