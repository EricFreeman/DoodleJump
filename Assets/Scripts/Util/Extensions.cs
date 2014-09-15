using System;
using System.Collections.Generic;

namespace Assets.Scripts.Util
{
    public static class Extensions
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null) return;
            foreach (var e in enumerable)
                action(e);
        }

        public static string ToFormat(this string s, params object[] parmaters)
        {
            return string.Format(s, parmaters);
        }
    }
}
