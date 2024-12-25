namespace AoC2024;

public class Day25 : Solution
{
	private readonly List<List<int>> locks = new();
	private readonly List<List<int>> keys = new();

	public Day25(string file) : base(file)
	{
		Input.Chunk(7).Select(c => new Grid2d(c)).ForEach(g =>
		{
			var pins = new List<int>();
            Enumerable.Range(0, g.Width).ForEach(n => pins.Add(g.GetColumn(n).Count(c => c.Character == '#')));
            if(g.GetRow(0).All(c => c.Character == '#')) locks.Add(pins);
            else keys.Add(pins);
		});
			
	}
    
    public override async Task<string> SolvePart1( )
    {
	    var pairs = 0;
	    
	    foreach (var @lock in locks)
	    {
		    foreach (var key in keys)
		    {
			    var fit = @lock.Zip(key, (l, k) => l + k).All(p => p <= 7);
				if (fit)
					pairs++;
			}
	    }

    	return pairs.ToString();
    }

    public override async Task<string> SolvePart2( )
    {
    	return string.Empty;
    }

    
}
