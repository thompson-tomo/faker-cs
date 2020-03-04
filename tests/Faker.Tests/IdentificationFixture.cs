using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Faker.Tests
{
    [TestFixture]
    public class IdentificationFixture
    {
        [Test]
        public void Should_Create_SSN()
        {
            var ssn = Identification.SocialSecurityNumber();
            Console.WriteLine($@"SocialSecurityNumber=[{ssn}]");
            
            Assert.IsTrue(Regex.IsMatch(ssn, @"\d{3}-\d{2}-\d{4}"));
        }

        [Test]
        public void Should_Create_MBI()
        {
            var mbi = Identification.MedicareBeneficiaryIdentifier();
            Console.WriteLine($@"MedicareBeneficiaryIdentifier=[{mbi}]");

            Assert.IsTrue(Regex.IsMatch(mbi, @"\b[1-9][AC-HJKMNP-RT-Yac-hjkmnp-rt-y][AC-HJKMNP-RT-Yac-hjkmnp-rt-y0-9][0-9]-?[AC-HJKMNP-RT-Yac-hjkmnp-rt-y][AC-HJKMNP-RT-Yac-hjkmnp-rt-y0-9][0-9]-?[AC-HJKMNP-RT-Yac-hjkmnp-rt-y]{2}\d{2}\b"));
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

            Assert.IsTrue(Regex.IsMatch(passport, @"^[0-9]{9,9}$"));
        }

        [Test]
        public void Should_Create_UK_PassportNumber()
        {
            var passport = Identification.UkPassportNumber();
            Console.WriteLine($@"PassportNumber=[{passport}]");

            Assert.IsTrue(Regex.IsMatch(passport, @"^[0-9]{9,9}$"));
        }
    }
}