using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;

namespace AoC2015
{
    public class Day12 : Solution
    {
        public Day12(string file) : base(file) { }
        
        public Day12(List<string> input) : base(input) { }

        public override string SolvePart1( )
        {
            var matches = Regex.Matches(Input.First(), @"(?<positive>\d{1,})|(?<negative>-\d{1,})");
            var result = 0;
            foreach(Match match in matches )
            {
                result += match.Groups["positive"].Success ? 
                    int.Parse(match.Groups["positive"].Value) : 
                    int.Parse(match.Groups["negative"].Value); 
            }


            return result.ToString();
        }

        public override string SolvePart2( )
        {
            var openings = new Stack<int>( );

            var ranges = Input[0].Select((c,i) =>
            {
                var range = (opening: 0, closing: 0);
                if ( c == '{' ) openings.Push(i);
                if ( c == '}' ) range = (opening: openings.Pop( ), closing: i);
                return range;
            }).Where(r => r.closing != 0)
            .Select(r => Enumerable.Range(r.opening, r.closing - r.opening)).ToList();

            var reds = Regex.Matches(Input.First( ), @"(?<red>""red"")");
            var tobeIgnored = new List<IEnumerable<int>>( );
            foreach(Match match in reds )
            {
                var idx = match.Index;
                var add = ranges.OrderBy(r => r.First( ) - idx).FirstOrDefault( );
                if(add != default)
                    tobeIgnored.Add(add);                
            }
            tobeIgnored = tobeIgnored.Distinct( ).ToList();

            var matches = Regex.Matches(Input.First( ), @"(?<positive>\d{1,})|(?<negative>-\d{1,})");
            var result = 0;
            foreach ( Match match in matches )
            {
                if ( !tobeIgnored.Any(r => r.Contains(match.Index)) )
                {
                    result += match.Groups["positive"].Success ?
                        int.Parse(match.Groups["positive"].Value) :
                        int.Parse(match.Groups["negative"].Value);
                }
            }

            return result.ToString();
        }
    }
}