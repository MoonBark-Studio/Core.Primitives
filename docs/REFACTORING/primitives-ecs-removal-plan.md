# Primitives ECS Removal Plan

**Date:** 2026-04-01  
**Project:** MoonBark.Core.Primitives  
**Goal:** Remove `Friflo.Engine.ECS` dependency from MoonBark.Core.Primitives

---

## Executive Summary

**Finding: The `Friflo.Engine.ECS` dependency in `MoonBark.Core.Primitives.csproj` is unused.**

None of the 8 source files in `src/` actually reference any Friflo types. The `.csproj` lists the package as a direct dependency, but no `using Friflo.Engine.ECS` or `using Friflo` directives appear in any source file. The only mention of "ECS" is in the `StringHash.cs` doc comment ("Provides high-performance, deterministic string hashing for **ECS** catalogs"), which is documentation—not actual ECS coupling.

Additionally, the entire `MoonBark.Core.Primitives` library is **deprecated as of March 2026** per its own README.md, which states types should be implemented locally in consuming plugins rather than as a shared dependency.

**Recommended Action:** Remove the unused `Friflo.Engine.ECS` package reference from the `.csproj`. No source code changes are needed since nothing uses it.

---

## Files Examined

### Source Files (8 total — NONE use ECS)

| File | Lines | ECS Usage |
|------|-------|-----------|
| `src/Core/Types/CoreColor.cs` | ~70 | None. Pure `CoreColor` struct with RGBA, predefined colors, lerp. |
| `src/Core/Types/CoreRect2.cs` | ~100 | None. Pure `CoreRect2` float rectangle struct. |
| `src/Core/Types/CoreRect2I.cs` | ~80 | None. Pure `CoreRect2I` integer rectangle struct. |
| `src/Core/Types/CoreTransform.cs` | ~50 | None. Pure `CoreTransform` (position/rotation/scale). |
| `src/Core/Types/CoreVector2.cs` | ~80 | None. Pure `CoreVector2` float vector. |
| `src/Core/Types/CoreVector2I.cs` | ~80 | None. Pure `CoreVector2I` integer vector for grid ops. |
| `src/Core/Types/CoreVector3.cs` | ~90 | None. Pure `CoreVector3` float 3D vector. |
| `src/Core/Utils/StringHash.cs` | ~40 | None. Uses `System.IO.Hashing.XxHash32`. Doc comment mentions "ECS catalogs" but no actual ECS type coupling. |

### Other Project Files

| File | Notes |
|------|-------|
| `MoonBark.Core.Primitives.csproj` | Contains `<PackageReference Include="Friflo.Engine.ECS" Version="3.6.0" />` — **unused** |
| `src/Core/ECS/` | Empty directory (placeholder, no files) |
| `README.md` | ⚠️ **DEPRECATED** as of March 2026. States types should be migrated to local implementations. |

---

## ECS Usage Analysis

### What the .csproj Contains

```xml
<ItemGroup>
  <PackageReference Include="Friflo.Engine.ECS" Version="3.6.0" />
  <PackageReference Include="System.IO.Hashing" Version="10.0.5" />
</ItemGroup>
```

### What the Source Files Actually Use

- `CoreColor.cs` → `System`, `Math`
- `CoreRect2.cs` → `System`, `Math`
- `CoreRect2I.cs` → `System`
- `CoreTransform.cs` → `System`
- `CoreVector2.cs` → `System`, `MathF`
- `CoreVector2I.cs` → `System`, `MathF`
- `CoreVector3.cs` → `System`, `MathF`
- `StringHash.cs` → `System`, `System.IO.Hashing`, `System.Runtime.InteropServices`

**Zero `using Friflo` directives across all 8 source files.**

---

## Consumer Projects (15 total)

The `MoonBark.Core.Primitives` package is referenced by:

| Consumer | Actually Uses ECS? | Needs Primitives? |
|----------|-------------------|-------------------|
| `CharacterControl/Core/CharacterControl.Core.csproj` | Unknown | Yes - types only |
| `EntitySensors/EntitySensors.csproj` | Unknown | Yes - types only |
| `GridPathfinding/GridPathfinding.Core.csproj` | Unknown | Yes - types only |
| `GridPlacement/cs/Core/GridPlacement.Core.csproj` | **No** (ECS is in `GridPlacement.ECS`) | Yes - CoreVector2, CoreVector2I |
| `GridPlacement/cs/ECS/GridPlacement.ECS.csproj` | **Yes** | Yes - types + ECS |
| `GridPlacement/cs/Godot/GridPlacement.Godot.csproj` | **No** | Yes - types only |
| `GridPlacement/cs/Godot/addons/grid_placement/GridPlacement.csproj` | **No** | Yes - types only |
| `GridPlacement/cs/Godot/test/GridPlacement.Tests.csproj` | Mixed | Yes - types only |
| `GridPlacement/cs/test/Core/GridBuilding.Core.Tests.csproj` | **No** | Yes - types only |
| `GridPlacement/cs/test/Performance/PlacementPerformanceComparison.csproj` | Mixed | Yes - types only |
| `GridPlacement/cs/Unity/GridPlacement.Unity.csproj` | **No** | Yes - types only |
| `Inventory/Tests/Inventory.Core.Tests.csproj` | Unknown | Yes - types only |
| `MoonBark.AI/MoonBark.AI.csproj` | Unknown | Yes - types only |
| `Wiring/MoonBark.Wiring.Core.csproj` | **Yes** (has direct Friflo package ref) | Yes - types only |
| `Wiring/MoonBark.Wiring.Godot.csproj` | Unknown | Yes - types only |

