using NUnit.Framework;

namespace Faker.Tests.Extensions
{
    [TestFixture]
    public class UkHelperFixture
    {
        [TestCase("526948577", ExpectedResult = "10")]
        [TestCase("987654432", ExpectedResult = "2")]
        [TestCase("549610020", ExpectedResult = "8")]
        [TestCase("756227039", ExpectedResult = "2")]
        [TestCase("943476591", ExpectedResult = "9")]
        public string Nhs_Number_Checksum(string number)
        {
            var checkSum = UkHelper.CalculateNhsNumberChecksum(number);
            return checkSum;
        }
    }
}