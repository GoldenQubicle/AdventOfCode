using System.Text;
using Common;

namespace AoC2023;

public class Day06 : Solution
{
	private List<(int t, int d)> races;
	public Day06(string file) : base(file)
	{
		var time = Input[0]
			.Split(':', StringSplitOptions.RemoveEmptyEntries| StringSplitOptions.TrimEntries)[1]
			.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
			.Select(int.Parse).ToList();
		var dist = Input[1]
			.Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
			.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
			.Select(int.Parse).ToList( );

		races = time.Zip(dist, (t, d) => (t, d)).ToList();
	}
        

	public override string SolvePart1( )
	{
		var dist = races.Select(r =>
		{
			var wins = 0;

			for (var i = 0; i < r.t; i++)
			{
				if ((r.t - i) * i > r.d)
					wins++;
			}

			return wins;
		}).ToList();

		return dist.Skip(1).Aggregate(dist[0], (s, i) => s* i).ToString();
	}

	public override string SolvePart2( )
	{
		var time = long.Parse(races.Aggregate(new StringBuilder(), (sb, r) => sb.Append(r.t)).ToString());
		var dist = long.Parse(races.Aggregate(new StringBuilder( ), (sb, r) => sb.Append(r.d)).ToString( ));

		var wins = 0L;

		for (var i = 0 ;i < time ;i++)
		{
			if ((time - i) * i > dist)
				wins++;
		}

		return wins.ToString();

	}
}