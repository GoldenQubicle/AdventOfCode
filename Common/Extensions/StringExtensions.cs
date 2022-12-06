using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static List<string> ToList(this string s) => new() { s };

        public static int AsInteger(this string s) => int.Parse(new(s.Where(char.IsDigit).ToArray()));

        public static bool HasInteger(this string s) => s.Any(char.IsDigit);

        public static string ReplaceAt(this string s, int idx, string n, int r)
        {
            s = s.Remove(idx, r);
            s = s.Insert(idx, n);
            return s;
        }

        public static string ReplaceAt(this string s, int idx, string n)
        {
            s = s.Remove(idx, n.Length);
            s = s.Insert(idx, n);
            return s;
        }

        public static string ReplaceAt(this string s, int idx, char c)
        {
            s = s.Remove(idx, 1);
            s = s.Insert(idx, c.ToString());
            return s;
        }

        public static long ToDecimal(this StringBuilder sb) => Convert.ToInt64(sb.ToString(), 2);
    }
}
