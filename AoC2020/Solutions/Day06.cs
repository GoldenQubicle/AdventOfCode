using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Solutions
{
    public class Day06 : Solution<int>
    {
        List<string> part1;
        List<string> part2;
        Dictionary<string, int> answers = new( );
        public Day06(string file) : base(file, "\r\n\r\n")
        {
            part1 = Input.Select(s => s.Replace("\r\n", "")).ToList( );

            var part2 = Input.Select(s => s.Split("\r\n"));
            var counts = new List<int>( );
            foreach(var group in part2)
            {
                if ( group.Length == 1 )
                {
                    counts.Add(group[0].Length);
                    continue;
                }

                var count = new Dictionary<char, int>( );
                foreach ( var person in group )
                {
                    foreach ( var c in person )
                    {
                        if ( !count.ContainsKey(c) )
                            count.Add(c, 1);
                        else
                            count[c]++;
                    }
                }
               counts.Add(count.Where(c => c.Value == group.Length).ToList( ).Count);                
            };
            var s = counts.Sum( );
        }
        public override int SolvePart1( ) => part1.Select(s => s.Distinct( )).Select(s => s.Count( )).Sum( );
        public override int SolvePart2( ) => 0;
    }
}
