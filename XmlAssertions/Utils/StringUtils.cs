using System.Collections.Generic;

namespace XmlAssertions.Utils
{
    public static class StringUtils
    {
        public static string Join(this IEnumerable<string> strings)
        {
            return string.Join(", ", strings);
        }
    }
}
