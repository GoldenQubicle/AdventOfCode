using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2020
{
    public class Day16 : Solution
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

        public override string SolvePart1( )
        {
            var validNumbers = fields.SelectMany(f => f.valid).ToList( );
            return tickets.Sum(ticket => ticket.Sum(n => !validNumbers.Contains(n) ? n : 0)).ToString( );
        }

        public override string SolvePart2( )
        {
            var fieldOrder = new List<(string name, int index)>( );
            var validNumbers = fields.SelectMany(f => f.valid).ToList( );
            var validTickets = tickets.Where(ticket => ticket.All(n => validNumbers.Contains(n))).ToList( );

            while ( fields.Count > 0 )
            {
                for ( int i = 0 ; i < myTicket.Count ; i++ )
                {
                    if ( fieldOrder.Any(f => f.index == i) ) continue;

                    var position = validTickets.Select(t => t[i]);
                    var matches = fields.Where(f => position.All(n => f.valid.Contains(n)));

                    if ( matches.Count() == 1 )
                    {                        
                        fieldOrder.Add((matches.First().name, i));
                        fields.Remove(matches.First());
                    }
                }
            }

            return fieldOrder.Aggregate(1L, (sum, field) => 
                field.name.StartsWith("departure") ? sum *= myTicket[field.index] : sum).ToString( );
        }
    }
}
