using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faker.Extensions;

namespace Faker
{
    public static class Identification
    {
        // https://en.wikipedia.org/wiki/Jeanne_Calment
        public const int MaxAgeAllowed = 122;

        private static readonly Func<int, bool> IsEvenFn = z => z % 2 == 0;

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
            return string.Join("", Resources.Identification.MbiNumeric.Split(Config.Separator)
                    .Random(),
                Resources.Identification.MbiAlphabet.Split(Config.Separator)
                    .Random(),
                Resources.Identification.Mbi.Split(Config.Separator)
                    .Random(),
                Resources.Identification.Numeric.Split(Config.Separator)
                    .Random(),
                Resources.Identification.MbiAlphabet.Split(Config.Separator)
                    .Random(),
                Resources.Identification.Mbi.Split(Config.Separator)
                    .Random(),
                Resources.Identification.Numeric.Split(Config.Separator)
                    .Random(),
                Resources.Identification.MbiAlphabet.Split(Config.Separator)
                    .Random(),
                Resources.Identification.MbiAlphabet.Split(Config.Separator)
                    .Random(),
                Resources.Identification.Numeric.Split(Config.Separator)
                    .Random(),
                Resources.Identification.Numeric.Split(Config.Separator)
                    .Random());
        }

        /// <summary>
        ///     Return a list of integers from min to max value inclusive that satisfy check condition
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max">Inclusive</param>
        /// <param name="conditionFn">All values are valid if not specified</param>
        /// <returns></returns>
        private static IEnumerable<int> Range(int min, int max, Func<int, bool> conditionFn)
        {
            var range = new List<int>();
            conditionFn ??= z => true;
            if (min > max) return range;
            var current = min;
            do
            {
                if (conditionFn(current)) range.Add(current);
                current++;
            } while (current <= max);

            return range;
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
            var groups = Range(1, 9, z => !IsEvenFn(z))
                .ToList();
            groups.AddRange(Range(10, 98, IsEvenFn));
            groups.AddRange(Range(2, 8, IsEvenFn));
            groups.AddRange(Range(11, 99, z => !IsEvenFn(z)));

            var group = groups.ElementAt(RandomNumber.Next(0, groups.Count - 1));
            var serial = RandomNumber.Next(1, 9999);
            var ssn = $"{area:000}-{group:00}-{serial:0000}";
            return !dashFormat ? ssn.Replace("-", "") : ssn;
        }

