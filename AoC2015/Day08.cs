using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;

namespace AoC2015
{
    public class Day08 : Solution
    {
        public Day08(string file) : base(file, "\n") { }

        public override async Task<string> SolvePart1( )
        {
            var expression = new Regex(@"(\\{2})|(\\x(\w{2}))|(\\"")|(\w)");
            var count = Input
                .Select(line => (code: line.Length, memory: expression.Matches(line).Count))
                .Aggregate((code: 0, memory: 0), (agg, line) => (agg.code + line.code, agg.memory + line.memory));

            return ( count.code - count.memory ).ToString( );
        }

        public override async Task<string> SolvePart2( )
        {
            var expression = new Regex(@"(?<s>\\{2})|(?<h>\\x\w{2})|(?<sq>\\"")|(?<q>\"")");

            return Input
                .Select(line =>
                {
                    var s = 0;
                    var q = 0;
                    var sq = 0;
                    var h = 0;
                    foreach ( Match match in expression.Matches(line) )
                    {
                        s += match.Groups["s"].Captures.Count * 2;
                        q += match.Groups["q"].Captures.Count * 2;
                        sq += match.Groups["sq"].Captures.Count * 2;
                        h += match.Groups["h"].Captures.Count;
                    }
                    return ( line.Length + s + q + sq + h ) - line.Length;
                }).Sum( ).ToString();
        }
    }
}