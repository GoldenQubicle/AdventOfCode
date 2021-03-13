using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;
using NUnit.Framework;

namespace AoC2015
{
    public class Day24 : Solution
    {
        private readonly List<int> packages;
        public Day24(string file) : base(file, "\n") => packages = Input.Select(int.Parse).ToList();


        public Day24(List<string> input) : base(input) { }

        public override string SolvePart1( )
        {
            var targetWeight = packages.Sum() / 3;
            
            var indices = Combinator.Generate(new List<int> {0, 1}, new CombinatorOptions
            {
                Length = packages.Count
            });

    
            var sets = new HashSet<List<int>>();

            indices.ForEach(c =>
            {
                var p = c.Select((v, i) => v == 1 ? packages[i] : 0).ToList();
                if (p.Sum() == targetWeight)
                {
                    sets.Add(p.Where(v => v != 0).ToList());
                }
            });

            var result = sets.GroupBy(p => p.Count).OrderBy(g => g.Key).First();
            var quantumEntanglement = result.Count() > 1
                ? result.GroupBy(g => g.Aggregate(1, (sum, v) => sum * v)).OrderBy(g => g.Key).First().Key
                : result.First().Aggregate(1, (sum, v) => sum * v);


            return quantumEntanglement.ToString();
        }

       

        public override string SolvePart2( ) => null;
    }
}