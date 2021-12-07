using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;

namespace AoC2021
{
    public class Day07 : Solution
    {
        public Day07(string file) : base(file) { }
        
        public Day07(List<string> input) : base(input) { }

        public override string SolvePart1( )
        {
            var numbers = Input.First().Split(',').Select(int.Parse).ToList();
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
            var numbers = Input.First().Split(',').Select(int.Parse).ToList();
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