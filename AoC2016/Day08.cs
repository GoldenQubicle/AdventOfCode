using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2016
{
    public class Day08 : Solution
    {
        public List<List<int>> Screen { get; set; }

        public Day08(string file) : base(file, "\n")
        {
            InitializeScreen(50, 6);

            var regex = new Regex(@"(?<col>column.x=(?<x>\d+) by (?<d1>\d+))|(?<row>row.y=(?<y>\d+) by (?<d2>\d+))|(?<rect>rect (?<w>\d+)x(?<h>\d+))");

            Input.ForEach(line =>
            {
                foreach(Match match in regex.Matches(line))
                {
                    if(match.Groups["rect"].Success)
                        AddRect(int.Parse(match.Groups["w"].Value), int.Parse(match.Groups["h"].Value));

                    if(match.Groups["col"].Success) 
                        ShiftColumn(int.Parse(match.Groups["x"].Value), int.Parse(match.Groups["d1"].Value));

                    if(match.Groups["row"].Success) 
                        ShiftRow(int.Parse(match.Groups["y"].Value), int.Parse(match.Groups["d2"].Value));
                }
            });
        }

        public Day08(List<string> input) : base(input) { }

        public override string SolvePart1() => Screen.Sum(row => row.Count(p => p == 1)).ToString();

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