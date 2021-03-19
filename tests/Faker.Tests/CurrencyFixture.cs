using System;
using NUnit.Framework;

namespace Faker.Tests
{
    [TestFixture]
    public class CurrencyFixture
    {
        [Test]
        public void should_return_three_letter_currency_code()
        {
            for (var i = 0; i < 20; i++)
            {
                var currency = Currency.ThreeLetterCode();
                Console.WriteLine($@"Iteration=[{i}], ThreeLetterCode=[{currency}]");

                Assert.That(currency.Length, Is.EqualTo(3));
            }
        }

        [Test]
        public void should_return_currency_name()
        {
            for (var i = 0; i < 20; i++)
            {
                var currency = Currency.Name();
                Console.WriteLine($@"Iteration=[{i}], Name=[{currency}]");

                Assert.That(currency, Is.Not.Empty);
            }
        }
    }
}