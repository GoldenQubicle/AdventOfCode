using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day06 : Solution
    {
        public Day06(string file) : base(file) { }

        public override string SolvePart1() => Simulate(80);

        public override string SolvePart2() => Simulate(256);

        private string Simulate(int iterations)
        {
            var fishes = new Dictionary<int, long>
            {
                { 0, 0 }, { 1, 0 }, { 2, 0 },
                { 3, 0 }, { 4, 0 }, { 5, 0 },
                { 6, 0 }, { 7, 0 }, { 8, 0 },
            };
            Input.First().Split(',').Select(int.Parse).ForEach(f => fishes[f]++);

            for (var i = 0; i < iterations; i++)
            {
                var spawning = fishes[0];
                var nextGen = fishes.Skip(1).ToDictionary(kvp => kvp.Key - 1, kvp => kvp.Value);
                nextGen[6] += spawning;
                nextGen[8] = spawning;
                fishes = nextGen;
            }
            return fishes.Values.Sum().ToString();
        }
    }
}