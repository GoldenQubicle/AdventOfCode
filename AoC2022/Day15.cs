using System.Reflection.Metadata.Ecma335;

namespace AoC2022
{
    public class Day15 : Solution
    {
        private readonly List<((long x, long y) s, (long x, long y) b)> Sensors;

        public int rowToCheck { get; set; } = 2000000;
        public (int min, int max) SearchArea { get; set; } = (0, 4000000);

        public Day15(string file) : base(file)
        {

            Sensors = Input.Select(line => Regex.Match(line, @"Sensor at x=(?<sx>-?\d+), y=(?<sy>-?\d+): closest beacon is at x=(?<bx>-?\d+), y=(?<by>-?\d+)"))
                .Select(m => (s: (x: m.AsLong("sx"), y: m.AsLong("sy")), b: (x: m.AsLong("bx"), y: m.AsLong("by")))).ToList();
        }

        public override string SolvePart1()
        {
            var minx = Sensors.Min(p => p.s.x);
            var maxx = Sensors.Max(p => p.s.x);

            var distances = Sensors.Select(p => (p.s, d: GetDistance(p.s, p.b))).ToList();

            minx -= distances.Max(p => p.d);
            maxx += distances.Max(p => p.d);

            return (Enumerable.Range((int)minx, (int)(maxx - minx))
                .Count(x => distances.Any(d => GetDistance(d.s, (x, rowToCheck)) <= d.d)) - (Sensors.Any(p => p.b.y == rowToCheck) ? 1 : 0)).ToString();


        }

        private IEnumerable<(long x, long y)> GetPerimeter((long x, long y) c, long d)
        {
            // (0, d) -> (1,-1) until x == s.x
            var directions = new List<(int, int)>
            {
                (1, -1),
                (-1, -1),
                (-1, 1),
                (1, 1),
            };
            var current = (c.x, c.y + d);
            var i = -1;
            while (i++ < d * 4)
            {
                
            }
        }

        private long GetDistance((long x, long y) from, (long x, long y) to) =>
            Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y);



        public override string SolvePart2()
        {
            var distances = Sensors.Select(p => (p.s, d: GetDistance(p.s, p.b))).ToList();
            var t = distances.First();
            //new idea, walk along the edges of each sensor checking for free spots
            foreach (var p in GetPerimeter((0, 0), 1))
            {
                var c = p;
            }

            var ranges = distances.Select(d => (
                x: (min: d.s.x - d.d, max: d.s.x + d.d),
                y: (min: d.s.y - d.d, max: d.s.y + d.d))).ToList();




            return string.Empty;
        }
    }
}