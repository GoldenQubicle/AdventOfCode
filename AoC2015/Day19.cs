using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2015
{
    public class Day19 : Solution
    {
        private readonly string molecule;
        public readonly Dictionary<string, List<string>> Mappings = new();
        public Day19(string file) : base(file, "\n")
        {
            foreach(var line in Input)
            {
                Regex.Matches(line, @"(\w+)( => )(\w+)")
                    .Where(m => m.Success)
                    .ForEach(m =>
                    {
                        if(!Mappings.ContainsKey(m.Groups[1].Value))
                            Mappings.Add(m.Groups[1].Value, new List<string> { m.Groups[3].Value });
                        else
                            Mappings[m.Groups[1].Value].Add(m.Groups[3].Value);
                    });
            }

            molecule = Input.Last();
        }

        public Day19(List<string> input) : base(input)
        {
            molecule = Input[0];
        }

        public override async Task<string> SolvePart1( )
        {
            var result = new HashSet<string>();

            foreach(var mapping in Mappings)
            {
                foreach(var map in mapping.Value)
                {
                    Regex.Matches(molecule, $"({mapping.Key})")
                    .Where(m => m.Success)
                    .ForEach(m => result.Add(molecule.ReplaceAt(m.Index, map, mapping.Key.Length)));
                }
            }
            return result.Count.ToString();
        }

        public override async Task<string> SolvePart2( )
        {
            // after a week of frustration with BFS & combinatorial explosions and what not
            // I finally had enough, admitted defeat and yoinked the solution from here; 
            // https://www.reddit.com/r/adventofcode/comments/3xflz8/day_19_solutions/
            // not my proudest moment and still don't quite understand the solution tbh, which 
            // bolster the argument for nicking it since I'd never-ever figured this one out on my own
            // also note this doesn't play nice with testing, hence no unit test for part 2

            var elements = molecule.Count(char.IsUpper);    
            var RnAr = Regex.Matches(molecule, "(Ar)|(Rn)").Count(m => m.Success);
            var Y = Regex.Matches(molecule, "(Y)").Count(m => m.Success);

            var result = elements - RnAr - (2 * Y) - 1;
            
            return result.ToString();
        }
    }
}