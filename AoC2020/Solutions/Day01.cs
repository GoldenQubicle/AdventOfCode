using System.Linq;

namespace AoC2020
{
    public class Day01 : Solution
    {

        public Day01(string day) : base(day)
        {
        }

        public override int SolvePart1( ) => Input.Select(int.Parse)
            .Where(i => Input.Contains(( 2020 - i ).ToString( )))
            .Select(i => i * ( 2020 - i )).First( );


        public override int SolvePart2( )
        {
            var input = Input.Select(int.Parse);

            foreach ( var number in input )
            {
                var lookingFor1 = 2020 - number;
                var subset = input.Where(i => i < lookingFor1 && i != number);

                foreach ( var other in subset )
                {
                    var lookingFor2 = lookingFor1 - other;
                    if ( input.Contains(lookingFor2) )
                    {
                        return number * other * lookingFor2;
                    }
                }
            }
            return 0;
        }
    }
}
