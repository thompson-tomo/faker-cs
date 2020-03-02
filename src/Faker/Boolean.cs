namespace Faker
{
    public static class Boolean
    {
        public static bool Random()
        {
            var index = RandomNumber.Next(0, 2);
            return index != 0;
        }
    }
}