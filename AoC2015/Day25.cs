using System.Collections.Generic;
using System.Threading.Tasks;
using Common;

namespace AoC2015
{
    public class Day25 : Solution
    {
        public Day25(List<string> input) : base(input) { }

        public override async Task<string> SolvePart1( )
        {
            var idx = (row: 1, column: 1);
            var pRow = 1;
            long code = 20151125;

            while (true)
            {
                if (idx.row == 2978 && idx.column == 3083) break;

                code = (code * 252533) % 33554393;

                if(idx.row == 1)
                {
                    pRow++;
                    idx = (pRow, 1);
                }
                else
                {
                    idx = (idx.row - 1, idx.column + 1);
                }
            }
            return code.ToString();
        }

        public override async Task<string> SolvePart2( ) => null;
    }
}