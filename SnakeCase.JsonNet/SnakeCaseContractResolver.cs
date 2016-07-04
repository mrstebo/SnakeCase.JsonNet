using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SnakeCase.JsonNet
{
    public class SnakeCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return GetSnakeCase(propertyName);
        }

        private static string GetSnakeCase(string input)
        {
            return mrsteboGetSnakeCase(input);
        }

        private static string jonniiGetSnakeCase(string input)
        {
            return Regex.Replace(input, "([a-z])([A-Z])", "$1_$2").ToLower();
        }

        private static string jonniiModGetSnakeCase(string input)
        {
            return Regex.Replace(input, "([a-z0-9])([A-Z0-9])", "$1_$2").ToLower();
        }

        // not great
        private static string roryfGetSnakeCase(string input, string separator = "_")
        {

            var parts = new List<string>();
            var currentWord = new StringBuilder();

            foreach (var c in input)
            {
                if (char.IsUpper(c) && currentWord.Length > 0)
                {
                    parts.Add(currentWord.ToString());
                    currentWord.Clear();
                }
                currentWord.Append(char.ToLower(c));
            }

            if (currentWord.Length > 0)
            {
                parts.Add(currentWord.ToString());
            }

            return string.Join(separator, parts.ToArray());
        }


        private static string mrsteboGetSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var buffer = "";

            for (var i = 0; i < input.Length; i++)
            {
                var isLast = (i == input.Length - 1);
                var isSecondFromLast = (i == input.Length - 2);

                var current = input[i];
                var next = !isLast ? input[i + 1] : '\0';
                var afterNext = !isSecondFromLast && !isLast ? input[i + 2] : '\0';

                buffer += char.ToLower(current);

                if (!char.IsDigit(current) && char.IsUpper(next))
                {
                    if (char.IsUpper(current))
                    {
                        if (!isLast && !isSecondFromLast && !char.IsUpper(afterNext))
                            buffer += "_";
                    }
                    else
                        buffer += "_";
                }

                if (!char.IsDigit(current) && char.IsDigit(next))
                    buffer += "_";
                if (char.IsDigit(current) && !char.IsDigit(next) && !isLast)
                    buffer += "_";
            }

            return buffer;
        }
    }
}
