using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Faker.Tests
{
    [TestFixture]
    public class RandomFixture
    {
        [Test]
        public void Should_Generate_Maximum()
        {
            var result = false;
            for (var i = 0; i < 1000000; i++)
            {
                var number = RandomNumber.Next(0, 1);
                if (number == 1)
                {
                    result = true;
                    break;
                }
            }
            
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_Generate_Minimum()
        {
            var result = false;
            for (var i = 0; i < 1000000; i++)
            {
                var number = RandomNumber.Next(0, 1);
                if (number == 0)
                {
                    result = true;
                    break;
                }
            }

            Assert.IsTrue(result);
        }
    }
}