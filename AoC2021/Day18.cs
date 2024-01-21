namespace AoC2021;

public class Day18 : Solution
{
	public Day18(string file) : base(file)
	{
		var number = "[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]";

		var matches = Regex.Matches(number, @"\[*\[(?<x>\d),(?<y>\d)\]\]*");
		var stack = new Stack<SnailNumber>();
		var root = new SnailNumber();
		foreach (Match match in matches)
		{
			var toOpen = match.Value.Count(c => c == '[');
			
			for (var i = 0; i < toOpen ; i++)
			{
				stack.Push(new SnailNumber( ));
			}

			var current = stack.Pop();
			current.X = new SnailNumber { Value = match.AsInt("x") };
			current.Y = new SnailNumber { Value = match.AsInt("y") };

			if (stack.Peek().HasLeft())
				stack.Peek().Y = current;
			else stack.Peek().X = current;

			var toClose = match.Value.Count(c => c == ']') - 1;
			
			for (var i = 1; i <= toClose; i++)
			{
				current = stack.Pop();
				if (stack.Count > 0)
				{
					if (stack.Peek( ).HasLeft( ))
						stack.Peek( ).Y = current;
					else
						stack.Peek( ).X = current;
				}
				else
				{
					root = current;
				}
			}
		};
		
	}
    
    public Day18(List<string> input) : base(input) { }

    public override async Task<string> SolvePart1( ) 
    {
    	return string.Empty;
    }

    public override async Task<string> SolvePart2( )
    {
    	return string.Empty;
    }

    public record SnailNumber
    {
        public int Value { get; set; }
        public SnailNumber X { get; set; }
        public SnailNumber Y { get; set; }

        public bool HasLeft() => X is not null;
    }
}
