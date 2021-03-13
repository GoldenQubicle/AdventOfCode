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
        private readonly List<long> packages;
        public Day24(string file) : base(file, "\n") => packages = Input.Select(long.Parse).ToList();


        public Day24(List<string> input) : base(input) { }

        public override string SolvePart1( )
        {
            var targetWeight = packages.Sum() / 3;
            var sets = new HashSet<List<long>>();

            for(var i = packages.Count - 1 ; i >= 0 ; i--)
            {
                var sum = packages[i];
                var cSet = new List<long> { packages[i] };
                var n = i - 1;
                var nP = n;

                while(sum < targetWeight)
                {
                    if(n < 0)
                    {
                        cSet = new List<long> { packages[i] };
                        sum = packages[i];
                        nP--;
                        n = nP;
                    }

                    if(nP < 0) break;

                    sum += packages[n];
                    cSet.Add(packages[n]);

                    if(sum == targetWeight)
                    {
                        sets.Add(cSet);
                        cSet = new List<long> { packages[i] };
                        sum = packages[i];
                        nP--;
                        n = nP;
                    }

                    if(sum > targetWeight)
                    {
                        sum -= packages[n];
                        cSet.Remove(packages[n]);
                    }
                    n--;
                }

                var t = "";
            }

            var result = sets.GroupBy(p => p.Count).OrderBy(g => g.Key).First();
            var quantumEntanglement = result.Count() > 1
                ? result.GroupBy(g => g.Aggregate(1L, (sum, v) => sum * v)).OrderBy(g => g.Key).First().Key
                : result.First().Aggregate(1L, (sum, v) => sum * v);
            return quantumEntanglement.ToString();


        }

        private long OldCombinatorAlmostSolution( )
        {
            var targetWeight = packages.Sum() / 3;
            // this works brilliantly for the test case but alas, too big a list of input to generate all +200 million combinations in a timely manner
            var indices = Combinator.Generate(new List<int> { 0, 1 }, new CombinatorOptions
            {
                Length = packages.Count
            });

            var sets = new HashSet<List<long>>();

            indices.ForEach(c =>
            {
                var p = c.Select((v, i) => v == 1 ? packages[i] : 0).ToList();
                if(p.Sum() == targetWeight)
                {
                    sets.Add(p.Where(v => v != 0).ToList());
                }
            });

            var result = sets.GroupBy(p => p.Count).OrderBy(g => g.Key).First();
            var quantumEntanglement = result.Count() > 1
                ? result.GroupBy(g => g.Aggregate(1L, (sum, v) => sum * v)).OrderBy(g => g.Key).First().Key
                : result.First().Aggregate(1L, (sum, v) => sum * v);
            return quantumEntanglement;
        }


        public override string SolvePart2( ) => null;
    }
}