**Key observation:** The `MoonBark.Wiring.Core.csproj` already has its own direct `Friflo.Engine.ECS` package reference. The ECS dependency does NOT need to flow through `MoonBark.Core.Primitives`.

---

## What "ECS Usage" Actually Means in This Project

The `Friflo.Engine.ECS` reference in `MoonBark.Core.Primitives.csproj` is a **transitive dependency artifact**, not a genuine code dependency. The library's source files have no coupling to ECS concepts.

The dependency likely exists because:
1. Some early draft of the library planned to include ECS-idiomatic types (e.g., `EntityId`, `ComponentId`)
2. It was copy-pasted from another MoonBark project template
3. It was intended for "future use" but never materialized

None of the 8 source files provide:
- `Entity`, `EntityId`, or entity reference types
- `Query` or query helpers
- `Archetype` references
- `Component` base types or markers
- Any `World`-scoped operations

---

## Recommended Approach: Option A (Minimal)

**Option A: Remove the unused `Friflo.Engine.ECS` package reference**

Since no source code actually uses ECS, the fix is a **single-line .csproj change**:

```xml
<!-- Remove this from MoonBark.Core.Primitives.csproj -->
<PackageReference Include="Friflo.Engine.ECS" Version="3.6.0" />
```

This is the correct approach because:
1. The library is **deprecated** anyway — significant refactoring is not warranted
2. **Zero source changes required** — no code actually depends on ECS
3. **No breaking changes** — consumers will not be affected (they were not using ECS through this package anyway)
4. **Risk: Minimal** — only removes an unused dependency
5. The library's own README says types should be migrated to local implementations, so the goal is just to leave the package clean before it possibly gets archived

### Option B and C are Not Warranted

- **Option B (split into two packages)** — Overkill for a deprecated library with no actual ECS code
- **Option C (interface abstraction)** — There is no concrete ECS usage to abstract; the dependency is entirely absent from the source

---

## Migration Steps

### Step 1: Remove unused package reference

**File:** `MoonBark.Core.Primitives.csproj`

**Change:** Remove the `Friflo.Engine.ECS` package reference.

**Risk:** None. The package is unused in source.

```xml
<!-- Before -->
<ItemGroup>
  <PackageReference Include="Friflo.Engine.ECS" Version="3.6.0" />
  <PackageReference Include="System.IO.Hashing" Version="10.0.5" />
</ItemGroup>

<!-- After -->
<ItemGroup>
  <PackageReference Include="System.IO.Hashing" Version="10.0.5" />
</ItemGroup>
```

### Step 2: Verify build still succeeds

```bash
dotnet build MoonBark.Core.Primitives.csproj
```

**Risk:** None. No source changes.

### Step 3: Run tests if any exist

```bash
dotnet test MoonBark.Core.Primitives.csproj  # if tests exist
```

### Step 4: Update README if desired

The README says "Dependencies: No external dependencies required" but the csproj had `Friflo.Engine.ECS`. After removal, the README would be accurate.

**Risk:** Documentation only.

---

## Deprecation Context

Per the README.md (dated March 2026):

> **⚠️ DEPRECATED - This library is no longer maintained.**
>
> This library has been deprecated as of March 2026. The types provided by this library should be implemented locally in the plugins that need them, rather than as a shared dependency.

This means the proper long-term direction is for consumers (particularly `GridPlacement.Core`) to migrate the types into their own namespace (`GridPlacement.Core.Types` as the README already suggests). However, that migration is **out of scope** for this ECS-removal task.

The ECS removal is simply about **cleaning up an unused dependency** from an already-deprecated library.

---

## Summary Table

| Aspect | Finding |
|--------|---------|
| Source files in `Core.Primitives` | 8 files |
| Files using `Friflo.Engine.ECS` | **0 files** |
| Files with `using Friflo` directive | **0 files** |
| Actual ECS types referenced | **None** |
| Package reference is | **Unused — dead weight** |
| Library status | **Deprecated** |
| Recommended fix | Remove one `<PackageReference>` line |
| Estimated risk | **Minimal** |
| Source changes needed | **None** |

---

## Conclusion

The `Friflo.Engine.ECS` dependency in `MoonBark.Core.Primitives.csproj` is completely unused. No source file references any Friflo type. The fix is a single package reference removal in the `.csproj` file. No refactoring of source code is needed.

Given the library is already deprecated (per its own README), this change is purely about leaving the package in a clean state rather than shipping an unused transitive dependency to consumers.
