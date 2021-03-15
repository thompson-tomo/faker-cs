using NUnit.Framework;

namespace Faker.Tests.Extensions
{
    [TestFixture]
    public class UkNhsHelperFixture
    {
        [Test]
        public void CheckSum_Should_be_Nine()
        {
            var checkSum = UkNhsHelper.CalculateCheckSum("943476591");
            Assert.AreEqual("9", checkSum);
        }
    }
}