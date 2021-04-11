using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;

namespace AoC2016
{
    public class Day20 : Solution
    {
        private readonly List<(long start, long end)> ranges;
        public long MaxIp { get; set; } = 4294967295;
        public Day20(string file) : base(file, "\n")
        {
            ranges = Input
                .Select(line => line.Split("-"))
                .Select(parts => (start: long.Parse(parts[0]), end: long.Parse(parts[1]))).ToList();
        }

        public override string SolvePart1( )
        {
            var ip = 0l;
            ranges.OrderBy(r => r.start)
                .ForEach(range =>
                {
                    if(ip >= range.start && ip <= range.end)
                        ip = range.end + 1;
                });
            return ip.ToString();
        }

        public override string SolvePart2( )
        {
            var ips = new List<long>();
            var max = -1l;
            
            ranges.OrderBy(r => r.start).ForEach(range =>
            {
                if(max < range.start - 1)
                {
                    ips.Add((range.start - 1) - max);
                }
                max = range.end > max ? range.end : max;
            });

            if(max < MaxIp)
            {
                ips.Add(MaxIp - max );
            }

            return ips.Sum().ToString();
        }
    }
}