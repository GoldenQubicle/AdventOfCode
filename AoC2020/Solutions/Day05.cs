using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Solutions
{
    public class Day05 : Solution<int>
    {
        public Day05(string file) : base(file) { }

        public override int SolvePart1( ) => Input.Select(PartitionString).Max(p => p.id);

        public override int SolvePart2( )
        {
            var ids = Input.Select(PartitionString).OrderBy(p => p.id).Select(p => p.id).ToList( );
            return ids
                .Where(id => !ids.Contains(id + 1) || !ids.Contains(id - 1))
                .ToArray( )[1..2]
                .First( ) + 1;
        }

        public (int row, int col, int id) PartitionString(string s)
        {
            var row = s.Substring(0, 7);
            var col = s.Substring(7, 3);
            var rowPart = (min: 0, max: 127, div: 128);
            var colPart = (min: 0, max: 7, div: 8);
            for ( int i = 0 ; i < row.Length ; i++ )
            {
                rowPart = Partition(row[i], rowPart);
            }
            for ( int i = 0 ; i < col.Length ; i++ )
            {
                colPart = Partition(col[i], colPart);
            }
            return (rowPart.min, colPart.min, ( rowPart.min * 8 ) + colPart.min);
        }

        public (int min, int max, int div) Partition(char c, (int min, int max, int div) d) =>
              c switch
              {
                  'F' or 'L' => (d.min, d.max - d.div / 2, d.div / 2),
                  'B' or 'R' => (d.min + d.div / 2, d.max, d.div / 2)
              };


    }
}
