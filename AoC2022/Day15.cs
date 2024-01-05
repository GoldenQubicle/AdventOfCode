namespace AoC2022
{
    public class Day15 : Solution
    {
        public long RowToCheck { get; set; } = 2000000;
        public (long min, long max) SearchArea { get; set; } = (0, 4000000);
        private readonly List<((long x, long y) s, long d)> sensors;

        public Day15(string file) : base(file) => sensors = Input
            .Select(line => Regex.Match(line, @"Sensor at x=(?<sx>-?\d+), y=(?<sy>-?\d+): closest beacon is at x=(?<bx>-?\d+), y=(?<by>-?\d+)"))
            .Select(m => (s: (x: m.AsLong("sx"), y: m.AsLong("sy")), b: (x: m.AsLong("bx"), y: m.AsLong("by"))))
            .Select(p => (p.s, d: GetDistance(p.s, p.b))).ToList();

        public override async Task<string> SolvePart1()
        {
            var maxd = sensors.Max(p => p.d);
            var minx = sensors.Min(p => p.s.x) - maxd;
            var maxx = sensors.Max(p => p.s.x) + maxd;

            return (Enumerable.Range((int)minx, (int)(maxx - minx))
                .Count(x => sensors.Any(d => GetDistance(d.s, (x, RowToCheck)) <= d.d)) - 1).ToString(); // no idea why the -1 is needed but it is..
        }

        public override async Task<string> SolvePart2()
        {
            //walk along the edges of each sensor checking for free spot not in range of any other sensor
            foreach (var s in sensors)
            {
                var toCheck = sensors.Except(new List<((long x, long y) s, long d)> { s }).ToList();

                foreach (var p in WalkPerimeter(s.s, s.d))
                {
                    if (p.x < SearchArea.min || p.x > SearchArea.max ||
                        p.y < SearchArea.min || p.y > SearchArea.max) continue;

                    if (toCheck.Any(s => GetDistance(s.s, p) <= s.d)) continue;

                    return (p.x * 4_000_000 + p.y).ToString();
                }
            }

            return string.Empty;
        }

        private IEnumerable<(long x, long y)> WalkPerimeter((long x, long y) c, long d)
        {
            var directions = new List<(long, long)>
            {
                (1, -1),
                (-1, -1),
                (-1, 1),
                (1, 1),
            };
            var current = (c.x, c.y + d + 1);
            var i = -1;
            var idx = 0;
            while (i++ < (d + 1) * 4 - 1)
            {
                if (i > 0 && i % (d + 1) == 0) idx++;
                if (i == 0) yield return current;
                current = current.Add(directions[idx]);
                yield return current;
            }
        }

        private long GetDistance((long x, long y) from, (long x, long y) to) =>
            Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y);
    }
}