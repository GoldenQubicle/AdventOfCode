using System.Collections.Generic;
using System.Linq;
using Common;

namespace AoC2015
{
    public class Day02 : Solution
    {
        public Day02(string file) : base(file, "\n") { }

        public override string SolvePart1( ) => Input
            .Select(line => line.Split('x').Select(int.Parse).ToList( ))
            .Select(list => new List<int>
            {
                2 * list[0] * list[1],
                2 * list[1] * list[2],
                2 * list[2] * list[0],
            }).Aggregate(0L, (sum, list) =>
            {
                sum += list.Min( ) / 2;
                sum += list.Sum( );
                return sum;
            }).ToString( );


        public override string SolvePart2( ) => Input
            .Select(line => line.Split('x').Select(int.Parse).ToList( ))
            .Aggregate(0l, (sum, list) =>
            {
                var order = list.OrderBy(i => i).ToList( );
                sum += ( order[0] * 2 + order[1] * 2 + list[0] * list[1] * list[2] );
                return sum;
            }).ToString( );
    }
}