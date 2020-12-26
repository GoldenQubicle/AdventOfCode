using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2020
{
    public class Day02 : Solution<int>
    {
        private List<Policy> Policies { get; }
        public Day02(string file) : base(file) =>
            Policies = Input
            .Select(i => i.Split(":"))
            .Select(parts => (parts[0].Split('-'), parts[1].Trim( )))
            .Select(t => new Policy
            {
                Digit1 = int.Parse(t.Item1[0]),
                Digit2 = int.Parse(t.Item1[1].Where(char.IsDigit).ToArray()),
                Letter = t.Item1[1].Last( ),
                Password = t.Item2
            }).ToList( );


        public override int SolvePart1( ) => Policies.Count(p =>
                p.Password.Count(s => s == p.Letter) >= p.Digit1 &&
                p.Password.Count(s => s == p.Letter) <= p.Digit2);

        public override int SolvePart2( ) => Policies.Count(p =>
                ( p.Password[p.Digit1 - 1] == p.Letter && p.Password[p.Digit2 - 1] != p.Letter ) ||
                ( p.Password[p.Digit1 - 1] != p.Letter && p.Password[p.Digit2 - 1] == p.Letter ));

        private struct Policy
        {
            public int Digit1 { get; init; }
            public int Digit2 { get; init; }
            public char Letter { get; init; }
            public string Password { get; init; }
        }
    }
}
