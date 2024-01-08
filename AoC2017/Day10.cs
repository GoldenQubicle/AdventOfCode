namespace AoC2017;

public class Day10 : Solution
{
	public static CircularList<int> List = Enumerable.Range(0, 256)
		.Aggregate(new CircularList<int>(), (list, i) =>
	    {
	    	list.Add(i);
	    	return list;
	    });

	private readonly List<int> lengths;

	public Day10(string file) : base(file) => lengths = Input.First()
		.Split(',', StringSplitOptions.TrimEntries)
		.Select(int.Parse).ToList();
    

    public override async Task<string> SolvePart1( ) 
    {
		List.ResetHead();
		var skipSize = 0;
		foreach (var length in lengths)
		{
			var sub = List.TakeAt(length).Reverse();
			List.ReplaceRange(sub);
			List.MoveRight(length + skipSize);
			skipSize++;
		}

		List.ResetHead();
		var e1 = List.Current;
		List.MoveRight();
		var e2 = List.Current;
		return (e1 * e2).ToString();
    }

    public override async Task<string> SolvePart2( )
    {
    	return string.Empty;
    }
}
