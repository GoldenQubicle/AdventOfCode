using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2016
{
    public class Day08 : Solution
    {
        private IEnumerable<(string op, int, int)> instructions;
        public List<List<int>> Screen { get; set; }
        private const string Rect = nameof(Rect);
        private const string Col = nameof(Col);
        private const string Row = nameof(Row);


        public Day08(string file) : base(file, "\n")
        {
            var regex = new Regex(@"(?<col>column.x=(?<x>\d+) by (?<d1>\d+))|(?<row>row.y=(?<y>\d+) by (?<d2>\d+))|(?<rect>rect (?<w>\d+)x(?<h>\d+))");
            instructions = Input.Select(line =>
            {
                foreach(Match match in regex.Matches(line))
                {
                    if(match.Groups["rect"].Success) return (op: Rect, int.Parse(match.Groups["w"].Value), int.Parse(match.Groups["h"].Value));
                    if(match.Groups["col"].Success) return (op: Col, int.Parse(match.Groups["x"].Value), int.Parse(match.Groups["d1"].Value));
                    if(match.Groups["row"].Success) return (op: Row, int.Parse(match.Groups["y"].Value), int.Parse(match.Groups["d2"].Value));
                }
                return (op: "", 0, 0);
            });
        }

        public Day08(List<string> input) : base(input) { }

        public override string SolvePart1( )
        {
            InitializeScreen(50, 6);

            instructions.ForEach(i =>
            {
                switch(i.op)
                {
                    case Rect:
                        AddRect(i.Item2, i.Item3);
                        break;
                    case Col:
                        ShiftColumn(i.Item2, i.Item3);
                        break;
                    case Row:
                        ShiftRow(i.Item2, i.Item3);
                        break;
                }
            });

            return Screen.Sum(row => row.Count(p => p == 1)).ToString();
        }

        public override string SolvePart2( )
        {
            SolvePart1();
            var sb = new StringBuilder();
            sb.AppendLine();
            Screen.ForEach(row =>
            {
                row.ForEach(s => sb.Append(s == 1 ? "#" : " "));
                sb.AppendLine();
            });
            return sb.ToString();
        }

        public void InitializeScreen(int width, int height)
        {
            Screen = new List<List<int>>();

            for(var h = 0 ; h < height ; h++)
            {
                var row = new List<int>();
                for(var w = 0 ; w < width ; w++)
                {
                    row.Add(0);
                }
                Screen.Add(row);
            }
        }

        public void AddRect(int width, int height)
        {
            for(var w = 0 ; w < width ; w++)
            {
                for(var h = 0 ; h < height ; h++)
                {
                    Screen[h][w] = 1;
                }
            }
        }

        public void ShiftRow(int row, int shift) => Screen = Screen.ReplaceAt(row, ShiftPixels(Screen[row].ToArray(), shift).ToList());

        public void ShiftColumn(int col, int shift) => Screen = Screen.Select((row, i) => row.ReplaceAt(col, ShiftPixels(Screen.Select(r => r[col]).ToArray(), shift)[i])).ToList();

        public T[ ] ShiftPixels<T>(T[ ] vec, int shift) => vec[^shift..].Concat(vec[..^shift]).ToArray();

    }
}