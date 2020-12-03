using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020
{
    public abstract class Solution<TOut>
    {
        public List<string> Input { get; }
        protected Solution(string day) => Input = ParseInput(day);
        public static List<string> ParseInput(string file) =>
            File.ReadAllText($"../../../../input/{file}.txt")
                .Split(',')
                .Select(s => s.Trim( ))
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList( );        

        public abstract TOut SolvePart1( );
        public abstract TOut SolvePart2( );
    }
}
