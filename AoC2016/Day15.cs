using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace AoC2016
{
    public class Day15 : Solution
    {
        private readonly List<Disc> discs;

        public Day15(string file) : base(file, "\n")
        {
            var digits = new Regex("\\d+");
            discs = Input.Select(line =>
            {
                var matches = digits.Matches(line);
                return new Disc
                {
                    Offset = int.Parse(matches[0].Value),
                    Slots = int.Parse(matches[1].Value),
                    Position = int.Parse(matches[3].Value),
                };
            }).ToList();
        }

        public override string SolvePart1( ) => RotateDiscs();

        public override string SolvePart2( )
        {
            discs.Add(new Disc
            {
                Position = 0,
                Offset = discs.Count + 1,
                Slots = 11
            });
            return RotateDiscs();
        }

        private string RotateDiscs()
        {
            var time = 0;

            while (discs.Any(d => (d.Position + d.Offset + time) % d.Slots != 0))
                time++;

            return time.ToString();
        }


        public class Disc
        {
            public int Slots { get; set; }
            public int Position { get; set; }
            public int Offset { get; set; }
        }
    }
}