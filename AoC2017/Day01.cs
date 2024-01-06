using Common;
using Common.Extensions;

namespace AoC2017;

public class Day01 : Solution
{
	private CircularList<int> digits;
	public Day01(string file) : base(file) => InputToDigits( );

	public Day01(List<string> input) : base(input) => InputToDigits( );

	private void InputToDigits()
	{
		digits = Input.First( ).Aggregate(new CircularList<int>( ), (list, c) =>
		{
			list.Add(c.ToInt( ));
			return list;
		});
		digits.ResetHead( );
	}

	public override async Task<string> SolvePart1() => digits
		.Aggregate(0, (sum, i) =>
		{
			digits.MoveRight( );
			return i == digits.Current ? sum + i : sum;
		}).ToString( );


	public override async Task<string> SolvePart2() => digits.WithIndex()
		.Aggregate(0, (sum, i) =>
		{
			digits.SetHeadByIndex(i.idx);
			digits.MoveRight(digits.Count / 2);
			return i.Value == digits.Current ? sum + i.Value : sum;
		}).ToString( );
}
