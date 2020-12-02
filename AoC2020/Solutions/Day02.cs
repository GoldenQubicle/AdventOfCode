using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day02 : Solution
    {
        public List<Policy> Policies { get; }
        public Day02(string file) : base(file)
        {
            Policies = Input.Select(i =>
            {
                var parts = i.Split(":");
                var password = parts[1].Trim( );
                var rule = parts[0];
                var letter = rule.Last( );
                var digits = rule.Remove(rule.Length - 1, 1).Split('-');

                return new Policy
                {
                    Digit1 = int.Parse(digits[0]),
                    Digit2 = int.Parse(digits[1]),
                    Letter = letter,
                    Password = password
                };

            }).ToList( );
        }

        public override int SolvePart1( ) => Policies.Count(p =>
        {
            var occurance = p.Password.Count(s => s == p.Letter);
            return occurance >= p.Digit1 && occurance <= p.Digit2;
        });


        public override int SolvePart2( ) => Policies.Count(p =>
            ( p.Password[p.Digit1 - 1] == p.Letter && p.Password[p.Digit2 - 1] != p.Letter ) ||
            (p.Password[p.Digit1 - 1] != p.Letter && p.Password[p.Digit2 - 1] == p.Letter));
    }

    public class Policy
    {
        public int Digit1 { get; set; }
        public int Digit2 { get; set; }
        public char Letter { get; set; }
        public string Password { get; set; }
    }
}
