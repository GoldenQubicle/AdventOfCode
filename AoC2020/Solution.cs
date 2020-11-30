using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020
{
    public abstract class Solution
    {
        public List<string> Input { get; }
        protected Solution(string day) =>
            Input = File.ReadAllText($"../../../../input/{day}.txt").Split(',').ToList( );
        
        public abstract string SolvePart1( );
        public abstract string SolvePart2( );
    }
}
