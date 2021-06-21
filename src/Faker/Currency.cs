using Faker.Extensions;

namespace Faker
{
    public static class Currency
    {
        public static string ThreeLetterCode()
        {
            return Resources.Currency.Iso3LetterCodes.Split(Config.Separator)
                .Random();
        }

        public static string Name()
        {
            return Resources.Currency.Names.Split(Config.Separator)
                .Random();
        }
    }
}