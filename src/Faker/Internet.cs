using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Faker.Extensions;

namespace Faker
{
    public static class Internet
    {
        public static string Email()
        {
            return $"{UserName()}@{DomainName()}";
        }

        public static string Email(string name)
        {
            return $"{UserName(name)}@{DomainName()}";
        }

        public static string FreeEmail()
        {
            return $"{UserName()}@{Resources.Internet.FreeMail.Split(Config.Separator).Random()}";
        }

        public static string UserName()
        {
            return _userNameFormats.Random();
        }

        public static string UserName(string name)
        {
            return Regex.Replace(name, @"[^\w]+", new MatchEvaluator(x => new [] { ".", "_" }.Random()), RegexOptions.Compiled).ToLowerInvariant();
        }

        public static string DomainName()
        {
            return $"{DomainWord()}.{DomainSuffix()}";
        }

        public static string DomainWord()
        {
            return Company.Name().Split(' ').First().AlphanumericOnly().ToLowerInvariant();
        }

        public static string DomainSuffix()
        {
            return Resources.Internet.DomainSuffix.Split(Config.Separator).Random();
        }

        private static readonly IEnumerable<Func<string>> _userNameFormats = new List<Func<string>>
        {
            () => Name.First().AlphanumericOnly().ToLowerInvariant(),
            () => $"{Name.First().AlphanumericOnly()}{new[] {".", "_"}.Random()}{Name.Last().AlphanumericOnly()}".ToLowerInvariant()
        };
    }
}