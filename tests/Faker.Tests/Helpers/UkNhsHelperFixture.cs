using NUnit.Framework;

namespace Faker.Tests.Extensions
{
    [TestFixture]
    public class UkNhsHelperFixture
    {
        [Test]
        public void CheckSum_Should_Be_Nine()
        {
            var checkSum = UkNhsHelper.CalculateCheckSum("943476591");
            Assert.AreEqual("9", checkSum);
        }

        [Test]
        public void CheckSum_Should_Be_Zero()
        {
            var checkSum = UkNhsHelper.CalculateCheckSum("549610020");
            Assert.AreNotEqual("11", checkSum);
            Assert.AreEqual("0", checkSum);
        }
    }
}