using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020
{
    public abstract class Solution
    {
        public List<string> Input { get; }

        protected Solution(string day) =>
            Input = File.ReadAllText($"../../../../input/{day}.txt")
                .Split(',')
                .ToList()
                .Select(s => s.Trim())
                .ToList();
                
        
        public abstract int SolvePart1( );
        public abstract int SolvePart2( );
    }
}
