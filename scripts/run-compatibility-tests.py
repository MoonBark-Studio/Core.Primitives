#!/usr/bin/env python3
"""
MoonBark.Core.Primitives Compatibility Test Runner

Tests compatibility with Godot and Unity engine types.
"""

import os
import subprocess
import sys
from pathlib import Path


def step(message: str):
    """Print a step message."""
    print(f"\n==> {message}")


def warn(message: str):
    """Print a warning message."""
    print(f"WARN: {message}", file=sys.stderr)


def fail(message: str):
    """Print an error message and exit."""
    print(f"ERROR: {message}", file=sys.stderr)
    sys.exit(1)


def run_command(cmd: list, cwd: Path = None, check: bool = True) -> subprocess.CompletedProcess:
    """Run a command and return the result."""
    try:
        result = subprocess.run(
            cmd,
            cwd=cwd,
            check=check,
            capture_output=True,
            text=True
        )
        return result
    except subprocess.CalledProcessError as e:
        print(f"Command failed: {' '.join(cmd)}")
        print(f"Exit code: {e.returncode}")
        print(f"stdout: {e.stdout}")
        print(f"stderr: {e.stderr}")
        raise


def main():
    """Main entry point."""
    print("🧪 MoonBark.Core.Primitives Compatibility Test Suite")
    print("==================================================")

    # Configuration
    project_path = Path("C:/dev/godot/projects/plugins/MoonBark.Core.Primitives")
    test_results_dir = project_path / "TestResults"
    coverage_dir = project_path / "Coverage"

    # Create directories
    test_results_dir.mkdir(parents=True, exist_ok=True)
    coverage_dir.mkdir(parents=True, exist_ok=True)

    print("")
    print("📋 Test Configuration:")
    print(f"  Project Path: {project_path}")
    print(f"  Results Dir: {test_results_dir}")
    print(f"  Coverage Dir: {coverage_dir}")

    # Check if we're in the right directory
    if not (project_path / "MoonBark.Core.Primitives.csproj").exists():
        fail(f"MoonBark.Core.Primitives.csproj not found at {project_path}")

    # Build project
    print("")
    step("Building project...")
    try:
        run_command(
            ["dotnet", "build", "--configuration", "Debug"],
            cwd=project_path
        )
        print("✅ Build successful")
    except subprocess.CalledProcessError:
        fail("Build failed")

    # Run cross-engine compatibility tests
    print("")
    step("Running CrossEngine tests...")
    try:
        run_command([
            "dotnet", "test",
            "--logger", "trx;LogFileName=CrossEngine.trx",
            "--logger", "console;verbosity=detailed",
            "--results-directory", str(test_results_dir),
            "--filter", "FullyQualifiedName~CrossEngine",
            "--collect:XPlat Code Coverage",
            "--settings", "coverlet.runsettings",
            "--configuration", "Debug",
            "--no-build"
        ], cwd=project_path)
        print("✅ CrossEngine tests completed")
    except subprocess.CalledProcessError:
        fail("CrossEngine tests failed")

    # Run performance tests
    print("")
    step("Running performance tests...")
    try:
        run_command([
            "dotnet", "test",
            "--logger", "trx;LogFileName=Performance.trx",
            "--logger", "console;verbosity=detailed",
            "--results-directory", str(test_results_dir),
            "--filter", "FullyQualifiedName~Performance",
            "--configuration", "Debug",
            "--no-build"
        ], cwd=project_path)
        print("✅ Performance tests completed")
    except subprocess.CalledProcessError:
        fail("Performance tests failed")

    # Generate coverage report
    print("")
    step("Generating coverage report...")

    # Install reportgenerator tool if not present
    try:
        subprocess.run(
            ["dotnet-reportgenerator", "--help"],
            capture_output=True,
            check=True
        )
    except (subprocess.CalledProcessError, FileNotFoundError):
        print("Installing reportgenerator...")
        run_command(["dotnet", "tool", "install", "--global", "dotnet-reportgenerator"])

    # Generate HTML report
    try:
        run_command([
            "dotnet-reportgenerator",
            f"-reports:{test_results_dir}/**/coverage.cobertura.xml",
            f"-targetdir:{coverage_dir}",
            "-reporttypes:Html",
            "-verbosity:Error"
        ], cwd=project_path)
        print(f"✅ Coverage report generated: {coverage_dir / 'index.html'}")
    except subprocess.CalledProcessError:
        fail("Coverage report generation failed")

    print("")
    print("🎉 Compatibility test suite completed successfully!")
    print("")
    print("📁 Results:")
    print(f"  Test Results: {test_results_dir}")
    print(f"  Coverage Report: {coverage_dir / 'index.html'}")
    print("")
    print("📝 Summary:")
    print("  ✅ Cross-engine compatibility verified")
    print("  ✅ Mathematical operations validated")
    print("  ✅ Performance benchmarks completed")
    print("  ✅ Coverage report generated")


if __name__ == "__main__":
    main()
