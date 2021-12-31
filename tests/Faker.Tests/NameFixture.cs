using System;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
#pragma warning disable CS8618

namespace Faker.Tests
{
    [TestFixture]
    public class NameFixture
    {
        [OneTimeSetUp]
        public static void SetUp()
        {
            var prefixes = string.Join("|", Resources.Name.Prefix.Split(Config.Separator)
                .Select(x => $"(({x} )?)"));

            var suffixes = string.Join("|", Resources.Name.Suffix.Split(Config.Separator)
                .Select(x => $"(( {x})?)"));

            var fullNameRegExString = $@"({prefixes})(\w+?) (\w\'?)(\w+?\-?\w+)({suffixes})";
            var fullNameWithMiddleRegExString = $@"({prefixes})(\w+?) (\w\'?)(\w+\'?) (\w\'?)(\w+?\-?\w+)({suffixes})";

            _fullNameRegex = new Regex(fullNameRegExString, RegexOptions.Compiled);
            _fullNameWithMiddleRegex = new Regex(fullNameWithMiddleRegExString, RegexOptions.Compiled);
        }

        private static Regex _fullNameRegex;
        private static Regex _fullNameWithMiddleRegex;

        [Test]
        public void Should_Get_FullName()
        {
            for (var i = 0; i < 999; i++)
            {
                var name = Name.FullName();
                Console.WriteLine($@"Iteration=[{i}], Name=[{name}]");

                Assert.IsTrue(_fullNameRegex.IsMatch(name));
            }
        }

        [Test]
        public void Should_Get_FullName_With_Standard_Format()
        {
            for (var i = 0; i < 99; i++)
            {
                var name = Name.FullName(NameFormats.Standard);
                Console.WriteLine($@"Iteration=[{i}], FullName_Standard_Format=[{name}]");

                Assert.IsTrue(_fullNameRegex.IsMatch(name));
            }
        }

        [Test]
        public void Should_Get_FullName_With_Standard_With_Middle_Format()
        {
            for (var i = 0; i < 99; i++)
            {
                var name = Name.FullName(NameFormats.StandardWithMiddle);
                Console.WriteLine($@"Iteration=[{i}], FullName_Middle_Format=[{name}]");

                Assert.IsTrue(_fullNameWithMiddleRegex.IsMatch(name));
            }
        }

        [Test]
        public void Should_Get_FullName_With_Standard_With_Middle_Format_And_Prefix()
        {
            for (var i = 0; i < 99; i++)
            {
                var name = Name.FullName(NameFormats.StandardWithMiddleWithPrefix);
                Console.WriteLine($@"Iteration=[{i}], FullName_Middle_Format=[{name}]");

                Assert.IsTrue(_fullNameWithMiddleRegex.IsMatch(name));
            }
        }

        [Test]
        public void Should_Get_Prefix()
        {
            var prefix = Name.Prefix();
            Console.WriteLine($@"Prefix=[{prefix}]");

            Assert.IsTrue(Regex.IsMatch(prefix, @"^[A-Z][a-z]+\.?$"));
        }

        [Test]
        public void Should_Get_Suffix()
        {
            var suffix = Name.Suffix();
            Console.WriteLine($@"Suffix=[{suffix}]");

            Assert.IsTrue(Regex.IsMatch(suffix, @"^[A-Z][A-Za-z]*\.?$"));
        }

        [Test]
        public void Validate_FullName_Regular_Expressions()
        {
            var firstNames = Resources.Name.First.Split(Config.Separator)
                .ToArray();
            var lastNames = Resources.Name.Last.Split(Config.Separator)
                .ToArray();

            var fullNames = firstNames.SelectMany(firstName => lastNames,
                    (firstName, lastName) => $"{firstName.Trim()} {lastName.Trim()}")
                .ToArray();

            foreach (var fullName in fullNames)
            {
                var match = _fullNameRegex.IsMatch(fullName);
                if (!match)
                    Console.WriteLine($@"Name=[{fullName}]");

                Assert.IsTrue(match);
            }
        }
    }
}