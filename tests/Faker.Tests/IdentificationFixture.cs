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

        private static readonly Regex NhsFormattedDigitRegex =
            new Regex(@"^[0-9]{3}[\s]{1}[0-9]{3}[\s]{1}[0-9]{4}$", RegexOptions.Compiled);

        private static readonly Regex MbiRegex =
            new Regex(
                @"^\b[1-9][AC-HJKMNP-RT-Yac-hjkmnp-rt-y][AC-HJKMNP-RT-Yac-hjkmnp-rt-y0-9][0-9]-?[AC-HJKMNP-RT-Yac-hjkmnp-rt-y][AC-HJKMNP-RT-Yac-hjkmnp-rt-y0-9][0-9]-?[AC-HJKMNP-RT-Yac-hjkmnp-rt-y]{2}\d{2}\b",
                RegexOptions.Compiled);

        private static readonly Regex UkNiNumberRegex =
            new Regex(
                @"^(?!BG|GB|NK|KN|TN|NT|ZZ)[A-CEGHJ-PR-TW-Z][A-CEGHJ-NPR-TW-Z](?:\s*\d{2}){3}\s*[A|B|C|D|F|M|P]$");

        [Test]
        public void Should_Create_BG_Pin()
        {
            var bgPin = Identification.BulgarianPin();
            Console.WriteLine($@"BgPin=[{bgPin}]");

            Assert.IsTrue(TenDigitRegex.IsMatch(bgPin));
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
        public void Should_Create_US_Mbi()
        {
            var mbi = Identification.MedicareBeneficiaryIdentifier();
            Console.WriteLine($@"US_MedicareBeneficiaryIdentifier=[{mbi}]");

            Assert.IsTrue(MbiRegex.IsMatch(mbi));
        }

        [Test]
        public void Should_Create_US_SSN()
        {
            var ssn = new
            {
                WithDashes = Identification.SocialSecurityNumber(),
                Without = Identification.SocialSecurityNumber(false)
            };

            Console.WriteLine($@"US_SocialSecurityNumber=[{ssn.WithDashes}]");
            Assert.IsTrue(Regex.IsMatch(ssn.WithDashes, @"\d{3}-\d{2}-\d{4}"));
            Console.WriteLine($@"US_SocialSecurityNumber=[{ssn.Without}]");
            Assert.IsTrue(Regex.IsMatch(ssn.Without, @"\d{9}"));
        }

        [Test]
        public void Should_Create_UK_Passport_Number()
        {
            var passport = Identification.UkPassportNumber();
            Console.WriteLine($@"UK_PassportNumber=[{passport}]");

            Assert.IsTrue(NineDigitRegex.IsMatch(passport));
        }

        [Test]
        public void Should_Create_UK_NationalInsurance_Number()
        {
            for (var i = 0; i < 99; i++)
            {
                var nin = Identification.UkNationalInsuranceNumber();
                Console.WriteLine($@"Iteration =[{i}], UK_NI_Number=[{nin}]");

                Assert.IsTrue(UkNiNumberRegex.IsMatch(nin));
            }
        }

        [Test]
        public void Should_Create_UK_Formatted_NationalInsurance_Number()
        {
            for (var i = 0; i < 99; i++)
            {
                var nin = Identification.UkNationalInsuranceNumber(true);
                Console.WriteLine($@"Iteration =[{i}], UK_NI_Number =[{nin}]");

                Assert.IsTrue(UkNiNumberRegex.IsMatch(nin));
            }
        }

        [Test]
        public void Should_Create_US_Passport_Number()
        {
            var passport = Identification.UsPassportNumber();
            Console.WriteLine($@"US_Passport_Number=[{passport}]");

            Assert.IsTrue(NineDigitRegex.IsMatch(passport));
        }

        [Test]
        public void Should_Create_UK_Formatted_NHS_Number()
        {
            for (var i = 0; i < 99; i++)
            {
                var nhs = Identification.UkNhsNumber(true);
                Console.WriteLine($@"Iteration =[{i}], UK_NHS_Number=[{nhs}]");

                Assert.IsTrue(NhsFormattedDigitRegex.IsMatch(nhs));
            }
        }

        [Test]
        public void Should_Create_UK_NHS_Number()
        {
            for (var i = 0; i < 99; i++)
            {
                var nhs = Identification.UkNhsNumber();
                Console.WriteLine($@"Iteration =[{i}], UK_NHS_Number=[{nhs}]");

                Assert.IsTrue(TenDigitRegex.IsMatch(nhs));
            }
        }
    }
}