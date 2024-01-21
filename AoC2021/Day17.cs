namespace AoC2021;

public class Day17 : Solution
{
	private static (int xmin, int xmax, int ymin, int ymax) _targetArea;

	public Day17(string file) : base(file)
	{
		var values = Regex.Matches(Input.First( ), @"-?\d+")
			.Select(m => int.Parse(m.Value)).ToList( );

		_targetArea = (xmin: values[0], xmax: values[1], ymin: values[2], ymax: values[3]);

	}

	public override async Task<string> SolvePart1() => Enumerable.Range(0, _targetArea.xmin)
		.SelectMany(x => Enumerable.Range(0, Math.Abs(_targetArea.ymin))
			.Select(y => DoesHitTargetArea(x, y)))
			.Where(r => r.hit)
			.MaxBy(r => r.maxY).maxY.ToString( );


	public override async Task<string> SolvePart2() => Enumerable.Range(0, _targetArea.xmax + 1)
		.SelectMany(x => Enumerable.Range(_targetArea.ymin, Math.Abs(_targetArea.ymin) * 2)
			.Select(y => DoesHitTargetArea(x, y))).Count(r => r.hit).ToString( );

	
	public static (bool hit, float maxY) DoesHitTargetArea(float x, float y)
	{
		var pos = Vector2.Zero;
		var vel = new Vector2(x, y);
		var steps = 0;
		var maxY = 0f;

		while (!InTargetArea(pos))
		{
			steps++;
			pos += vel;

			maxY = pos.Y > maxY ? pos.Y : maxY;

			if (vel.X > 0) vel.X--;
			if (vel.X < 0) vel.X++;

			vel.Y--;

			if (pos.X > _targetArea.xmax || (pos.Y < 0 && pos.Y < _targetArea.ymin))
				return (false, maxY);
		}

		return (true, maxY);
	}

	private static bool InTargetArea(Vector2 p) =>
		p.X >= _targetArea.xmin && p.X <= _targetArea.xmax &&
		p.Y >= _targetArea.ymin && p.Y <= _targetArea.ymax;

	
}
