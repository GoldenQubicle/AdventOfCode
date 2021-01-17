using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

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
                var name = matches.First( ).Value;
                var other = matches.Last( ).Value;
                var direction = matches[1].Value;
                var unit = direction.Equals("gain") ?
                            int.Parse(matches[2].Value) :
                            int.Parse(matches[2].Value) * -1;

                if ( !persons.ContainsKey(name) )
                    persons.Add(name, new Dictionary<string, int> { { other, unit } });
                else
                    persons[name].Add(other, unit);
            });
        }

        public override string SolvePart1( )
        {
            var arrangments = new List<int>( );
            persons.ForEach(person =>
            {
                person.Value.ForEach(other =>
                {
                    var queue = new Queue<(string person, List<string> seated, int score)>( );
                    queue.Enqueue(( other.Key,
                        new List<string> { person.Key, other.Key },
                        persons[person.Key][other.Key] + persons[other.Key][person.Key]));

                    while ( queue.Count > 0 )
                    {
                        var current = queue.Dequeue( );
                        var nextMatches = persons[current.person].Where(kvp => !current.seated.Contains(kvp.Key));
                        if ( nextMatches.Any( ) )
                        {
                            nextMatches.ForEach(n =>
                            {
                                queue.Enqueue((n.Key,
                                    current.seated.Expand(n.Key),
                                    current.score + persons[current.person][n.Key] + persons[n.Key][current.person]));
                            });
                        } else
                        {
                            arrangments.Add(current.score);
                        }
                    }
                });
            });

            return arrangments.Max( ).ToString( );
        }

        public override string SolvePart2( ) => null;
    }
}