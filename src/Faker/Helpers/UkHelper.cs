using System;

namespace Faker
{
    public static class UkHelper
    {
        public static string CalculateNhsNumberChecksum(string nineDigitNumber)
        {
            if (string.IsNullOrWhiteSpace(nineDigitNumber) || nineDigitNumber.Length != 9)
                throw new Exception("Invalid partial NHS number!");

            var result = 0;
            for (var i = 0; i < 9; i++)
                result += Convert.ToInt16(nineDigitNumber[i]
                    .ToString()) * (11 - (i + 1));

            result = 11 - result % 11;
            if (result == 11)
                result = 0;

            return result.ToString();
        }
    }
}