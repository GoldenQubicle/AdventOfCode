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
            var options = new CombinatorOptions
            {
                IsFullSet = true,
                IsElementUnique = true,
                IsOrdered = false
            };

            var result = Combinator.Generate(new List<string> { "0", "1", "2", "3", "4" }, options)
                .Select(r => r.Select(int.Parse))
                .Select(l => l.Aggregate(0d, (sum, d) => sum += containers[d]))
                .Count(sum => sum == Liters);

            return result.ToString( );
        }

        public override string SolvePart2( )
        {
            var options = new CombinatorOptions
            {
                IsFullSet = true,
                IsElementUnique = true,
                IsOrdered = false
            };

            var result = Combinator.Generate(new List<string> { "0", "1", "2", "3", "4" }, options)
                .Select(r => r.Select(int.Parse))
                .Select(l => (containers: l.Count( ), liters: l.Aggregate(0d, (sum, d) => sum += containers[d])))
                .Where(sum => sum.liters == Liters);

            var min = result.Min(sum => sum.containers);

            return result.Count(sum => sum.containers == min).ToString( );
        }
    }
}