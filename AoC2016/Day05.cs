using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2016
{
    public class Day05 : Solution
    {
        public Day05(string file) : base(file) { }

        public Day05(List<string> input) : base(input) { }

        public override string SolvePart1( )
        {
            var password = string.Empty;
            var count = 0;
            using var md5 = MD5.Create();
            while(password.Length < 8)
            {
                var bytes = Encoding.ASCII.GetBytes(Input[0] + count);
                var hashBytes = md5.ComputeHash(bytes);
                var sb = new StringBuilder();
                foreach(var h in hashBytes)
                {
                    sb.Append(h.ToString("X2"));
                }

                var hash = sb.ToString();
                if(hash.StartsWith("00000"))
                    password += hash[5];

                count++;
            }

            return password;
        }

        public override string SolvePart2( )
        {
            var password = "________";
            var count = 0;
            using var md5 = MD5.Create();
            while(password.Contains("_"))
            {
                var bytes = Encoding.ASCII.GetBytes(Input[0] + count);
                var hashBytes = md5.ComputeHash(bytes);
                var sb = new StringBuilder();
                foreach(var h in hashBytes)
                {
                    sb.Append(h.ToString("X2"));
                }

                var hash = sb.ToString();
                if(hash.StartsWith("00000") && char.IsDigit(hash[5]))
                {
                    var idx = int.Parse(hash[5].ToString());

                    if (idx < 8 && password[idx] == '_')
                        password = password.ReplaceAt(idx, hash[6]);
                }
                count++;
            }

            return password;
        }
    }
}