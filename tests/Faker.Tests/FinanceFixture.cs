using NUnit.Framework;

namespace Faker.Tests
{
    [TestFixture]
    public class FinanceFixture
    {
        [Test]
        public void should_return_an_isin()
        {
            var isin = Finance.Isin();

            Assert.That(isin, Is.Not.Empty);
            Assert.That(isin.Length, Is.EqualTo(12));
        }
    }
}