using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day13 : Solution
    {
        private readonly List<(char dim, int v)> instructions = new();
        private HashSet<(int x, int y)> dots = new();
        public Day13(string file) : base(file)
        {
            Input.ForEach(l =>
            {
                if (string.IsNullOrEmpty(l)) return;
                if (l.StartsWith("fold"))
                {
                    instructions.Add((l[11], int.Parse(l[13..])));
                }
                else
                {
                    var parts = l.Split(",");
                    dots.Add((int.Parse(parts[0]), int.Parse(parts[1])));
                }
            });
        }

        public override string SolvePart1() => Fold(dots, instructions.First()).Count.ToString();

        public override string SolvePart2()
        {
            instructions.ForEach(i => dots = Fold(dots, i));
        
            return Enumerable.Range(0, dots.Max(d => d.y) + 1).Aggregate(new StringBuilder(), (sb, y) =>
              {
                  sb.AppendLine();
                  Enumerable.Range(0, dots.Max(d => d.x) + 1).ForEach(x => sb.Append(dots.Contains((x, y)) ? '#' : '.'));
                  return sb;
              }).ToString();
        }

        private HashSet<(int x, int y)> Fold(HashSet<(int x, int y)> dots, (char dim, int v) op) =>
            dots.Aggregate(new List<(int x, int y)>(), (set, dot) => set.Expand(
                    op.dim == 'x' && dot.x >= op.v ?
                    dot.Add(-((dot.x - op.v) * 2), 0) :
                    op.dim == 'y' && dot.y >= op.v ?
                    dot.Add(0, -((dot.y - op.v) * 2)) :
                    dot
                )).ToHashSet();
    }
}