using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace AoC2015
{
    public class Day09 : Solution
    {
        private Dictionary<string, List<(string dest, int dist)>> graph = new( );
        public Day09(string file) : base(file, "\n")
        {
            var expression = new Regex(@"(?<from>\w*).{4}(?<to>\w*).{3}(?<distance>\d*)");
            Input
                .Select(line => expression.Match(line))
                .ForEach(match =>
                {
                    var from = match.Groups["from"].Value;
                    var to = match.Groups["to"].Value;
                    var dist = int.Parse(match.Groups["distance"].Value);

                    if ( !graph.ContainsKey(from) )
                        graph.Add(from, new List<(string dest, int dist)> { (to, dist) });
                    else
                        graph[from].Add((to, dist));

                    if ( !graph.ContainsKey(to) )
                        graph.Add(to, new List<(string dest, int dist)> { (from, dist) });
                    else
                        graph[to].Add((from, dist));
                });
        }

        public override string SolvePart1( ) => CalculateDistances( ).Values.Select(i => i.Min( )).Min( ).ToString( );
        
        public override string SolvePart2( ) => CalculateDistances( ).Values.Select(i => i.Max( )).Max( ).ToString( );
      
        private Dictionary<string, List<int>> CalculateDistances( )
        {
            var distances = new Dictionary<string, List<int>>( );

            foreach ( var (key, nodes) in graph )
            {
                distances.Add(key, new List<int>( ));
                var stack = new Stack<(string node, List<string> visited, int distance)>( );
                nodes.ForEach(n => stack.Push((n.dest, new List<string> { key, n.dest }, n.dist)));

                while ( stack.Count > 0 )
                {
                    var current = stack.Pop( );
                    var next = graph[current.node].Where(n => !current.visited.Contains(n.dest)).ToList( );

                    if ( next.Count == 0 )
                        distances[key].Add(current.distance);
                    else
                        next.ForEach(n => stack.Push((n.dest, current.visited.Expand(n.dest), current.distance + n.dist)));
                }
            }
            return distances;
        }
    }
}