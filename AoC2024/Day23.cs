namespace AoC2024;

public class Day23 : Solution
{

	private readonly Dictionary<string, List<string>> network = new( );

	public Day23(string file) : base(file)
	{

		Input.ForEach(line =>
		{
			var parts = line.Split("-", StringSplitOptions.TrimEntries);
			if (!network.TryAdd(parts[0], [parts[1]]))
				network[parts[0]].Add(parts[1]);
			if (!network.TryAdd(parts[1], [parts[0]]))
				network[parts[1]].Add(parts[0]);
		});

	}


	public override async Task<string> SolvePart1()
	{
		var set = new HashSet<Group>( );
		foreach (var pc1 in network)
		{
			foreach (var pc2 in pc1.Value)
			{
				var pc3 = network[pc2].Where(pc1.Value.Contains).ToList( );
				if (pc3.Count == 0)
					continue;
				set.AddRange(pc3.Select(p => new Group(pc1.Key, pc2, p)).ToList( ));
			}
		}

		var groups = set.Where(s => s.Pc1.StartsWith('t') || s.Pc2.StartsWith('t') || s.Pc3.StartsWith('t'))
			.ToHashSet();

		return groups.Count().ToString();
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	private record Group(string Pc1, string Pc2, string Pc3)
	{
		public virtual bool Equals(Group? other)
		{
			return
				(other.Pc1 == Pc1 && other.Pc2 == Pc2 && other.Pc3 == Pc3)
				|| (other.Pc1 == Pc1 && other.Pc2 == Pc3 && other.Pc3 == Pc2)
				|| (other.Pc1 == Pc2 && other.Pc2 == Pc3 && other.Pc3 == Pc1)
				|| (other.Pc1 == Pc2 && other.Pc2 == Pc1 && other.Pc3 == Pc3)
				|| (other.Pc1 == Pc3 && other.Pc2 == Pc1 && other.Pc3 == Pc2)
				|| (other.Pc1 == Pc3 && other.Pc2 == Pc2 && other.Pc3 == Pc1);
		}

		public override int GetHashCode()
		{
			return Pc1.GetHashCode() ^ Pc2.GetHashCode() ^ Pc3.GetHashCode();
		}
	};
}
