using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2015
{
    public class Day17 : Solution
    {
        private readonly List<int> containers;
        public int Liters { get; set; } = 150;
        public Day17(string file) : base(file, "\n") => 
            containers = Input.Select(line => int.Parse(line)).ToList( );

        public override string SolvePart1( )
        {
            var start = Enumerable.Range(0, containers.Count).Select(i => 9).ToList();

            // not quite ready yet, need some way to also indicate I want only unique combinations
            // that is to same, atm a-b is seen as different from b-a where this is not correct in this case
            var perm = Combinator.Generate(new List<int> {0,1,2,3,4}, isFullSet: true)
                .Select(l => l.Aggregate(0d, (sum, d) => sum += containers[d]))
                .Count(sum => sum == Liters); 
            
            var permutations = GenerateCombinations(start, new List<List<int>>( ))
                .Where(l => l.Count(d => d == 1) >= 2) 
                .Select(l => l.Select((d, i) => d == 1 ? i : -1))
                .Select(l => l.Aggregate(0d, (sum, d) => d != -1 ? sum += containers[d] : sum))
                .Count(sum => sum == Liters);
            
            return permutations.ToString( );
        }

        public override string SolvePart2( )
        {
            var start = Enumerable.Range(0, containers.Count).Select(i => 9).ToList( );

            var permutations = GenerateCombinations(start, new List<List<int>>( ))
                .Where(l => l.Count(d => d == 1) >= 2)
                .Select(l => l.Select((d, i) => d == 1 ? i : -1))
                .Select(l => (containers: l.Count(d => d != -1), liters: l.Aggregate(0d, (sum, d) => d != -1 ? sum += containers[d] : sum)))
                .Where(sum => sum.liters == Liters);

            var min = permutations.Min(sum => sum.containers);

            return permutations.Count(sum => sum.containers == min).ToString( );
        }

        private List<List<int>> GenerateCombinations(List<int> perm, List<List<int>> result)
        {
            var idx = perm.IndexOf(9);
            if ( idx == -1 )
                return result.Expand(perm);

            var c1 = perm.ReplaceAt(idx, 0);
            var c2 = perm.ReplaceAt(idx, 1);

            return GenerateCombinations(c1, result).Concat(GenerateCombinations(c2, result)).ToList();
        }
    }
}