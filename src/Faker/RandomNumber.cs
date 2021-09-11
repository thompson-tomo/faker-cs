using System;
using System.Security.Cryptography;

namespace Faker
{
    // constrain our values to between our min and max
    // https://stackoverflow.com/a/3057867/86411
    //
    public static class RandomNumber
    {
        private static readonly RandomNumberGenerator Rnd = RandomNumberGenerator.Create();

        private static int Next(this RandomNumberGenerator generator, int min, int max)
        {
            var bytes = new byte[sizeof(int)];
            generator.GetNonZeroBytes(bytes);
            var val = BitConverter.ToUInt32(bytes, 0);
            var result = ((val - min) % (max - min + 1) + (max - min) + 1) % (max - min + 1) + min;
            return (int)result;
        }

        private static long Next(this RandomNumberGenerator generator, long min, long max)
        {
            var bytes = new byte[sizeof(long)];
            generator.GetNonZeroBytes(bytes);
            var val = BitConverter.ToInt32(bytes, 0);
            var result = ((val - min) % (max - min + 1) + (max - min) + 1) % (max - min + 1) + min;
            return result;
        }

        public static int Next()
        {
            return Rnd.Next(0, int.MaxValue);
        }

        public static int Next(int max)
        {
            return Rnd.Next(0, max);
        }

        public static long Next(long max)
        {
            return Rnd.Next(0, max);
        }

        public static int Next(int min, int max)
        {
            return Rnd.Next(min, max);
        }

        public static long Next(long min, long max)
        {
            return Rnd.Next(min, max);
        }
    }
}