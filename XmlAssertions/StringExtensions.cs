using System;

namespace XmlAssertions
{
    public static class StringExtensions
    {
        public static bool EqualsCaseInsensitive(this string me, string other)
        {
            var result = StringComparer.InvariantCultureIgnoreCase.Equals(me, other);
            return result;
        }
    }
}