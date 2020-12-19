using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Solutions
{
    public class Day19 : Solution<int>
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

        public override int SolvePart1( ) => Solve( );

        public override int SolvePart2( )
        {
            //  8: 42     | 42 8
            // 11: 42 31  | 42 11 31
            rules["8"].Clear( );
            rules["8"].AddRange(new List<List<string>> { new List<string> { "42" }, new List<string> { "42", "8" } });
            rules["11"].Clear( );
            rules["11"].AddRange(new List<List<string>> { new List<string> { "42", "31" }, new List<string> { "42", "11", "31" } });

            //valid message can now be of different length, since the rules have infinite solutions!
            //thus, what we need to do, per rule, is generate a new iteration, and see if those match the message..?!

            var r = RecurseRule(new List<string> { rules["0"][0][0] });
          

            return Solve();
        }

        private int Solve( )
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
            return messages.Count(m => m.Length == startIndex);
        }

        public List<List<string>> RecurseRule(List<string> rule)
        {      
            if ( rule.All(s => s.AllLetter( )) ) return new List<List<string>> { rule };

            var current = rule.First(s => s.IsDigit( ));
            var index = rule.IndexOf(current);

            var next = rules[current];

            if ( next.Count == 2 && next[1].Count == 2 && next[1][1].Equals("8") )
                next[1] = next[1].Take(1).ToList();

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
