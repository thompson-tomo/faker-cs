using System;
using System.Collections.Generic;
using Faker.Extensions;

namespace Faker
{
    public static class Address
    {
        private static readonly IEnumerable<Func<string>> CityFormats = new List<Func<string>>
        {
            () => $"{CityPrefix()} {Name.First()}{CitySuffix()}",
            () => $"{CityPrefix()} {Name.First()}",
            () => $"{Name.First()}{CitySuffix()}",
            () => $"{Name.Last()}{CitySuffix()}"
        };

        private static readonly IEnumerable<Func<string[]>> StreetFormats = new List<Func<string[]>>
        {
            () => new[] {Name.Last(), StreetSuffix()},
            () => new[] {Name.First(), StreetSuffix()}
        };

        private static readonly IEnumerable<Func<string>> StreetAddressFormats = new List<Func<string>>
        {
            () => string.Format(Resources.Address.AddressFormat.Split(Config.Separator)
                .Random()
                .Trim(), StreetName())
        };

        public static string Country()
        {
            return Resources.Address.Country.Split(Config.Separator)
                .Random()
                .Trim();
        }

        public static string ZipCode()
        {
            return Resources.Address.ZipCode.Split(Config.Separator)
                .Random()
                .Trim()
                .Numerify();
        }

        public static string UsMilitaryState()
        {
            return Resources.Address.UsMilitaryState.Split(Config.Separator)
                .Random()
                .Trim();
        }

        public static string UsMilitaryStateAbbr()
        {
            return Resources.Address.UsMilitaryStateAbbr.Split(Config.Separator)
                .Random();
        }

        public static string UsTerritory()
        {
            return Resources.Address.UsTerritory.Split(Config.Separator)
                .Random()
                .Trim();
        }

        public static string UsTerritoryStateAbbr()
        {
            return Resources.Address.UsTerritoryAbbr.Split(Config.Separator)
                .Random();
        }

        public static string UsState()
        {
            return Resources.Address.UsState.Split(Config.Separator)
                .Random()
                .Trim();
        }

        public static string UsStateAbbr()
        {
            return Resources.Address.UsStateAbbr.Split(Config.Separator)
                .Random();
        }

        public static string CityPrefix()
        {
            return Resources.Address.CityPrefix.Split(Config.Separator)
                .Random();
        }

        public static string CitySuffix()
        {
            return Resources.Address.CitySuffix.Split(Config.Separator)
                .Random();
        }

        public static string City()
        {
            return CityFormats.Random();
        }

        public static string StreetSuffix()
        {
            return Resources.Address.StreetSuffix.Split(Config.Separator)
                .Random();
        }

        public static string StreetName()
        {
            var result = string.Join(Resources.Address.StreetNameSeparator, StreetFormats.Random());
            return result;
        }

        public static string StreetAddress(bool includeSecondary = false)
        {
            return StreetAddressFormats.Random()
                .Numerify() + (includeSecondary ? " " + SecondaryAddress() : "");
        }

        public static string SecondaryAddress()
        {
            return Resources.Address.SecondaryAddress.Split(Config.Separator)
                .Random()
                .Trim()
                .Numerify();
        }

        public static string UkCounty()
        {
            return Resources.Address.UkCounties.Split(Config.Separator)
                .Random()
                .Trim();
        }

        public static string UkCountry()
        {
            return Resources.Address.UkCountry.Split(Config.Separator)
                .Random()
                .Trim();
        }

        public static string UkPostCode()
        {
            return Resources.Address.UkPostCode.Split(Config.Separator)
                .Random()
                .Trim()
                .Numerify()
                .Letterify();
        }

        public static string CaProvince()
        {
            return Resources.Address.CaProvince.Split(Config.Separator)
                .Random()
                .Trim();
        }
    }
}