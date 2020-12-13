using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Solutions
{
    public class Day13 : Solution<long>
    {
        private int timestap;
        private List<int> buses;
        public Day13(string file) : base(file)
        {
            timestap = int.Parse(Input[0]);
            buses = Input[1].Split(',').Where(s => !s.Equals("x")).Select(int.Parse).ToList( );
        }
        public override long SolvePart1( )
        {
            return buses.Select(id => (id: id, wait: MathF.Ceiling(( float ) timestap / id) * id))
                .OrderBy(id => id.wait)
                .Select(id => ( int ) ( id.wait - timestap ) * id.id)
                .First( );
        }

        public override long SolvePart2( )
        {
            var departure = Input[1].Split(',')
                .Select((b, i) => (b, i))
                .Where(b => !b.b.Equals("x"))
                .Select(b => b.i % int.Parse(b.b))
                .ToList( );

            long startTime = 0;
            long increment = buses[0];
            var current = 1;

            while ( current < buses.Count )
            {
                if ( buses[current] - ( startTime % buses[current] ) == departure[current] )
                {
                    increment *= buses[current];
                    current += 1;

                    if ( current == buses.Count )
                        break;
                }
                startTime += increment;
            }
            return startTime;
        }
    }
}
