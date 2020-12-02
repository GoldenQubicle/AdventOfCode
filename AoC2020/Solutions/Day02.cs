using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day02 : Solution
    {
        private List<Policy> Policies { get; }
        public Day02(string file) : base(file) =>
            Policies = Input.Select(i => i.Split(":"))
            .Select(parts => new Policy
            {
                Digit1 = int.Parse(parts[0].Remove(parts[0].Length - 1, 1).Split('-')[0]),
                Digit2 = int.Parse(parts[0].Remove(parts[0].Length - 1, 1).Split('-')[1]),
                Letter = parts[0].Last( ),
                Password = parts[1].Trim( )
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
