namespace AoC2024;

public class Day22 : Solution
{
	private readonly List<long> secrets;

	public Day22(string file) : base(file) => secrets = Input.Select(long.Parse).ToList( );


	public override async Task<string> SolvePart1()
	{
		for (var i = 0 ;i < secrets.Count ;i++)
		{
			Enumerable.Range(0, 2000).ForEach(_ =>
			{
				secrets[i] = GetNextSecret(secrets[i]);
			});
		}

		return secrets.Sum( ).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var buyers = secrets.WithIndex( ).ToList( );
		var tracker = new Dictionary<(long, long, long, long), Dictionary<int, long>>( );

		foreach (var buyer in buyers)
		{
			var secret = buyer.Value;
			var changes = new List<long>( );
			var previous = secret % 10;
			Enumerable.Range(0, 2000).ForEach(n =>
			{
				secret = GetNextSecret(secret);
				var current = secret % 10;
				changes.Add(current - previous);
				previous = current;

				if (changes.Count < 4)
					return;
				if (changes.TakeLast(2).SequenceEqual([0, 0]))
					return;

				var key = (changes[n - 3], changes[n - 2], changes[n - 1], changes[n]);

				if (!tracker.TryAdd(key, new( ) { { buyer.idx, current } }))
					tracker[key].TryAdd(buyer.idx, current);
			});
		}

		return tracker.Values.Select(t => t.Values.Sum( )).Max( ).ToString( );
	}

	public long GetNextSecret(long secret)
	{
		secret = Prune(Mix(secret, secret * 64L));
		secret = Prune(Mix(secret, secret / 32));
		secret = Prune(Mix(secret, secret * 2048));
		return secret;
	}

	public long Prune(long n) => n % 16777216L;

	public long Mix(long n1, long n2) => n1 ^ n2;
}
