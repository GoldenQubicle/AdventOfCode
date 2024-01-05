using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day08 : Solution
    {
        private readonly List<(List<string> patterns, List<string> output)> digits;

        public Day08(string file) : base(file) => digits = Input.Select(i =>
                {
                    var parts = i.Split(" | ");
                    return (parts[0].Split(' ').ToList(), parts[1].Split(' ').ToList());
                }).ToList();

        public override async Task<string> SolvePart1() => digits.Sum(d =>
            d.output.Count(s => s.Length == 2 || s.Length == 3 || s.Length == 4 || s.Length == 7)).ToString();

        public override async Task<string> SolvePart2() => digits.Select(DetermineWireSegments).Sum().ToString();

        private static int DetermineWireSegments((List<string> patterns, List<string> output) i)
        {
            var decode = new Dictionary<string, string>
            {
                { "1", i.patterns.First(s => s.Length == 2) },
                { "4", i.patterns.First(s => s.Length == 4) },
                { "7", i.patterns.First(s => s.Length == 3) },
                { "8", i.patterns.First(s => s.Length == 7) },
            };

            i.patterns.Where(s => s.Length == 6).Distinct().ForEach(s =>
            {
                var segment = decode["8"].Except(s).First();
                if (!decode["4"].Contains(segment) && !decode["7"].Contains(segment))
                    decode.Add("9", s);
                else if (decode["4"].Contains(segment) && decode["7"].Contains(segment))
                    decode.Add("6", s);
                else decode.Add("0", s);
            });

            i.patterns.Where(s => s.Length == 5).Distinct().ForEach(s =>
            {
                var segments = decode["8"].Except(s).ToList();
                if (!decode["1"].Contains(segments[0]) && !decode["1"].Contains(segments[1]))
                    decode.Add("3", s);
                else if (decode["4"].Contains(segments[0]) && decode["4"].Contains(segments[1]))
                    decode.Add("2", s);
                else decode.Add("5", s);
            });

            return int.Parse(i.output.Select(o => decode.First(kvp => kvp.Value.Length == o.Length && !kvp.Value.Except(o).Any()).Key)
                .Aggregate(new StringBuilder(), (builder, s) => builder.Append(s)).ToString());
        }
    }
}