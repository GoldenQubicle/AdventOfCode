using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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


        public override string SolvePart1( )
        {
            var distances = new Dictionary<string, int>( );
            foreach ( var key in graph.Keys )
            {
                var nodes = graph.Keys.ToList( );
                var current = key;
                nodes.RemoveAt(nodes.IndexOf(current));

                var distance = 0;
                while ( nodes.Count > 0 )
                {
                    var next = graph[current].OrderBy(d => d.dist).First(d => nodes.Contains(d.dest));
                    nodes.RemoveAt(nodes.IndexOf(next.dest));
                    current = next.dest;
                    distance += next.dist;
                }
                distances.Add(key, distance);
            }

            return distances.Values.Min( ).ToString( );
        }

        public override string SolvePart2( )
        {
            var distances = new Dictionary<string, List<int>>( );

            foreach ( var (key, nodes) in graph )
            {
                distances.Add(key, new List<int>( ));
                var stack = new Stack<(string node, List<string> visited, int distance)>( );
                nodes.ForEach(n => stack.Push((n.dest, new List<string> { key, n.dest }, n.dist)));
               
                while(stack.Count > 0 )
                {
                    var current = stack.Pop( );
                    var next = graph[current.node].Where(n => !current.visited.Contains(n.dest)).ToList( );

                    if(next.Count == 0 )
                        distances[key].Add(current.distance);
                    else 
                        next.ForEach(n => stack.Push((n.dest, current.visited.AddTo(n.dest), current.distance + n.dist)));
                }
            }

            return distances.Values.Select(i => i.Max( )).Max( ).ToString();
        }
    }
}