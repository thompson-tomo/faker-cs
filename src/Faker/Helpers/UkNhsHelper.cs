using System;

namespace Faker
{
    public static class UkNhsHelper
    {
        public static string CalculateCheckSum(string nineDigitNumber)
        {
            if (string.IsNullOrWhiteSpace(nineDigitNumber) || nineDigitNumber.Length != 9)
                throw new Exception("Invalid partial NHS number!");

            var result = 0;
            for (var i = 0; i < 9; i++)
            {
                result += Convert.ToInt16(nineDigitNumber[i]
                    .ToString()) * (11 - (i + 1));
                result = 11 - result % 11;
            }

            return result.ToString();
        }
    }
}