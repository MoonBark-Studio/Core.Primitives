# MoonBark.Core.Primitives

Reusable primitive types for the MoonBark ecosystem.

## Overview

This library provides core primitive types used across MoonBark plugins and projects:

- **CoreColor** - Enhanced color type with additional utilities
- **CoreVector2** - 2D vector type with extended functionality  
- **CoreVector2I** - 2D integer vector type for grid operations

## Usage

```csharp
using GridPlacement.Core.Types;

// Create colors
var color = CoreColors.Red;
var customColor = new CoreColor(1.0f, 0.5f, 0.0f);

// Work with vectors
var position = new CoreVector2(10.5f, 20.0f);
var gridPos = new CoreVector2I(10, 20);
```

## Integration

This library is designed to be:
- Used as a git submodule in MoonBark projects
- Published as a NuGet package for broader distribution
- Shared across multiple Godot and Unity projects

## Development

This is a foundational library for the MoonBark ecosystem, providing consistent types and utilities across all projects.