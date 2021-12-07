using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2021
{
    public class Day07 : Solution
    {
        private readonly List<int> numbers;

        public Day07(string file) : base(file)
        {
            numbers = Input.First().Split(',').Select(int.Parse).ToList();
        }
        

        public override string SolvePart1( )
        {
            var cheapest = int.MaxValue;
            for (var i = 0; i < numbers.Count; i++)
            {
                var cost = numbers.Select(n => Math.Abs(n - i)).Sum();
                cheapest = cost < cheapest ? cost : cheapest;
            }
            return cheapest.ToString();
        }

        public override string SolvePart2()
        {
            var cheapest = int.MaxValue;
            for (var i = 0; i < numbers.Count; i++)
            {
                var cost = numbers.Select(n => Math.Abs(n - i)).Select(c => Enumerable.Range(1, c).Sum()).Sum();
                cheapest = cost < cheapest ? cost : cheapest;
            }
            return cheapest.ToString();
        }
    }
}