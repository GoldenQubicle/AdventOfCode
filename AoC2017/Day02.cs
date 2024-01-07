using Common;
using Common.Extensions;

namespace AoC2017;


public class Day02 : Solution
{
	private List<List<float>> SpreadSheet;

	public Day02(string file) : base(file) => SpreadSheet = Input.Select(l => l.Split('\t').Select(float.Parse).ToList( )).ToList( );


	public override async Task<string> SolvePart1() => SpreadSheet
		.Select(l => l.Max( ) - l.Min( )).Sum( ).ToString( );


	public override async Task<string> SolvePart2() => SpreadSheet
		.Select(r => r.OrderDescending( ).WithIndex( ).ToList( ))
		.Aggregate(0f, (sumOverall, row) => sumOverall + row.SkipLast(1)
			.Aggregate(0f, (sum, entry) => sum + Enumerable.Range(entry.idx + 1, row.Count - entry.idx - 1)
				.Aggregate(0f, (s, idx) => float.IsInteger(entry.Value / row[idx].Value) ? s + entry.Value / row[idx].Value : s)
		)).ToString( );

}
