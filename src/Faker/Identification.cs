using Faker.Extensions;
using System.Text;

namespace Faker
{
    public static class Identification
    {
        private static readonly string[] _alphabet = "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z".Split(' ');
        public static string UKNationalInsuranceNumber()
        {
            var NINumber = new StringBuilder();
            NINumber.Append(_alphabet.Random());
            NINumber.Append(_alphabet.Random());
            for (var i = 0; i < 6; i++)
            {
                NINumber.Append(RandomNumber.Next(0, 9));
            }
            NINumber.Append(_alphabet.Random());
            return NINumber.ToString();
        }
    }
}
