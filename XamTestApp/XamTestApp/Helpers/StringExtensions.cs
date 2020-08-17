using System;
using System.Linq;

namespace XamTestApp.Helpers
{
    internal static class StringExtensions
    {
        public static bool FoundInAny(this string search, params string[] searchSource)
        {
            if (searchSource == null || string.IsNullOrEmpty(search))
            {
                return false;
            }

            var searchCharArray = search.ToCharArray();

            searchCharArray = Array.FindAll(searchCharArray, (p => char.IsLetterOrDigit(p) || char.IsWhiteSpace(p)));
            var searchNormalized = new string(searchCharArray);

            var str = string.Join(" ", searchSource);
            return searchNormalized.Split(' ').All(a => str.IndexOf(a, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
