using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Faker.Tests
{
    [TestFixture]
    public class IdentificationFixture
    {
        private static readonly Regex NineDigitRegex = new Regex(@"^[0-9]{9,9}$", RegexOptions.Compiled);
        private static readonly Regex TenDigitRegex = new Regex(@"^[0-9]{10,10}$", RegexOptions.Compiled);

        [Test]
        public void Should_Create_BG_Pin()
        {
            var passport = Identification.BulgarianPin();
            Console.WriteLine($@"BGPIN=[{passport}]");

            Assert.IsTrue(TenDigitRegex.IsMatch(passport));
        }

        [Test]
        public void Should_Create_DOB()
        {
            var now = DateTime.UtcNow.Date;

            var dob = Identification.DateOfBirth();
            Console.WriteLine($@"DateOfBirth=[{dob:d}]");

            Assert.IsTrue(dob < now);
            Assert.IsTrue(dob > now.AddYears(Identification.MaxAgeAllowed * -1));
            Assert.That(dob.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void Should_Create_MBI()
        {
            var mbi = Identification.MedicareBeneficiaryIdentifier();
            Console.WriteLine($@"MedicareBeneficiaryIdentifier=[{mbi}]");

            Assert.IsTrue(Regex.IsMatch(mbi,
                @"\b[1-9][AC-HJKMNP-RT-Yac-hjkmnp-rt-y][AC-HJKMNP-RT-Yac-hjkmnp-rt-y0-9][0-9]-?[AC-HJKMNP-RT-Yac-hjkmnp-rt-y][AC-HJKMNP-RT-Yac-hjkmnp-rt-y0-9][0-9]-?[AC-HJKMNP-RT-Yac-hjkmnp-rt-y]{2}\d{2}\b"));
        }

        [Test]
        public void Should_Create_SSN()
        {
            var ssn = new
            {
                WithDashes = Identification.SocialSecurityNumber(),
                Without = Identification.SocialSecurityNumber(false)
            };

            Console.WriteLine($@"SocialSecurityNumber=[{ssn.WithDashes}]");
            Assert.IsTrue(Regex.IsMatch(ssn.WithDashes, @"\d{3}-\d{2}-\d{4}"));
            Console.WriteLine($@"SocialSecurityNumber=[{ssn.Without}]");
            Assert.IsTrue(Regex.IsMatch(ssn.Without, @"\d{9}"));
        }

        [Test]
        public void Should_Create_UK_PassportNumber()
        {
            var passport = Identification.UkPassportNumber();
            Console.WriteLine($@"PassportNumber=[{passport}]");

            Assert.IsTrue(NineDigitRegex.IsMatch(passport));
        }

        [Test]
        public void Should_Create_UkNationalInsuranceNumber()
        {
            var nin = Identification.UkNationalInsuranceNumber();
            Console.WriteLine($@"UKNationalInsuranceNumber=[{nin}]");

            Assert.IsTrue(Regex.IsMatch(nin, @"^\s*[a-zA-Z]{2}(?:\s*\d\s*){6}[a-zA-Z]?\s*$"));
        }

        [Test]
        public void Should_Create_US_PassportNumber()
        {
            var passport = Identification.UsPassportNumber();
            Console.WriteLine($@"PassportNumber=[{passport}]");

            Assert.IsTrue(NineDigitRegex.IsMatch(passport));
        }
    }
}