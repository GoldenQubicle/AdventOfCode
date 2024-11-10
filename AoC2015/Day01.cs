namespace AoC2015
{
	public class Day01 : Solution
	{
		public Day01(string file) : base(file) { }

		public Day01(List<string> input) : base(input) { }


		public override async Task<string> SolvePart1() => Input[0].Aggregate(0, (sum, c) =>
			c switch
				{
					'(' => sum += 1,
					')' => sum -= 1
				}
		).ToString( );

		public override async Task<string> SolvePart2()
		{
			var floor = 0;
			for (var i = 0 ;i < Input[0].Length ;i++)
			{
				floor = Input[0][i] switch
				{
					'(' => floor += 1,
					')' => floor -= 1
				};
				if (floor == -1)
					return (i + 1).ToString( );
			}
			return string.Empty;
		}
	}
}