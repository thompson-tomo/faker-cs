using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Faker
{
    public static class Finance
    {
        private const string SecurityCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const string TickerCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly CultureInfo UsCulture = new CultureInfo("en-US");

        public static string Isin()
        {
            var securityIdentifier = $"{Country.TwoLetterCode()}{SecurityIdentifier()}";
            var checkSum = CalculateChecksum(securityIdentifier);

            return $"{securityIdentifier}{checkSum}";
        }

        public static string Ticker()
        {
            var length = RandomNumber.Next(2, 4);

            return new string(Enumerable.Repeat(TickerCharacters, length)
                .Select(s => s[RandomNumber.Next(s.Length - 1)]).ToArray());
        }

        public static DateTime Maturity(int minimumMaturityInMonths = 6, int maximumMaturityInMonths = 180)
        {
            var months = RandomNumber.Next(minimumMaturityInMonths, maximumMaturityInMonths);
            var days = RandomNumber.Next(1, 28);
            var date = DateTime.Now.Date.AddMonths(months).AddDays(days);

            return date;
        }

        public static decimal Coupon()
        {
            var minor = RandomNumber.Next(0, 1000);
            var major = RandomNumber.Next(0, 12) * 1000;

            return decimal.Round(Convert.ToDecimal(major + minor) / 1000, 3);
        }

        private static string SecurityIdentifier()
        {
            return new string(Enumerable.Repeat(SecurityCharacters, 9)
                .Select(s => s[RandomNumber.Next(s.Length - 1)]).ToArray());
        }

        private static int CalculateChecksum(IEnumerable<char> codeWithoutChecksum, bool reverseLuhn = false,
            bool allowSymbols = false)
        {
            return reverseLuhn
                ? codeWithoutChecksum
                    .Select((c, i) => c.OrdinalPosition(allowSymbols).ConditionalMultiplyByTwo(i.IsOdd()).SumDigits())
                    .Sum()
                    .TensComplement()
                : codeWithoutChecksum
                    .ToArray()
                    .ToDigits(allowSymbols)
                    .Select((d, i) => d.ConditionalMultiplyByTwo(i.IsEven()).SumDigits())
                    .Sum()
                    .TensComplement();
        }

        private static int OrdinalPosition(this char c, bool allowSymbols = false)
        {
            if (char.IsLower(c))
                return char.ToUpper(c) - 'A' + 10;

            if (char.IsUpper(c))
                return c - 'A' + 10;

            if (char.IsDigit(c))
                return c.ToInt();

            if (allowSymbols)
                switch (c)
                {
                    case '*':
                        return 36;
                    case '@':
                        return 37;
                    case '#':
                        return 38;
                }

            throw new ArgumentOutOfRangeException(nameof(c),
                @"Specified character is not a letter, digit or allowed symbol.");
        }

        private static bool IsEven(this int x)
        {
            return x % 2 == 0;
        }

        private static bool IsOdd(this int x)
        {
            return !IsEven(x);
        }

        private static int ToInt(this char digit)
        {
            if (char.IsDigit(digit))
                return digit - '0';
            throw new ArgumentOutOfRangeException(nameof(digit), @"Specified character is not a digit.");
        }

        private static IEnumerable<int> ToDigits(this char[] s, bool allowSymbols = false)
        {
            var digits = new List<int>();
            for (var i = s.Length - 1; i >= 0; i--)
            {
                var ordinalPosition = s[i].OrdinalPosition(allowSymbols);
                digits.Add(ordinalPosition % 10);
                if (ordinalPosition > 9)
                    digits.Add(ordinalPosition / 10);
            }

            return digits;
        }

        private static int SumDigits(this int value)
        {
            return value / 10 + value % 10;
        }

        private static int ConditionalMultiplyByTwo(this int value, bool condition)
        {
            return condition ? value * 2 : value;
        }

        private static int TensComplement(this int value)
        {
            return (10 - value % 10) % 10;
        }

        public static class Credit
        {
            public static string BondName()
            {
                return BondClass().ToString();
            }

            public static Bond BondClass()
            {
                var ticker = Ticker();
                var coupon = Coupon();
                var maturity = Maturity();

                if (Equals(Thread.CurrentThread.CurrentCulture, UsCulture) ||
                    Equals(Thread.CurrentThread.CurrentUICulture, UsCulture))
                    return new Bond(ticker, coupon, maturity, "MM/dd/yy");

                return new Bond(ticker, coupon, maturity, "dd/MM/yy");
            }

            public struct Bond
            {
                private readonly string _toString;

                public string Ticker { get; }

                public decimal Coupon { get; }

                public DateTime Maturity { get; }

                internal Bond(string ticker, decimal coupon, DateTime maturity, string format)
                {
                    Ticker = ticker;
                    Coupon = coupon;
                    Maturity = maturity;

                    var maturityString = Maturity.ToString(format);
                    _toString = $"{Ticker} {Coupon} {maturityString}";
                }

                public override string ToString()
                {
                    return _toString;
                }
            }
        }
    }
}