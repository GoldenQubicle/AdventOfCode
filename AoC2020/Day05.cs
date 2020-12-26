using System.Linq;
using Common;

namespace AoC2020
{
    public class Day05 : Solution<int>
    {
        public Day05(string file) : base(file) { }

        public override int SolvePart1( ) => Input.Select(PartitionString).Max(p => p.id);

        public override int SolvePart2( ) => 
                Enumerable.Range(0, Input.Select(PartitionString).Max(p => p.id))
                .Except(Input.Select(PartitionString).Select(p => p.id)).Max( );

        public (int row, int col, int id) PartitionString(string s)
        {
            var rows = s.Substring(0, 7);
            var cols = s.Substring(7, 3);
            var rowPart = (min: 0, max: 127, div: 128);
            var colPart = (min: 0, max: 7, div: 8);
            for ( int i = 0 ; i < rows.Length ; i++ )
            {
                rowPart = Partition(rows[i], rowPart);
            }
            for ( int i = 0 ; i < cols.Length ; i++ )
            {
                colPart = Partition(cols[i], colPart);
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
