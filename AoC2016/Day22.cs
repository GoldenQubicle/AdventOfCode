using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;

namespace AoC2016
{
    public class Day22 : Solution
    {
        private readonly IList<Node> nodes;

        public Day22(string file) : base(file, "\n")
        {
            var pattern = new Regex($@"(?<x>\d+).y(?<y>\d+)\s+(?<size>\d+).\s+(?<used>\d+).\s+(?<avail>\d+).\s+(?<perc>\d+)", RegexOptions.Compiled);

            nodes = Input.Skip(2)
                .Select(line =>
                {
                    var match = pattern.Match(line);
                    return new Node
                    {
                        Position = (int.Parse(match.Groups["x"].Value), int.Parse(match.Groups["y"].Value)),
                        Size = int.Parse(match.Groups["size"].Value),
                        Used = int.Parse(match.Groups["used"].Value),
                        Avail = int.Parse(match.Groups["avail"].Value),
                        Percentage = int.Parse(match.Groups["perc"].Value)
                    };
                }).ToList();
        }

        public override async Task<string> SolvePart1() => nodes.Sum(a => a.Used != 0 ? nodes.Count(b => a != b && b.Avail >= a.Used) : 0).ToString();

        public override async Task<string> SolvePart2()
        {
            //taking a clue from the example in part 2
            //solved this manually by printing the grid from there it shows; 
            //there's only 1 empty cell which we can use to move data around,
            //and there's a wall in our way. 

            var empty = nodes.First(n => n.Used == 0);
            //how many steps it takes to Position the empty cell to the goal cell
            var width = nodes.Max(n => n.Position.x) + 1 ;
            var steps = empty.Position.x + empty.Position.y + width-1;
            //now the empty cell is in front of our goal cell, and it takes 5 steps to complete a cycle and move it over
            //we need to move the goal cell over the entire width
            steps += 5 * (width-2);

            /* drawing the grid
            var orderedGrid = nodes.OrderBy(n => n.Position.x).ThenBy(n => n.Position.y);            
            orderedGrid
                .GroupBy(n => n.Position.y)
                .ForEach(group =>
                {
                    var sb = new StringBuilder();
                    group.ForEach(n =>
                    {
                        if (n.Used == 0)
                            sb.Append("_");
                        else if (n.Used > empty.Size)
                            sb.Append("#");
                        else sb.Append(".");
                    });
                    Console.WriteLine(sb.ToString());
                    
                });
            */
            return steps.ToString();
        }
    }

    public class Node
    {
        public (int x, int y) Position { get; init; }
        public int  Size { get; init; }
        public int  Used { get; init; }
        public int  Percentage { get; init; }
        public int  Avail { get; init; }

        
    }
}