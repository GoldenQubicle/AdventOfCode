using System.Collections.Generic;
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
            
            while(password.Length < 8)
            {
                var hash = Md5.HashToHexadecimal(Input[0] + count);
                
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

            while(password.Contains("_"))
            {
                var hash = Md5.HashToHexadecimal(Input[0] + count);
                
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