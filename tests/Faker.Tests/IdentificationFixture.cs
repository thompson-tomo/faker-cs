using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Faker.Tests
{
    public class IdentificationFixture
    {
        [Test]
        public void Should_Create_DateOfBirth()
        {
            var dob = Identification.DateOfBirth();
            Console.WriteLine($@"DateOfBirth=[{dob:O}]");
            Assert.IsTrue(dob < DateTime.UtcNow);
            Assert.IsTrue(dob > DateTime.UtcNow.AddYears(Identification.MaxAgeAllowed * -1));
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
        public void Should_Create_MBI()
        {
            var mbi = Identification.MedicareBeneficiaryIdentifier();
            Console.WriteLine($@"MedicareBeneficiaryIdentifier=[{mbi}]");

            Assert.IsTrue(Regex.IsMatch(mbi, @"\b[1-9][AC-HJKMNP-RT-Yac-hjkmnp-rt-y][AC-HJKMNP-RT-Yac-hjkmnp-rt-y0-9][0-9]-?[AC-HJKMNP-RT-Yac-hjkmnp-rt-y][AC-HJKMNP-RT-Yac-hjkmnp-rt-y0-9][0-9]-?[AC-HJKMNP-RT-Yac-hjkmnp-rt-y]{2}\d{2}\b"));
        }

        [Test]
        public void Should_Create_UKNationalInsuranceNumber()
        {
            var nin = Identification.UKNationalInsuranceNumber();
            Console.WriteLine($@"UKNationalInsuranceNumber=[{nin}]");

            Assert.IsTrue(Regex.IsMatch(nin, @"^\s*[a-zA-Z]{2}(?:\s*\d\s*){6}[a-zA-Z]?\s*$"));
        }
    }
}