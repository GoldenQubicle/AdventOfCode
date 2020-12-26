using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2020
{
    public class Day19 : Solution
    {
        private Dictionary<string, List<List<string>>> rules;
        private List<string> messages;

        public Day19(string file) : base(file, "\r\n\r\n")
        {
            rules = Input[0]
                .Split("\r\n")
                .Select(i => i.Split(":").Select(i => i.Trim( )).ToArray( ))
                .ToDictionary(i => i[0], i => i[1].Split("|").Select(i => i.Trim( )).Select(i => i.Split(" ").ToList( )).ToList( ));

            //this is so dumb
            foreach ( var kvp in rules )
            {
                if ( kvp.Value[0].Contains("\"a\"") )
                {
                    kvp.Value[0].Clear( );
                    kvp.Value[0].Add("a");
                }
                if ( kvp.Value[0].Contains("\"b\"") )
                {
                    kvp.Value[0].Clear( );
                    kvp.Value[0].Add("b");
                }
            }

            messages = Input[1].Split("\r\n").ToList( );
        }

        public override string SolvePart1( )
        {
            var startIndex = 0;
            rules["0"][0].ForEach(d =>
            {
                var ruleResult = RecurseRule(new List<string> { d });
                var ruleCombos = ruleResult.Select(l => new string(l.SelectMany(s => s.ToCharArray( )).ToArray( ))).ToList( );
                var endIndex = startIndex + ruleCombos[0].Length;
                messages = messages.Where(m => ruleCombos.Contains(m[startIndex..endIndex])).ToList( );
                startIndex = endIndex;
            });
            return messages.Count(m => m.Length == startIndex).ToString( );
        }

        public override string SolvePart2( )
        {
            messages = Input[1].Split("\r\n").ToList( );

            var valid = new Dictionary<int, List<string>>
            {
                { 31, RecurseRule(new List<string> { "31" })
                        .Select(l => new string(l.SelectMany(s => s.ToCharArray( )).ToArray( ))).ToList( ) },
                { 42, RecurseRule(new List<string> { "42" })
                        .Select(l => new string(l.SelectMany(s => s.ToCharArray( )).ToArray( ))).ToList( ) }
            };

            bool isBlockValid(string mssg, List<string> rule, int start, int end) => rule.Contains(mssg[start..end]);

            var blockLength = valid[31][0].Length;
            var maxLength = messages.Max(m => m.Length);
            var iterate8 = ( maxLength - 2 * blockLength ) / blockLength;
            var iterate11 = ( maxLength - blockLength ) / ( blockLength * 2 );

            var rule8 = new List<int>( );
            var rule11 = new List<int>( );
            var count = 0;

            for ( int i = 0 ; i <= iterate8 ; i++ )
            {
                rule8.Add(42);

                for ( int j = 0 ; j <= iterate11 ; j++ )
                {
                    rule11.Insert(0, 42);
                    rule11.Insert(rule11.Count, 31);

                    var toCheck = rule8.Select((r, i) =>
                    (rule: r, start: i * blockLength, end: i * blockLength + blockLength)).ToList( );

                    var index = toCheck.Last( ).end;
                    toCheck.AddRange(rule11.Select((r, i) =>
                    (rule: r, start: index + i * blockLength, end: index + i * blockLength + blockLength)).ToList( ));

                    count += messages.Where(m => m.Length == toCheck.Last( ).end)
                        .Count(m => toCheck.All(b => isBlockValid(m, valid[b.rule], b.start, b.end)));
                }
                rule11.Clear( );
            }
            return count.ToString( );
        }

        public List<List<string>> RecurseRule(List<string> rule)
        {
            if ( rule.All(s => s.AllLetter( )) ) return new List<List<string>> { rule };

            var current = rule.First(s => s.IsDigit( ));
            var index = rule.IndexOf(current);

            var next = rules[current];

            if ( next.Count == 1 && !next[0][0].IsDigit( ) )
            {
                rule.RemoveAt(index);
                rule.Insert(index, next[0][0]);
                return RecurseRule(rule);
            }

            if ( next.Count == 1 )
            {
                rule.RemoveAt(index);
                rule.InsertRange(index, next[0]);
                return RecurseRule(rule);
            }

            var r1 = new string[rule.Count];
            var r2 = new string[rule.Count];
            rule.CopyTo(r1);
            rule.CopyTo(r2);
            var cr1 = r1.ToList( );
            var cr2 = r2.ToList( );
            cr1.RemoveAt(index);
            cr1.InsertRange(index, next[0]);
            cr2.RemoveAt(index);
            cr2.InsertRange(index, next[1]);

            return RecurseRule(cr1).Concat(RecurseRule(cr2)).ToList( );
        }
    }

    public static class StringExtensions
    {
        public static bool IsDigit(this string s) => s.All(char.IsDigit);
        public static bool AllLetter(this string s) => s.All(char.IsLetter);
    }
}
