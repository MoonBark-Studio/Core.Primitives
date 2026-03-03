@echo off
REM MoonBark.Core.Primitives Compatibility Test Runner for Windows
REM Tests compatibility with Godot and Unity engine types

setlocal enabledelayedexpansion

echo 🧪 MoonBark.Core.Primitives Compatibility Test Suite
echo ==================================================

REM Configuration
set PROJECT_PATH=C:\dev\godot\projects\plugins\MoonBark.Core.Primitives
set TEST_RESULTS_DIR=%PROJECT_PATH%\TestResults
set COVERAGE_DIR=%PROJECT_PATH%\Coverage

REM Create directories
if not exist "%TEST_RESULTS_DIR%" mkdir "%TEST_RESULTS_DIR%"
if not exist "%COVERAGE_DIR%" mkdir "%COVERAGE_DIR%"

echo.
echo 📋 Test Configuration:
echo   Project Path: %PROJECT_PATH%
echo   Results Dir: %TEST_RESULTS_DIR%
echo   Coverage Dir: %COVERAGE_DIR%

REM Function to run tests with coverage
echo.
echo 🔨 Building project...
cd /d "%PROJECT_PATH%"
dotnet build --configuration Debug
if %ERRORLEVEL% neq 0 (
    echo ❌ Build failed
    exit /b 1
)
echo ✅ Build successful

echo.
echo 🔍 Running cross-engine compatibility tests...
dotnet test ^
    --logger "trx;LogFileName=CrossEngine.trx" ^
    --logger "console;verbosity=detailed" ^
    --results-directory "%TEST_RESULTS_DIR%" ^
    --filter "FullyQualifiedName~CrossEngine" ^
    --collect:"XPlat Code Coverage" ^
    --configuration Debug ^
    --no-build
if %ERRORLEVEL% neq 0 (
    echo ❌ CrossEngine tests failed
    exit /b 1
)
echo ✅ CrossEngine tests completed

echo.
echo ⚡ Running performance tests...
dotnet test ^
    --logger "trx;LogFileName=Performance.trx" ^
    --logger "console;verbosity=detailed" ^
    --results-directory "%TEST_RESULTS_DIR%" ^
    --filter "FullyQualifiedName~Performance" ^
    --configuration Debug ^
    --no-build
if %ERRORLEVEL% neq 0 (
    echo ❌ Performance tests failed
    exit /b 1
)
echo ✅ Performance tests completed

echo.
echo 📊 Generating coverage report...

REM Install reportgenerator tool if not present
dotnet tool install --global dotnet-reportgenerator

REM Generate HTML report
dotnet-reportgenerator ^
    -reports:"%TEST_RESULTS_DIR%\**\coverage.cobertura.xml" ^
    -targetdir:"%COVERAGE_DIR%" ^
    -reporttypes:Html ^
    -verbosity:Error
if %ERRORLEVEL% neq 0 (
    echo ❌ Coverage report generation failed
    exit /b 1
)

echo ✅ Coverage report generated: %COVERAGE_DIR%\index.html

echo.
echo 🎉 Compatibility test suite completed successfully!
echo.
echo 📁 Results:
echo   Test Results: %TEST_RESULTS_DIR%
echo   Coverage Report: %COVERAGE_DIR%\index.html
echo.
echo 📝 Summary:
echo   ✅ Cross-engine compatibility verified
echo   ✅ Mathematical operations validated
echo   ✅ Performance benchmarks completed
echo   ✅ Coverage report generated

pause
