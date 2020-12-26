using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common
{
    public abstract class Solution
    {
        public List<string> Input { get; }
        protected Solution(string day) => Input = ParseInput(day, "\r\n");
        protected Solution(string day, string split) => Input = ParseInput(day, split);

        public static List<string> ParseInput(string file, string split) =>
            File.ReadAllText($"{Directory.GetCurrentDirectory()}/data/{file}.txt")
                .Split(split)
                .Select(s => s.Trim( ))
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList( );        

        public abstract string SolvePart1( );
        public abstract string SolvePart2( );
    }
}
