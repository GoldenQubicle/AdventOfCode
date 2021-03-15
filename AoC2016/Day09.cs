using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;

namespace AoC2016
{
    public class Day09 : Solution
    {
        public Day09(string file) : base(file) { }

        public Day09(List<string> input) : base(input) { }

        public override string SolvePart1( )
        {
            var markers = Input
                .Select(line => Regex.Matches(line, @"(?<marker>\((?<take>\d+)x(?<repeat>\d+)\))"))
                .SelectMany(m => m.Where(g => g.Groups["marker"].Success))
                .ToDictionary(m => m.Groups["marker"].Index,
                    m => (take: int.Parse(m.Groups["take"].Value), repeat: int.Parse(m.Groups["repeat"].Value), length: m.Length));

            var counts = Input.Select(line =>
            {
                var idx = 0;
                var count = 0;
                while (idx < line.Length)
                {
                    if (line[idx] == '(')
                    {
                        count += (markers[idx].take * markers[idx].repeat);
                        idx += markers[idx].length + markers[idx].take;
                    }
                    else
                    {
                        idx++;
                        count++;
                    }
                }

                return count;

            });

            return counts.Sum().ToString();
        }

        public override string SolvePart2( ) => null;

        public int Decompress(string input)
        {


            return 0;
        }
    }
}