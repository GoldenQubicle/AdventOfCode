using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace AoC2021
{
    public class Day07 : Solution
    {
        private readonly List<int> numbers;

        public Day07(string file) : base(file) => numbers = Input.First().Split(',').Select(int.Parse).ToList();

        public override async Task<string> SolvePart1( ) => numbers.Select((_, idx) => 
            numbers.Sum(n => Math.Abs(n - idx))).Min().ToString();

        public override async Task<string> SolvePart2() => numbers.Select((_, idx) => 
            numbers.Select(n => Math.Abs(n - idx)).Sum(c => Enumerable.Range(1, c).Sum())).Min().ToString();
    }
}