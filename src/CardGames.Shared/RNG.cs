using System;
using System.Security.Cryptography;
using System.Threading;

namespace CardGames.Shared
{
    /// <summary>
    /// Uses <see cref="System.Random" /> with <see cref="ThreadStaticAttribute"/>
    /// and the default implementation of <see cref="RandomNumberGenerator" />
    /// (via <see cref="RandomNumberGenerator.Create()"/>) to generate random numbers.
    /// </summary>
    public static class RNG
    {
        [ThreadStatic]
        private static Random? _random;

        public static Random Random
            => _random ??= new Random(Environment.TickCount + Thread.CurrentThread.ManagedThreadId);

        /// <inheritdoc cref="Random.Next()" />
        public static int Next()
            => Random.Next();

        /// <inheritdoc cref="Random.Next(int)" />
        public static int Next(int maxValue)
            => Random.Next(maxValue);

        /// <inheritdoc cref="Random.Next(int, int)" />
        public static int Next(int maxValue, int minValue)
            => Random.Next(maxValue, minValue);

        /// <inheritdoc cref="Random.NextDouble()" />
        public static double NextDouble()
            => Random.NextDouble();

        /// <inheritdoc cref="Random.NextBytes(byte[])" />
        public static void NextBytes(byte[] buffer)
            => Random.NextBytes(buffer);

        /// <inheritdoc cref="RandomNumberGenerator.GetBytes(byte[], int, int)" />
        public static void NextBytes(byte[] data, int offset, int count)
        {
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(data, offset, count);
        }

        /// <inheritdoc cref="RandomNumberGenerator.GetNonZeroBytes(byte[])" />
        public static void NextNonZeroBytes(byte[] data)
        {
            using var rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(data);
        }
    }
}