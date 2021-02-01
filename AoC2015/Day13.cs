using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2015
{
    public class Day13 : Solution
    {
        private readonly Dictionary<string, Dictionary<string, int>> persons = new( );

        public Day13(string file) : base(file, "\n")
        {
            var pattern = new Regex(@"(?<name>[A-Z]\w+)|(?<direction>gain|lose)|(?<unit>\d{1,})");
           
            Input.ForEach(line =>
            {
                var matches = pattern.Matches(line);
                var name = matches[0].Value;
                var other = matches[3].Value;
                var unit = matches[1].Value.Equals("gain") ?
                            int.Parse(matches[2].Value) :
                            int.Parse(matches[2].Value) * -1;

                if ( !persons.ContainsKey(name) )
                    persons.Add(name, new Dictionary<string, int> { { other, unit } });
                else
                    persons[name].Add(other, unit);
            });

        }

        public override string SolvePart1( ) => FindOptimalArrangement( );

        public override string SolvePart2( )
        {
            persons.Add("me", new Dictionary<string, int>( ));
            persons.ForEach(p =>
            {
                if ( !p.Key.Equals("me") )
                {
                    persons["me"].Add(p.Key, 0);
                    p.Value.Add("me", 0);
                }
            });

            return FindOptimalArrangement( );
        }

        private string FindOptimalArrangement( )
        {
            var arrangments = new List<int>( );
            persons.ForEach(person =>
            {
                person.Value.ForEach(other =>
                {
                    var queue = new Queue<(string person, List<string> seated, int score)>( );
                    queue.Enqueue((other.Key, new List<string> { person.Key, other.Key }, GetScore(person.Key, other.Key)));

                    while ( queue.Count > 0 )
                    {
                        var current = queue.Dequeue( );
                        var nextMatches = persons[current.person].Where(kvp => !current.seated.Contains(kvp.Key));
                     
                        if ( nextMatches.Any( ) )
                            nextMatches.ForEach(n => queue.Enqueue((n.Key, current.seated.Expand(n.Key),
                                    current.score + GetScore(current.person, n.Key))));
                        else
                            arrangments.Add(current.score + GetScore(current.person, person.Key));
                    }
                });
            });

            return arrangments.Max( ).ToString( );
        }

        private int GetScore(string p1, string p2) => persons[p1][p2] + persons[p2][p1];

    }
}