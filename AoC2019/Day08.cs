namespace AoC2019;

public class Day08 : Solution
{
	private readonly Dictionary<int, List<int[ ]>> layers;
	public static int Width { get; set; } = 25;
	public static int Height { get; set; } = 6;

	public Day08(string file) : base(file) => layers = Input.First( )
		.Select(c => c.ToInt( ))
		.Chunk(Width * Height)
		.WithIndex( )
		.ToDictionary(a => a.idx, a => a.Value.Chunk(Width).ToList( ));


	public override async Task<string> SolvePart1()
	{
		var layer = layers.MinBy(l => l.Value.Sum(r => r.Count(i => i == 0))).Value;
		return (layer.Sum(r => r.Count(i => i == 1)) * layer.Sum(r => r.Count(i => i == 2))).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var r = 0;
		var c = 0;
		var idx = 0;
		var current = layers[idx];
		var finalImage = new Grid2d(Width, Height);

		while (true)
		{
			while (current[r][c] == 2)
			{
				current = layers[idx++];
			}

			finalImage[c, r].Character =  current[r][c] == 0 ? '.' : '#';
			c++;
			idx = 0;
			current = layers[idx];
			
			if (c == Width)
			{
				c = 0;
				r++;
			}

			if(r == Height)
				break;
		}

		return $"\n{finalImage}";
	}
}
