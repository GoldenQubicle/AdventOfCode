namespace AoC2024;

public class Day23 : Solution
{

	private readonly Dictionary<string, List<string>> network = new( );

	public Day23(string file) : base(file) => Input.ForEach(line =>
	{
		var parts = line.Split("-", StringSplitOptions.TrimEntries);

		if (!network.TryAdd(parts[0], [parts[1]]))
			network[parts[0]].Add(parts[1]);

		if (!network.TryAdd(parts[1], [parts[0]]))
			network[parts[1]].Add(parts[0]);
	});


	public override async Task<string> SolvePart1()
	{
		var groups = new HashSet<Group>( );
		foreach (var pc1 in network)
		{
			foreach (var pc2 in pc1.Value)
			{
				var cons = network[pc2].Where(pc1.Value.Contains).ToList( );
				if (cons.Count == 0)
					continue;
				groups.AddRange(cons.Select(pc3 => new Group(pc1.Key, pc2, pc3)));
			}
		}

		return groups
			.Count(g => g.Pc1.StartsWith('t') || g.Pc2.StartsWith('t') || g.Pc3.StartsWith('t'))
			.ToString( );
	}

	public override async Task<string> SolvePart2() => network.Keys
		.Aggregate(new HashSet<string>( ), (groups, pc) =>
		{
			var group = network[pc]
				.Select(n => network[n].Expand(n))
				.Expand(network[pc])
				.Aggregate(new Dictionary<string, int>( ), (dic, pc) =>
				{
					pc.ForEach(c => dic.AddTo(c, i => i + 1));
					return dic;
				})
				.GroupBy(v => v.Value).First( );

			if (group.Count( ) != group.Key)
				return groups;

			groups.Add(string.Join(",", group.ToList( ).Select(v => v.Key).Order( )));

			return groups;
		}).MaxBy(g => g.Length);


	private record Group(string Pc1, string Pc2, string Pc3)
	{
		public virtual bool Equals(Group? other) =>
			(other.Pc1 == Pc1 && other.Pc2 == Pc2 && other.Pc3 == Pc3) ||
			(other.Pc1 == Pc1 && other.Pc2 == Pc3 && other.Pc3 == Pc2) ||
			(other.Pc1 == Pc2 && other.Pc2 == Pc3 && other.Pc3 == Pc1) ||
			(other.Pc1 == Pc2 && other.Pc2 == Pc1 && other.Pc3 == Pc3) ||
			(other.Pc1 == Pc3 && other.Pc2 == Pc1 && other.Pc3 == Pc2) ||
			(other.Pc1 == Pc3 && other.Pc2 == Pc2 && other.Pc3 == Pc1);

		public override int GetHashCode() =>
			Pc1.GetHashCode( ) ^ Pc2.GetHashCode( ) ^ Pc3.GetHashCode( );

	};
}
