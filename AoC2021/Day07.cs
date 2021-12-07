using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2021
{
    public class Day07 : Solution
    {
        private readonly List<int> numbers;

        public Day07(string file) : base(file) => numbers = Input.First().Split(',').Select(int.Parse).ToList();

        public override string SolvePart1( ) => numbers.Select((_, idx) => 
            numbers.Select(n => Math.Abs(n - idx)).Sum()).Min().ToString();

        public override string SolvePart2() => numbers.Select((_, idx) => 
            numbers.Select(n => Math.Abs(n - idx)).Select(c => Enumerable.Range(1, c).Sum()).Sum()).Min().ToString();
    }
}