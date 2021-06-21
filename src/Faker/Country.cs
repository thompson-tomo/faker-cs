using Faker.Extensions;

namespace Faker
{
    public static class Country
    {
        public static string TwoLetterCode()
        {
            return Resources.Country.Iso2LetterCodes.Split(Config.Separator)
                .Random();
        }

        public static string Name()
        {
            return Resources.Country.Names.Split(Config.Separator)
                .Random();
        }
    }
}