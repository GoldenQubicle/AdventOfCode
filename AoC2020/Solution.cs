using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020
{
    public abstract class Solution<TOut>
    {
        public List<string> Input { get; }
        protected Solution(string day) => Input = ParseInput(day, "\r\n");
        protected Solution(string day, string split) => Input = ParseInput(day, split);

        public static List<string> ParseInput(string file, string split) =>
            File.ReadAllText($"../../../../input/{file}.txt")
                .Split(split)
                .Select(s => s.Trim( ))
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList( );        

        public abstract TOut SolvePart1( );
        public abstract TOut SolvePart2( );
    }
}
