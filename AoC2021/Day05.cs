using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day05 : Solution
    {
        private readonly List<((int x, int y) start, (int x, int y) end)> lines;
        private Dictionary<(int, int), int> points = new();

        public Day05(string file) : base(file)
        {
            var r = new Regex(@"(?<start>\d+,\d+).->.(?<end>\d+,\d+)");

            lines = Input.Select(i => r.Match(i))
                .Select(m =>
                {
                    var s = m.Groups["start"].Value.Split(',');
                    var e = m.Groups["end"].Value.Split(',');
                    return (start: (int.Parse(s[0]), int.Parse(s[1])),
                              end: (int.Parse(e[0]), int.Parse(e[1])));
                }).ToList();
        }

        public override string SolvePart1()
        {
            AddHorizontalPoints();
            AddVerticalPoints();

            return points.Values.Count(v => v >= 2).ToString();
        }

        public override string SolvePart2()
        {
            points = new();
            AddHorizontalPoints();
            AddVerticalPoints();
            AddDiagonalPoints();
            return points.Values.Count(v => v >= 2).ToString();
        }

        private void AddDiagonalPoints() => 
            lines.Where(l => Math.Abs(l.start.x - l.end.x) == Math.Abs(l.start.y - l.end.y)).ForEach(l =>
            Enumerable.Range(0, Math.Abs(l.start.x - l.end.x) + 1)
                   .ForEach(s =>
                   {
                       var x = l.start.x > l.end.x ? l.start.x - s : l.start.x + s;
                       var y = l.start.y > l.end.y ? l.start.y - s : l.start.y + s;
                       AddPoint(x, y);
                   }));
        private void AddVerticalPoints() =>
            lines.Where(l => l.start.y == l.end.y).ForEach(l =>
                Enumerable.Range(l.start.x > l.end.x ? l.end.x : l.start.x, Math.Abs(l.start.x - l.end.x) + 1)
                    .ForEach(x => AddPoint(x, l.start.y)));

        private void AddHorizontalPoints() =>
            lines.Where(l => l.start.x == l.end.x).ForEach(l =>
                Enumerable.Range(l.start.y > l.end.y ? l.end.y : l.start.y, Math.Abs(l.start.y - l.end.y) + 1)
                    .ForEach(y => AddPoint(l.start.x, y)));

        private void AddPoint(int x, int y)
        {
            if (points.ContainsKey((x, y))) points[(x, y)]++;
            else points.Add((x, y), 1);
        }
    }
}