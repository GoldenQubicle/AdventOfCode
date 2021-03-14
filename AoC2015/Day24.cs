using System.Collections.Generic;
using System.Linq;
using Combinatorics.Collections;
using Common;
using Common.Extensions;

namespace AoC2015
{
    public class Day24 : Solution
    {
        private readonly List<long> packages;
        public Day24(string file) : base(file, "\n") => packages = Input.Select(long.Parse).ToList();
        
        public override string SolvePart1( ) => GetQE(3);
        public override string SolvePart2( ) => GetQE(4);

        private string GetQE(int groups)
        {
            //note this will take a while to compute..
            var targetWeight = packages.Sum() / groups;
            var sets = new HashSet<IList<long>>();
            for (var i = 2; i < packages.Count; i++)
            {
                var combos = new Combinations<long>(packages, i);
                foreach (var combo in combos)
                {
                    if (combo.Sum() == targetWeight)
                        sets.Add(combo);
                }
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
    }
}