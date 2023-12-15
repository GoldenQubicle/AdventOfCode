using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace AoC2016
{
    public class Day14 : Solution
    {
        private readonly List<int> validKeyIndices = new();
        private readonly Dictionary<int, string> hashes = new();
        public string Salt { get; set; }

        public Day14(string file) : base(file) { }
        public Day14(List<string> input) : base(input) { }

        public override string SolvePart1( ) => GetLastValidKeyIndex();

        public override string SolvePart2( ) => GetLastValidKeyIndex(isPart2: true);

        private string GetLastValidKeyIndex(bool isPart2 = false)
        {
            Salt = Input[0];
            var idx = 0;

            while(validKeyIndices.Count < 64)
            {
                var hash = hashes.ContainsKey(idx) ? hashes[idx] : ComputeHash(idx, isPart2);
                var triplet = Regex.Match(hash, "(\\d|\\w)\\1\\1");

                if(triplet.Success)
                {
                    foreach(var i in Enumerable.Range(idx + 1, 1000).ToList())
                    {
                        if(!hashes.ContainsKey(i)) hashes[i] = ComputeHash(i, isPart2);
                        var match = triplet.Value[..1];
                        var quintuple = Regex.Match(hashes[i], @$"({match})\1\1\1\1");
                        if(quintuple.Success) validKeyIndices.Add(idx);
                    }
                }
                idx++;
            }
            return validKeyIndices.Last().ToString();
        }

        private string ComputeHash(int idx, bool isPart2)
        {
            var hash = Maths.HashToHexadecimal(Salt + idx).ToLowerInvariant();
            if(isPart2)
            {
                for(var i = 0 ; i < 2016 ; i++)
                {
                    hash = Maths.HashToHexadecimal(hash).ToLowerInvariant();
                }
            }
            return hash;
        }
    }
}