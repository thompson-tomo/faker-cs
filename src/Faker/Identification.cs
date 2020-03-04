using System.Linq;
using System.Text;
using Faker.Extensions;

namespace Faker
{
    public static class Identification
    {
        private const string Alpha = "ACDEFGHJKMNPQRTUVWXY";
        private const string Numeric = "0123456789";
        private static readonly string AlphaNumeric = string.Join("", Alpha, Numeric);
        private static readonly string Numeric19 = Numeric.Substring(1);

        /*
        Position 1 – numeric values 1 thru 9
        Position 2 – alphabetic values A thru Z (minus S, L, O, I, B, Z)
        Position 3 – alpha-numeric values 0 thru 9 and A thru Z (minus S, L, O, I, B, Z) Position 4 – numeric values 0 thru 9
        Position 4 – numeric values 0 thru 9
        Position 5 – alphabetic values A thru Z (minus S, L, O, I, B, Z)
        Position 6 – alpha-numeric values 0 thru 9 and A thru Z (minus S, L, O, I, B, Z) Position 7 – numeric values 0 thru 9
        Position 7 – numeric values 0 thru 9
        Position 8 – alphabetic values A thru Z (minus S, L, O, I, B, Z)
        Position 9 – alphabetic values A thru Z (minus S, L, O, I, B, Z)
        Position 10 – numeric values 0 thru 9
        Position 11 – numeric values 0 thru 9
        */
        // https://www.cms.gov/Medicare/New-Medicare-Card/Understanding-the-MBI-with-Format.pdf
        public static string MedicareBeneficiaryIdentifier()
        {
            return string.Join("", new[]
            {
                Numeric19.ElementAt(RandomNumber.Next(0, Numeric19.Length)),
                Alpha.ElementAt(RandomNumber.Next(0, Alpha.Length)),
                AlphaNumeric.ElementAt(RandomNumber.Next(0, AlphaNumeric.Length)),
                Numeric.ElementAt(RandomNumber.Next(0, Numeric.Length)),
                Alpha.ElementAt(RandomNumber.Next(0, Alpha.Length)),
                AlphaNumeric.ElementAt(RandomNumber.Next(0, AlphaNumeric.Length)),
                Numeric.ElementAt(RandomNumber.Next(0, Numeric.Length)),
                Alpha.ElementAt(RandomNumber.Next(0, Alpha.Length)),
                Alpha.ElementAt(RandomNumber.Next(0, Alpha.Length)),
                Numeric.ElementAt(RandomNumber.Next(0, Numeric.Length)),
                Numeric.ElementAt(RandomNumber.Next(0, Numeric.Length))
            });
        }

        public static string SocialSecurityNumber(bool dashFormat = true)
        {
            var area = RandomNumber.Next(1, 650);

            /*
            Odd numbers, 01 to 09
            Even numbers, 10 to 98
            Even numbers, 02 to 08
            Odd numbers, 11 to 99
            */

            var groups = Enumerable.Range(1, 10).Where(z => z % 2 == 1).ToList();
            groups.AddRange(Enumerable.Range(10, 99).Where(z => z % 2 == 0));
            groups.AddRange(Enumerable.Range(2, 9).Where(z => z % 2 == 0));
            groups.AddRange(Enumerable.Range(11, 99).Where(z => z % 2 == 1));
            var group = groups.ElementAt(RandomNumber.Next(0, groups.Count));
            var serial = RandomNumber.Next(1, 9999);
            var ssn = $"{area:000}-{group:00}-{serial:0000}";
            return !dashFormat ? ssn.Replace("-", "") : ssn;
        }

        private static readonly string[] Alphabet = "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z".Split(' ');

        public static string UkNationalInsuranceNumber()
        {
            var niNumber = new StringBuilder();
            niNumber.Append(Alphabet.Random());
            niNumber.Append(Alphabet.Random());
            for (var i = 0; i < 6; i++) niNumber.Append(RandomNumber.Next(0, 9));
            niNumber.Append(Alphabet.Random());
            return niNumber.ToString();
        }

        public static string UkPassportNumber()
        {
            return NineDigitPassportNumber();
        }

        public static string UsPassportNumber()
        {
            return NineDigitPassportNumber();
        }

        private static string NineDigitPassportNumber()
        {
            var passportNumber = new StringBuilder();
            for (var i = 0; i < 9; i++)
            {
                passportNumber.Append(Numeric.ElementAt(RandomNumber.Next(0, Numeric.Length)));
            }

            return passportNumber.ToString();
        }
    }
}
