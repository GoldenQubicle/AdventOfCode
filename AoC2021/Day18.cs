using System.Collections;
using System.Reflection.Metadata;

namespace AoC2021;

public class Day18 : Solution
{
	public Day18(string file) : base(file)
	{
		var number = "[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]";

		var matches = Regex.Matches(number, @"(?<x>\[+\d+)*(?<y>\d\]{1,})*");
		var stack = new Stack<SnailNumber>( );
		var root = new SnailNumber( );

		foreach (Match match in matches)
		{
			if (string.IsNullOrWhiteSpace(match.Value))
				continue;

			if (match.Groups["x"].Success)
			{
				var toOpen = match.Value.Count(c => c == '[');

				for (var i = 0 ;i < toOpen ;i++)
				{
					stack.Push(new SnailNumber( ));
				}

				stack.Peek( ).X = new SnailNumber { Value = match.AsInt("x") };
				stack.Peek().X.Parent = stack.Peek();
			}

			if (match.Groups["y"].Success)
			{
				stack.Peek( ).Y = new SnailNumber { Value = match.AsInt("y") };
				stack.Peek( ).Y.Parent = stack.Peek( );
				var toClose = match.Value.Count(c => c == ']');

				for (var i = 1 ;i <= toClose ;i++)
				{
					var current = stack.Pop( );
					
					if (stack.Count > 0)
					{
						if (current.Parent == null)
							current.Parent = stack.Peek();

							if (stack.Peek( ).HasX)
							stack.Peek( ).Y = current;
						else
							stack.Peek( ).X = current;
					}
					else
					{
						root = current;
					}
				}
			}
		};

		var r = root.TryGetExplodingPair();
	}

	public Day18(List<string> input) : base(input) { }

	public override async Task<string> SolvePart1()
	{
		return string.Empty;
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	public record SnailNumber
	{
		public int Value { get; set; }
		public SnailNumber Parent { get; set; }
		public SnailNumber X { get; set; }
		public SnailNumber Y { get; set; }
		public int Depth => Parent is null ? 1 : Parent.Depth + 1;
		public bool HasX => X is not null;
		public bool HasY => Y is not null;
		public bool HasValue => X == null && Y == null;
		public bool CanExplode => Depth > 4;


		public (bool, SnailNumber) TryGetExplodingPair()
		{
			var current = this;
			
			while (current.HasX)
			{
				if (current.CanExplode)
					return (true, current);
				
				current = current.X;
			}

			current = this;

			while (current.HasY)
			{
				if (current.CanExplode)
					return (true, current);

				current = current.Y;
			}

			return (false, null);
		}

		public SnailNumber Add(SnailNumber x, SnailNumber y)
		{
			var result = new SnailNumber { X = x, Y = y };
			result.X.Parent = result;
			result.Y.Parent = result;

			return result;
		}

		
	}
}
