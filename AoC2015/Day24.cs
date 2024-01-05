using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Combinatorics.Collections;
using Common;

namespace AoC2015
{
    public class Day24 : Solution
    {
        private readonly List<long> packages;
        public Day24(string file) : base(file, "\n") => packages = Input.Select(long.Parse).ToList();
        
        public override async Task<string> SolvePart1( ) => GetQuantumEntanglement(3);
        public override async Task<string> SolvePart2( ) => GetQuantumEntanglement(4);

        private string GetQuantumEntanglement(int groups)
        {
            //note this will take a while to compute..
            var targetWeight = packages.Sum() / groups;
            var sets = new HashSet<IReadOnlyList<long>>();
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
    }
}