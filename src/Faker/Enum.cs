using System;

namespace Faker
{
    /// <summary>
    ///     Generates a random enum value
    /// </summary>
    public static class Enum
    {
        public static T Random<T>()
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("The given type is not an enum");

            var values = System.Enum.GetValues(type);
            if (values.Length == 0)
                throw new ArgumentException("The given enum doesn't have any values");

            var index = RandomNumber.Next(0, values.Length - 1);
            return (T) values.GetValue(index);
        }
    }
}