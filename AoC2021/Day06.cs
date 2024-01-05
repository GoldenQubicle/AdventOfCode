using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day06 : Solution
    {
        public Day06(string file) : base(file) { }

        public override async Task<string> SolvePart1() => Simulate(80);

        public override async Task<string> SolvePart2() => Simulate(256);

        private string Simulate(int iterations)
        {
            var fishTimers = new Dictionary<int, long>
            {
                { 0, 0 }, { 1, 0 }, { 2, 0 },
                { 3, 0 }, { 4, 0 }, { 5, 0 },
                { 6, 0 }, { 7, 0 }, { 8, 0 },
            };

            Input.First().Split(',').Select(int.Parse).ForEach(f => fishTimers[f]++);

            for (var i = 0; i < iterations; i++)
            {
                var spawning = fishTimers[0];
                var nextTick = fishTimers.Skip(1).ToDictionary(kvp => kvp.Key - 1, kvp => kvp.Value);
                nextTick[6] += spawning;
                nextTick[8] = spawning;
                fishTimers = nextTick;
            }
            return fishTimers.Values.Sum().ToString();
        }
    }
}