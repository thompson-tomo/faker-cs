using NUnit.Framework;

namespace Faker.Tests
{
    [TestFixture]
    public class CountryFixture
    {
        [Test]
        public void should_return_two_letter_country_code()
        {
            var currency = Country.TwoLetterCode();

            Assert.That(currency.Length, Is.EqualTo(2));
        }

        [Test]
        public void should_return_country_name()
        {
            var currency = Country.Name();

            Assert.That(currency, Is.Not.Empty);
        }
    }
}