using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Faker.Extensions;

[assembly: InternalsVisibleTo("Faker.Tests.Net.5.0")]
namespace Faker
{
    public enum NameFormats
    {
        Standard,
        StandardWithMiddle,
        WithPrefix,
        WithSuffix
    }

    public static class Name
    {
        private static readonly IEnumerable<NameFormats> Formats = new List<NameFormats>
        {
            NameFormats.WithPrefix, NameFormats.WithSuffix, NameFormats.Standard, NameFormats.Standard,
            NameFormats.Standard, NameFormats.Standard, NameFormats.Standard, NameFormats.Standard, NameFormats.Standard
        };

        private static readonly IDictionary<NameFormats, Func<string[]>> FormatMap =
            new Dictionary<NameFormats, Func<string[]>>
            {
                {NameFormats.Standard, () => new[] {First(), Last()}},
                {NameFormats.StandardWithMiddle, () => new[] {First(), Middle(), Last()}},
                {NameFormats.WithPrefix, () => new[] {Prefix(), First(), Last()}},
                {NameFormats.WithSuffix, () => new[] {First(), Last(), Suffix()}}
            };

        /// <summary>
        ///     Create a name using a random format.
        /// </summary>
        public static string FullName()
        {
            return FullName(Formats.ElementAt(RandomNumber.Next(Formats.Count() - 1)));
        }

        /// <summary>
        ///     Create a name using a specified format.
        /// </summary>
        public static string FullName(NameFormats format)
        {
            return string.Join(" ", FormatMap[format].Invoke());
        }

        public static string First()
        {
            return Resources.Name.First.Split(Config.Separator).Random().Trim();
        }

        public static string Middle()
        {
            return Resources.Name.First.Split(Config.Separator).Random().Trim();
        }

        public static string Last()
        {
            return Resources.Name.Last.Split(Config.Separator).Random().Trim();
        }

        public static string Prefix()
        {
            return Resources.Name.Prefix.Split(Config.Separator).Random();
        }

        public static string Suffix()
        {
            return Resources.Name.Suffix.Split(Config.Separator).Random();
        }
    }
}