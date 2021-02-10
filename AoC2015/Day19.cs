using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;
using Common.Extensions;
// ReSharper disable StringIndexOfIsCultureSpecific.1

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

        public override string SolvePart1( )
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

        public override string SolvePart2( )
        {
            var steps = 0;
            var queue = new Queue<(string m, int s)>();
            var current = (m: molecule, s: 1);
            queue.Enqueue(current);
            var maps = Mappings.SelectMany(kvp => kvp.Value.Select(v => (from: kvp.Key, to: v))).ToList();
            
            while(!queue.Peek().m.Equals("e"))
            {
                current = queue.Dequeue();

                var longest = maps.Where(m => current.m.Contains(m.to)).ToList();
                var max = longest.Max(m => m.to.Length);
                var picks = longest.Where(m => m.to.Length == max).First();
                
                var replacement = false;
                Console.WriteLine(current.m);

                //foreach (var (from, to) in picks)
                //{
                    var regex = new Regex(picks.to);
                    var next = regex.Replace(current.m, picks.from, 1);
                    queue.Enqueue((next, current.s + 1));
                //}
                

                //foreach(var mapping in Mappings)
                //{
                //    foreach(var map in mapping.Value)
                //    {
                //        if(current.m.Contains(map))
                //        {
                //            var next = current.m.ReplaceAt(current.m.IndexOf(map), mapping.Key, map.Length);
                //            queue.Enqueue((next, current.s + 1));
                //            steps++;
                //            replacement = true;
                //            break;
                //        }
                //    }
                //    if(replacement) break;
                //}
            }

            return current.s.ToString();
        }        
    }
}