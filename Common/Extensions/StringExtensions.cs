﻿namespace Common.Extensions
{
    public static class StringExtensions
    {
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
    }
}