        public static DateTime DateOfBirth()
        {
            var rnd = new Random((int) DateTime.UtcNow.Ticks);
            var first = rnd.NextDouble();
            var second = rnd.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(first)) * Math.Sin(2.0 * Math.PI * second);
            const double average = MaxAgeAllowed * .5;
            var randNormal = average + 46 * randStdNormal;
            var yearsToSubtract = (int) Math.Abs(randNormal);
            if (yearsToSubtract > MaxAgeAllowed) yearsToSubtract = (int) average;
            var now = DateTime.UtcNow;
            var randomDay = RandomNumber.Next(1, 365); // Skip leap years for simplicity
            if (yearsToSubtract < 1) randomDay = RandomNumber.Next(1, now.DayOfYear);
            var gaussianDate = now.AddYears(yearsToSubtract * -1)
                .AddDays(randomDay * -1);
            return gaussianDate.Date;
        }

        public static string UkNationalInsuranceNumber(bool formatted = false)
        {
            var notAllowedPrefixes = Resources.Identification.UkNationalInsuranceNotAllowedPrefix
                .Split(Config.Separator)
                .ToArray();

            string prefix;
            while (true)
            {
                prefix =
                    $"{Resources.Identification.UkNationalInsuranceFirstDigit.Split(Config.Separator).Random()}{Resources.Identification.UkNationalInsuranceSecondDigit.Split(Config.Separator).Random()}";

                if (!notAllowedPrefixes.Contains(prefix))
                    break;
            }

            var number = string.Empty;
            for (var i = 0; i < 6; i++)
                number += RandomNumber.Next(0, 9);

            var suffix = $"{Resources.Identification.UkNationalInsuranceSuffix.Split(Config.Separator).Random()}";
            return formatted
                ? $"{prefix} {number.Substring(0, 2)} {number.Substring(2, 2)} {number.Substring(4, 2)} {suffix}"
                : $"{prefix}{number}{suffix}";
        }

        public static string UkPassportNumber()
        {
            return NineDigitNumber();
        }

        public static string UsPassportNumber()
        {
            return NineDigitNumber();
        }

        public static string UkNhsNumber(bool formatted = false)
        {
            string nineDigitNumber;
            string checksum;
            while (true)
            {
                nineDigitNumber = NineDigitNumber();
                checksum = UkHelper.CalculateNhsNumberChecksum(nineDigitNumber);
                if (checksum != "10")
                    break;
            }

            return formatted
                ? $"{nineDigitNumber.Substring(0, 3)} {nineDigitNumber.Substring(3, 3)} {nineDigitNumber.Substring(6, 3)}{checksum}"
                : $"{nineDigitNumber}{checksum}";
        }

        private static string NineDigitNumber()
        {
            var nineDigitNumber = new StringBuilder();

            for (var i = 0; i < 9; i++)
                nineDigitNumber.Append(Resources.Identification.Numeric.Split(Config.Separator)
                    .Random());

            return nineDigitNumber.ToString();
        }

        /// <summary>
        ///     Generates random Bulgarian Personal Identification Number / EGN
        /// </summary>
        /// <returns></returns>
        public static string BulgarianPin()
        {
            return BgPin();
        }

        /// <summary>
        ///     Generates random Bulgarian Personal Identification Number / EGN
        /// </summary>
        /// <returns></returns>
        private static string BgPin()
        {
            var bgRegionsRangeMin = 0;
            var bgRegionsRangeMax = 999;

            var r = new Random();
            var PIN = new StringBuilder();
            var weightNumbers = new List<int> {2, 4, 8, 5, 10, 9, 7, 3, 6};
            //Get Random Year
            var yearDigit = r.Next(1800, 2100);
            //Get Random Month 
            var monthDigit = r.Next(1, 12);
            //Get Random Birth Region 
            var regionDigit = r.Next(bgRegionsRangeMin, bgRegionsRangeMax);

            //Add year to th PIN 
            //Gets Only the last two digits of the year
            PIN.Append(yearDigit.ToString()
                .Substring(2, 2));

            //Maximum number of days in every month
            var monthsData = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(1, 31),
                new KeyValuePair<int, int>(2,
                    28), // I don`t think is that important to add support for 29 of February
                new KeyValuePair<int, int>(3, 31),
                new KeyValuePair<int, int>(4, 30),
                new KeyValuePair<int, int>(5, 31),
                new KeyValuePair<int, int>(6, 30),
                new KeyValuePair<int, int>(7, 31),
                new KeyValuePair<int, int>(8, 31),
                new KeyValuePair<int, int>(9, 30),
                new KeyValuePair<int, int>(10, 31),
                new KeyValuePair<int, int>(11, 30),
                new KeyValuePair<int, int>(12, 31)
            };
            //Get Random day in current month
            var dayDigit = r.Next(1, monthsData.Where(x => x.Key == monthDigit)
                .Select(y => y.Value)
                .FirstOrDefault());

            //This is rule for centuries
            if (yearDigit < 1900)
                monthDigit = 20 + monthDigit;
            else if (yearDigit >= 2000) monthDigit = 40 + monthDigit;

            //Add month to the PIN
            PIN.Append($"{monthDigit:00}");
            //Add day to the PIN
            PIN.Append($"{dayDigit:00}");
            //Add birth Region to the PIN
            PIN.Append($"{regionDigit:000}");

            //Calculate weights
            var weigthSums = 0;
            for (var i = 0; i < PIN.Length; i++)
            {
                var currentDigit = 0;
                currentDigit = int.Parse(PIN.ToString()
                    .Substring(i, 1));
                weigthSums += currentDigit * weightNumbers[i];
            }

            var controlNumber = weigthSums % 11;

            //Get the control number
            PIN.Append(controlNumber < 10 ? controlNumber.ToString() : "0");
            return PIN.ToString();
        }
    }
}