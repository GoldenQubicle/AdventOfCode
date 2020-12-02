using AoC2020.Solutions;
using System;

namespace AoC2020
{
    internal class Program
    {
        static void Main()
        {
            var day1 = new Day01(nameof(Day01));
            Console.WriteLine($"Day 1 part 1: {day1.SolvePart1( )} ");
            Console.WriteLine($"Day 1 part 2: {day1.SolvePart2( )} ");

            var day2 = new Day02(nameof(Day02));
            Console.WriteLine($"Day 2 part 1: {day2.SolvePart1( )} ");
            Console.WriteLine($"Day 2 part 2: {day2.SolvePart2( )} ");

        }
    }
}
