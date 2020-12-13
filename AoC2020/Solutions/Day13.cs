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
        private List<int> ids;
        public Day13(string file) : base(file)
        {
            timestap = int.Parse(Input[0]);
            ids = Input[1].Split(',').Where(s => !s.Equals("x")).Select(int.Parse).ToList( );
        }
        public override long SolvePart1( )
        {
            return ids.Select(id => (id: id, wait: MathF.Ceiling(( float ) timestap / id) * id))
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
            long increment = ids[0];
            var id = 0;

            while ( true )
            {
                var found = true;
                for ( var i = id + 1 ; i < ids.Count ; i++ )
                {
                    if ( CanDepart(ids[i], startTime, departure[i]) )
                    {
                        increment *= ids[i];
                        id = i;
                    }
                    else
                    {
                        found = false;
                        break;
                    }
                }
                if ( found )
                {
                    break;
                }
                startTime += increment;
            }
            return startTime;
        }

        private bool CanDepart(int id, long startTime, int departsIn)
        {
            var timeLeft = ( int ) ( startTime % id );
            if ( timeLeft > 0 )
            {
                timeLeft = id - timeLeft;
            }
            return timeLeft == departsIn;
        }
    }
}
