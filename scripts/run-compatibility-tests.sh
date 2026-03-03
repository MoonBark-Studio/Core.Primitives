#!/bin/bash

# MoonBark.Core.Primitives Compatibility Test Runner
# Tests compatibility with Godot and Unity engine types

set -e

echo "🧪 MoonBark.Core.Primitives Compatibility Test Suite"
echo "=================================================="

# Configuration
PROJECT_PATH="C:/dev/godot/projects/plugins/MoonBark.Core.Primitives"
TEST_RESULTS_DIR="$PROJECT_PATH/TestResults"
COVERAGE_DIR="$PROJECT_PATH/Coverage"

# Create directories
mkdir -p "$TEST_RESULTS_DIR"
mkdir -p "$COVERAGE_DIR"

echo ""
echo "📋 Test Configuration:"
echo "  Project Path: $PROJECT_PATH"
echo "  Results Dir: $TEST_RESULTS_DIR"
echo "  Coverage Dir: $COVERAGE_DIR"

# Function to run tests with coverage
run_tests() {
    local test_name="$1"
    local filter="$2"
    
    echo ""
    echo "🔍 Running $test_name tests..."
    
    cd "$PROJECT_PATH"
    
    dotnet test \
        --logger "trx;LogFileName=$test_name.trx" \
        --logger "console;verbosity=detailed" \
        --results-directory "$TEST_RESULTS_DIR" \
        --filter "$filter" \
        --collect:"XPlat Code Coverage" \
        --settings "coverlet.runsettings" \
        --configuration Debug \
        --no-build || {
        echo "❌ $test_name tests failed"
        return 1
    }
    
    echo "✅ $test_name tests completed"
}

# Function to run performance tests
run_performance_tests() {
    echo ""
    echo "⚡ Running performance tests..."
    
    cd "$PROJECT_PATH"
    
    dotnet test \
        --logger "trx;LogFileName=Performance.trx" \
        --logger "console;verbosity=detailed" \
        --results-directory "$TEST_RESULTS_DIR" \
        --filter "FullyQualifiedName~Performance" \
        --configuration Debug \
        --no-build || {
        echo "❌ Performance tests failed"
        return 1
    }
    
    echo "✅ Performance tests completed"
}

# Function to generate coverage report
generate_coverage_report() {
    echo ""
    echo "📊 Generating coverage report..."
    
    cd "$PROJECT_PATH"
    
    # Install reportgenerator tool if not present
    if ! command -v dotnet-reportgenerator &> /dev/null; then
        echo "Installing reportgenerator..."
        dotnet tool install --global dotnet-reportgenerator
    fi
    
    # Generate HTML report
    dotnet-reportgenerator \
        -reports:"$TEST_RESULTS_DIR/**/coverage.cobertura.xml" \
        -targetdir:"$COVERAGE_DIR" \
        -reporttypes:Html \
        -verbosity:Error || {
        echo "❌ Coverage report generation failed"
        return 1
    }
    
    echo "✅ Coverage report generated: $COVERAGE_DIR/index.html"
}

# Main execution
main() {
    echo ""
    echo "🚀 Starting compatibility test suite..."
    
    # Build project
    echo ""
    echo "🔨 Building project..."
    cd "$PROJECT_PATH"
    dotnet build --configuration Debug || {
        echo "❌ Build failed"
        exit 1
    }
    echo "✅ Build successful"
    
    # Run cross-engine compatibility tests
    run_tests "CrossEngine" "FullyQualifiedName~CrossEngine"
    
    # Run performance tests
    run_performance_tests
    
    # Generate coverage report
    generate_coverage_report
    
    echo ""
    echo "🎉 Compatibility test suite completed successfully!"
    echo ""
    echo "📁 Results:"
    echo "  Test Results: $TEST_RESULTS_DIR"
    echo "  Coverage Report: $COVERAGE_DIR/index.html"
    echo ""
    echo "📝 Summary:"
    echo "  ✅ Cross-engine compatibility verified"
    echo "  ✅ Mathematical operations validated"
    echo "  ✅ Performance benchmarks completed"
    echo "  ✅ Coverage report generated"
}

# Check if we're in the right directory
if [ ! -f "$PROJECT_PATH/MoonBark.Core.Primitives.csproj" ]; then
    echo "❌ Error: MoonBark.Core.Primitives.csproj not found at $PROJECT_PATH"
    echo "Please ensure you're running this script from the correct location."
    exit 1
fi

# Run main function
main "$@"
