using System;
using System.Collections.Generic;
using System.Text;

namespace SharedProject
{
    public static class StringUtilities
    {
        public static string TruncateWithEllipsis(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
    }
}
