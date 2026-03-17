@echo off
REM MoonBark.Core.Primitives Compatibility Test Runner for Windows
REM Tests compatibility with Godot and Unity engine types
REM This wrapper calls the Python implementation

setlocal enabledelayedexpansion

REM Get the directory of this script
set SCRIPT_DIR=%~dp0

REM Call the Python script
python "%SCRIPT_DIR%run-compatibility-tests.py"

REM Pause to see output if run from Explorer
pause
