using System.Linq;

namespace SeleniumScaffolder
{
    public static class StringHelper
    {
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
    }
}
