using System;
using System.IO.Hashing;
using System.Runtime.InteropServices;

namespace MoonBark.Core.Utils;

/// <summary>
/// Provides high-performance, deterministic string hashing for ECS catalogs.
/// Wraps Microsoft's System.IO.Hashing.XxHash32, the industry standard for fast, 
/// non-cryptographic 32-bit hashing.
/// </summary>
public static class StringHash
{
    /// <summary>
    /// Hashes a string deterministically using XxHash32 into an integer.
    /// Crucial for guaranteeing O(1) integer dictionary lookups within ECS core loops.
    /// </summary>
    public static int GetHash(string value)
    {
        if (string.IsNullOrEmpty(value)) return 0;
        
        // Zero-allocation span conversion of the native UTF-16 string memory.
        // Bypasses the Garbage Collector entirely.
        ReadOnlySpan<byte> bytes = MemoryMarshal.AsBytes(value.AsSpan());
        
        // HashToUInt32 is heavily SIMD-optimized under the hood in .NET 8
        return unchecked((int)XxHash32.HashToUInt32(bytes));
    }
}
