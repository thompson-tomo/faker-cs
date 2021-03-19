using System;
using NUnit.Framework;

namespace Faker.Tests
{
    [TestFixture]
    public class CountryFixture
    {
        [Test]
        public void should_return_two_letter_country_code()
        {
            for (var i = 0; i < 20; i++)
            {
                var currency = Country.TwoLetterCode();
                Console.WriteLine($@"Iteration=[{i}], TwoLetterCode=[{currency}]");

                Assert.That(currency.Length, Is.EqualTo(2));
            }
        }

        [Test]
        public void should_return_country_name()
        {
            for (var i = 0; i < 20; i++)
            {
                var name = Country.Name();
                Console.WriteLine($@"Iteration=[{i}], Name=[{name}]");

                Assert.That(name, Is.Not.Empty);
            }
        }
    }
}