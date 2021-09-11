using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Faker.Tests
{
    [TestFixture]
    public class RandomNumberFixture
    {
        [TestCase(1, "Coin flip")] // 0 ... 1 Coin flip
        [TestCase(6, "6 sided die")] // 0 .. 6
        [TestCase(9, "Random single digit")] // 0 ... 9
        [TestCase(20, "D20")] // 0 ... 20  The signature dice of the dungeons and dragons
        public void Should_Generate_All_Positive_Values_From_Zero(int max, string testName)
        {
            if (max < 0)
                throw new ArgumentException(@"Value must be greater than zero!", nameof(max));

            Console.WriteLine($@"RandomNumber.Next [{testName}]");

            var results = new Dictionary<int, int>();
            do
            {
                var value = RandomNumber.Next(0, max);

                if (value < 0)
                    throw new Exception($"Value is less than zero, value=[{value}]");

                results[value] = value;

                Console.WriteLine($@"RandomNumber.Next(0,{max})=[{value}]");
            } while (results.Count != max - 0);
        }

        [TestCase(-1000, "Negative 1000 Numbers")]
        public void Should_Generate_All_Negative_Values_To_Zero(int min, string testName)
        {
            if (min > 0)
                throw new ArgumentException(@"Value must be less than zero!", nameof(min));

            var results = new Dictionary<int, int>();
            do
            {
                var value = RandomNumber.Next(min,0);

                if (value > 0)
                    throw new Exception($"Value is greater than zero, value=[{value}]");

                results[value] = value;

                Console.WriteLine($@"RandomNumber.Next({min}, 0)=[{value}]");
            } while (results.Count != min * -1);
        }

        [Test]
        public void Should_Create_Zero()
        {
            var zero = RandomNumber.Next(0, 0);
            Console.WriteLine($@"RandomNumber.Next(0,0)=[{zero}]");

            Assert.IsTrue(zero == 0);
        }

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

        [Test]
        public void Should_Generate_To_Int32_Max_Value()
        {
            var results = new Dictionary<int, int>();
            var min = int.MaxValue - 1000;
            var max = int.MaxValue;

            do
            {
                var value = RandomNumber.Next(min, max);

                if (value < min)
                    throw new Exception($"Value is less than min, value=[{value}], min=[{min}]");

                results[value] = value;

                Console.WriteLine($@"RandomNumber.Next({min},{max})=[{value}]");
            } while (results.Count != 1000);

            Assert.That(results.ContainsKey(min));
            Assert.That(results.ContainsKey(max));
        }

        [Test]
        public void Should_Generate_To_Int64_Max_Value()
        {
            var results = new Dictionary<long, long>();
            var min = long.MaxValue - 1000;
            var max = long.MaxValue;

            do
            {
                var value = RandomNumber.Next(min, max);

                if (value < min)
                    throw new Exception($"Value is less than min, value=[{value}], min=[{min}]");

                results[value] = value;

                Console.WriteLine($@"RandomNumber.Next({min},{max})=[{value}]");
            } while (results.Count != 1000);

            Assert.That(results.ContainsKey(min));
            Assert.That(results.ContainsKey(max));
        }

        [Test]
        public void Should_Generate_To_Int32_Min_Value()
        {
            var results = new Dictionary<int, int>();
            var min = int.MinValue;
            var max = min + 1000;

            do
            {
                var value = RandomNumber.Next(min, max);

                if (value > max)
                    throw new Exception($"Value is greater than max, value=[{value}], max=[{max}]");

                results[value] = value;

                Console.WriteLine($@"RandomNumber.Next({min},{max})=[{value}]");
            } while (results.Count != 1000);

            Assert.That(results.ContainsKey(min));
            Assert.That(results.ContainsKey(max));
        }

        [Test]
        public void Should_Generate_To_Int64_Min_Value()
        {
            var results = new Dictionary<long, long>();
            var min = long.MinValue;
            var max = min + 1000;

            do
            {
                var value = RandomNumber.Next(min, max);

                if (value > max)
                    throw new Exception($"Value is greater than max, value=[{value}], max=[{max}]");

                results[value] = value;

                Console.WriteLine($@"RandomNumber.Next({min},{max})=[{value}]");
            } while (results.Count != 1000);

            Assert.That(results.ContainsKey(min));
            Assert.That(results.ContainsKey(max));
        }

        [Test]
        public void Should_Generate_Values_Greater_Than_Zero()
        {
            var result = true;
            for (var i = 0; i < 1000; i++)
            {
                var number = RandomNumber.Next();
                Console.WriteLine(number);
                if (number < 0)
                {
                    result = false;
                    break;
                }
            }

            Assert.IsTrue(result);
        }
    }
}