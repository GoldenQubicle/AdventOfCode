using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day16 : Solution<long>
    {
        private readonly List<(string name, List<int> valid)> fields = new( );
        private readonly List<List<int>> tickets = new( );
        private readonly List<int> myTicket = new( );
        public Day16(string file) : base(file, "\r\n\r\n")
        {
            var intermediate = Input.Select(i => i.Split("\r\n")).ToArray( );
            fields = intermediate[0]
                .Select(i => i.Split(":"))
                .Select(i => (name: i[0], range: i[1].Split(" or ").Select(r => r.Split("-"))))
                .Select(i => (name: i.name, range: i.range.SelectMany(r =>
                Enumerable.Range(int.Parse(r[0]), int.Parse(r[1]) - int.Parse(r[0]) + 1).ToList( )).ToList( ))).ToList( );

            tickets = intermediate[2].Skip(1).Select(i => i.Split(",").Select(int.Parse).ToList( )).ToList( );

            myTicket = intermediate[1].Skip(1).SelectMany(i => i.Split(",")).Select(int.Parse).ToList( );
        }

        public override long SolvePart1( )
        {
            var allValid = fields.SelectMany(f => f.valid).ToList( );
            return tickets.Sum(t => t.Sum(n => !allValid.Contains(n) ? n : 0));
        }

        public override long SolvePart2( )
        {
            var allValid = fields.SelectMany(f => f.valid).ToList( );
            var validTickets = tickets.Where(t => t.All(n => allValid.Contains(n))).ToList( );

            validTickets.Add(myTicket);

            var fieldOrder = new List<(string name, int index)>( );

            while ( fields.Count > 0 )
            {
                for ( int i = 0 ; i < validTickets[0].Count ; i++ )
                {
                    if ( fieldOrder.Any(f => f.index == i) ) continue;

                    var tNum = validTickets.Select(t => t[i]).ToList( );
                    var matches = fields.Where(f => tNum.All(n => f.valid.Contains(n))).ToList( );

                    if ( matches.Count == 1 )
                    {
                        fieldOrder.Add((matches[0].name, i));
                        fields.Remove(matches[0]);
                    }
                }
            }

            return fieldOrder.Aggregate(1L, (sum, field) => field.name.StartsWith("departure") ? sum *= myTicket[field.index] : sum);
        }
    }
}
