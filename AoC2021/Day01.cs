using System.Linq;
using Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Extensions;

namespace AoC2021
{
    public class Day01 : Solution
    {
        private readonly List<(int idx, int depth)> sonarReadings;
        public Day01(string file) : base(file) =>
            sonarReadings = Input.Select((i, idx) => (idx, int.Parse(i))).ToList();

        public override async Task<string> SolvePart1() => sonarReadings.Aggregate(0, (count, reading) =>
            reading.idx == 0 ? count :
            reading.depth > sonarReadings[reading.idx - 1].depth ? count + 1 : count).ToString();

        public override async Task<string> SolvePart2()
        {
            var summedWindows = sonarReadings.Aggregate(new List<int>(), (windows, reading) =>
                     reading.idx + 2 > sonarReadings.Count - 1 ? windows
                         : windows.Expand(reading.depth + sonarReadings[reading.idx + 1].depth + sonarReadings[reading.idx + 2].depth))
                   .Select((sum, idx) => (sum, idx)).ToList();

            return summedWindows.Aggregate(0, (count, window) => window.idx == 0 ? count :
                window.sum > summedWindows[window.idx - 1].sum ? count + 1 : count).ToString();
        }
    }
}