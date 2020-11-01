using System;
using System.Security.Cryptography;

namespace Faker
{
    public static class RandomNumber
    {
        private static readonly RandomNumberGenerator Rnd = RandomNumberGenerator.Create();

        private static int Next(this RandomNumberGenerator generator, int min, int max)
        {
            // Catch divide by zero case
            if (max == 0 && min == 0)
            {
                return 0;
            }
            // match Next of Random
            // where max is exclusive
            max = max - 1;

            var bytes = new byte[sizeof(int)]; // 4 bytes
            generator.GetNonZeroBytes(bytes);
            var val = BitConverter.ToInt32(bytes, 0);
            // constrain our values to between our min and max
            // https://stackoverflow.com/a/3057867/86411C:\Work\GitHub\faker-cs\src\Faker\RandomNumber.cs
            var result = ((val - min) % (max - min + 1) + (max - min) + 1) % (max - min + 1) + min;
            return result;
        }

        /// <summary>
        /// Matches behavior from Random.Next from https://docs.microsoft.com/en-us/dotnet/api/system.random.next?view=netcore-3.1
        /// A 32-bit signed integer that is greater than or equal to 0 and less than MaxValue or int.MaxValue
        /// </summary>
        /// <returns></returns>
        public static int Next()
        {
            return Rnd.Next(0, int.MaxValue);
        }

        /// <summary>
        /// Matches behavior from Random.Next from https://docs.microsoft.com/en-us/dotnet/api/system.random.next?view=netcore-3.1
        /// A 32-bit signed integer that is greater than or equal to 0 and less than MaxValue. 
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Next(int max)
        {
            return Rnd.Next(0, max);
        }

        /// <summary>
        /// Matches behavior from Random.Next from https://docs.microsoft.com/en-us/dotnet/api/system.random.next?view=netcore-3.1
        /// A 32-bit signed integer that is greater than or equal to 0 and less than MaxValue. 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Next(int min, int max)
        {
            return Rnd.Next(min, max);
        }
    }
}