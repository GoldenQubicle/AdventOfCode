namespace AoC2017;

public class Day07 : Solution
{
	private readonly Dictionary<string, Program> programs;

	public Day07(string file) : base(file)
	{
		var t = Input
			.Select(l => Regex.Match(l, @"(?<name>[a-z]+)\s\((?<weight>\d+)\)( -> )?(?<connections>.*)?"))
			.Select(m =>
			{
				var c = m.AsString("connections");
				return (p: new Program(m.AsString("name"), m.AsInt("weight")),
					up: (c == string.Empty ? new( ) : c.Split(",").Select(s => s.Trim( )).ToList( )));
			}).ToDictionary(t => t.p.Name, t => t);

		t.Values.ForEach(p => p.up.ForEach(name => p.p.Next.Add(t[name].p)));
		programs = t.ToDictionary(p => p.Key, t => t.Value.p);
	}


	public override async Task<string> SolvePart1() => GetRoot( );


	public override async Task<string> SolvePart2()
	{
		var root = programs[GetRoot( )];
		var current = (n: root.Name, w: 0);

		while (true)
		{
			var next = programs[current.n].Next
				.Select(p => (n: p.Name, w: p.GetWeight( )))
				.GroupBy(t => t.w)
				.FirstOrDefault(g => g.Count( ) == 1);

			if (next == default)
				break;

			current = next.First( );
		}

		var weights = root.Next
			.Select(p => p.GetWeight( ))
			.Distinct( )
			.OrderDescending( ).ToList( );

		return (programs[current.n].Weight - (weights[0] - weights[1])).ToString( );
	}

	private string GetRoot()
	{
		var current = programs.Values.First(p => p.Next.IsEmpty( ));
		while (true)
		{
			var next = programs.Values.FirstOrDefault(p => p.Next.Contains(current));
			if (next == default)
				return current.Name;
			current = next;
		}
	}

	private record Program(string Name, int Weight)
	{
		public List<Program> Next { get; } = new( );

		public int GetWeight() => Next.IsEmpty( ) ? Weight : Weight + Next.Sum(p => p.GetWeight( ));
	}
}
