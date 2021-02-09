using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2015
{
    public class Day19 : Solution
    {
        private string molecule;
        public Dictionary<string, List<string>> mappings = new();
        public Day19(string file) : base(file, "\n")
        {
            foreach(var line in Input)
            {
                Regex.Matches(line, @"(\w+)( => )(\w+)")
                    .Where(m => m.Success)
                    .ForEach(m =>
                    {
                        if(!mappings.ContainsKey(m.Groups[1].Value))
                            mappings.Add(m.Groups[1].Value, new List<string> { m.Groups[3].Value });
                        else
                            mappings[m.Groups[1].Value].Add(m.Groups[3].Value);
                    });
            }

            molecule = Input.Last();
        }

        public Day19(List<string> input) : base(input)
        {
            molecule = Input[0];
        }

        public override string SolvePart1( )
        {
            var result = new HashSet<string>();

            foreach(var mapping in mappings)
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

        public override string SolvePart2( )
        {
            var steps = 0;
            while(!molecule.Equals("e"))
            {
                var replacement = false;
                Console.WriteLine(molecule);
                foreach(var mapping in mappings)
                {
                    foreach(var map in mapping.Value)
                    {
                        if(molecule.Contains(map))
                        {
                            molecule = molecule.ReplaceAt(molecule.LastIndexOf(map), mapping.Key, map.Length);
                            steps++;
                            replacement = true;
                            break;
                        }
                    }
                    if(replacement) break;
                }
            }

            return steps.ToString();
        }        
    }
}