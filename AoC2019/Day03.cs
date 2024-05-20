namespace AoC2019;

public class Day03 : Solution
{
	private readonly List<List<Segment>> wires;

	public Day03(string file) : base(file, "\n") => wires = Input.Select(l =>
	{
		return l.Split(",").WithIndex( ).Aggregate(new List<Segment>( ), (wire, s) =>
		{
			var start = wire.Any( ) ? wire.Last( ).End : Vector2.Zero;
			var end = s.Value[0] switch
			{
				'R' => start with { X = start.X + s.Value.ToInt( ) },
				'L' => start with { X = start.X - s.Value.ToInt( ) },
				'U' => start with { Y = start.Y + s.Value.ToInt( ) },
				'D' => start with { Y = start.Y - s.Value.ToInt( ) },
			};
			wire.Add(new Segment(s.idx, start, end));
			return wire;
		}).ToList( );
	}).ToList( );


	public override async Task<string> SolvePart1() => wires[0]
		.SelectMany(s1 => wires[1].Select(s2 => s2.TryGetIntersection(s1)))
		.Where(intersect => intersect != Vector2.Zero)
		.Min(intersect => GetManhattanDistance(Vector2.Zero, intersect)).ToString( );


	public override async Task<string> SolvePart2() => wires[0]
			.SelectMany(s1 => wires[1].Select(s2 => (s1, s2, result: s2.TryGetIntersection(s1))))
			.Where(intersect => intersect.result != Vector2.Zero)
			.Select(intersect =>
			{
				var l1 = wires[0][..intersect.s1.Idx].Sum(s => s.Length)
						 + GetManhattanDistance(intersect.s1.Start, intersect.result);

				var l2 = wires[1][..intersect.s2.Idx].Sum(s => s.Length)
						 + GetManhattanDistance(intersect.s2.Start, intersect.result);

				return l1 + l2;
			}).Min( ).ToString( );

	public record Segment(int Idx, Vector2 Start, Vector2 End)
	{
		public bool IsHorizontal() => Start.X == End.X;

		public bool IsVertical() => Start.Y == End.Y;

		public int Length => GetManhattanDistance(Start, End);

		public Vector2 TryGetIntersection(Segment s)
		{
			if (s.IsHorizontal( ) && IsHorizontal( ) || s.IsVertical( ) && IsVertical( ))
				return Vector2.Zero;

			if (s.IsHorizontal( ) && IsXInRange(s, this) && IsYInRange(s, this))
				return new Vector2(s.Start.X, Start.Y);

			if (IsXInRange(this, s) && IsYInRange(this, s))
				return new Vector2(Start.X, s.Start.Y);

			return Vector2.Zero;
		}

		private static bool IsXInRange(Segment h, Segment v) =>
			h.Start.X > v.Start.X && h.Start.X < v.End.X || h.Start.X > v.End.X && h.Start.X < v.Start.X;

		private static bool IsYInRange(Segment h, Segment v) =>
			h.Start.Y > v.Start.Y && h.End.Y < v.Start.Y || h.End.Y > v.Start.Y && h.Start.Y < v.Start.Y;

	}
}
