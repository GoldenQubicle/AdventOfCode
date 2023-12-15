using System;
using System.Collections.Generic;
using System.Text;
using Common;
using System.Security.Cryptography;

namespace AoC2015
{
    public class Day04 : Solution
    {
        public Day04(string file) : base(file) { }

        public Day04(List<string> input) : base(input) { }

        public override string SolvePart1( ) => SolveHash("00000");

        public override string SolvePart2( ) => SolveHash("000000");

        private string SolveHash(string lookingFor )
        {
            var result = string.Empty;
            var count = 0;
            while ( !result.StartsWith(lookingFor) )
            {
                count++;
                result = Maths.HashToHexadecimal($"{Input[0]}{count}");
            }
            return count.ToString( );
        }
    }
}