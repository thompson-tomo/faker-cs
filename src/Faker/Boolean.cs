namespace Faker
{
    /// <summary>
    /// Generates a random boolean value
    /// </summary>
    public static class Boolean
    {
        public static bool Random()
        {
            int index = RandomNumber.Next(0, 2);
            return index != 0;
        }
    }
}