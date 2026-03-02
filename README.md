# MoonBark.Core.Primitives

Reusable primitive types for the MoonBark ecosystem.

## Overview

This library provides core primitive types used across MoonBark plugins and projects:

- **CoreColor** - Enhanced color type with additional utilities
- **CoreVector2** - 2D vector type with extended functionality  
- **CoreVector2I** - 2D integer vector type for grid operations
- **CoreVector3** - 3D vector type for spatial operations
- **CoreRect2** - 2D rectangle type (float coordinates)
- **CoreRect2I** - 2D rectangle type (integer coordinates)
- **CoreTransform** - Transform type (position, rotation, scale)

## Usage

```csharp
using MoonBark.Core.Types;

// Create colors
var color = CoreColors.Red;
var customColor = new CoreColor(1.0f, 0.5f, 0.0f);

// Work with vectors
var position = new CoreVector2(10.5f, 20.0f);
var gridPos = new CoreVector2I(10, 20);
var position3D = new CoreVector3(0.0f, 1.0f, 0.0f);

// Work with rectangles
var rect = new CoreRect2(0.0f, 0.0f, 100.0f, 50.0f);
var gridRect = new CoreRect2I(0, 0, 10, 5);

// Work with transforms
var transform = new CoreTransform(
    new CoreVector3(0, 0, 0),    // position
    new CoreVector3(0, 90, 0),   // rotation (degrees)
    new CoreVector3(1, 1, 1)     // scale
);
```

## Features

### Cross-Platform Compatibility
- Engine-agnostic implementations
- Consistent APIs across Godot, Unity, and other platforms
- No engine-specific dependencies

### Enhanced Functionality
- Rich set of operators and utility methods
- Immutable struct design for performance
- Comprehensive equality and hashing support

### Namespace: `MoonBark.Core.Types`
All types are organized under the `MoonBark.Core.Types` namespace to avoid conflicts and provide clear organization.

## Integration

This library is designed to be:
- Used as a git submodule in MoonBark projects
- Published as a NuGet package for broader distribution
- Shared across multiple Godot and Unity projects

## Development

This is a foundational library for the MoonBark ecosystem, providing consistent types and utilities across all projects.