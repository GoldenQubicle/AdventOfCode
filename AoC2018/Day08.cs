using Common;

namespace AoC2018
{
	public class Day08 : Solution
	{

		private readonly Node root;

		public Day08(string file) : base(file)
		{
			var license = Input[0].Split(" ").Select(int.Parse).ToList( );
			root = new Node(license[0], license[1]);

			var stack = new Stack<Node>( );
			stack.Push(root);

			var idx = 2;

			while (idx < license.Count - 1)
			{
				var current = stack.Peek( );

				if (current.AddChild( ))
				{
					var header = license.Slice(idx, 2);
					var child = new Node(header[0], header[1]);
					current.Children.Add(child);
					stack.Push(child);
					idx += 2;
					continue;
				}

				current.MetaData = license.Slice(idx, current.MetaDataCount);
				idx += current.MetaDataCount;
				stack.Pop( );
			}
		}


		public override string SolvePart1() => root.Resolve(isPart1: true).ToString( );


		public override string SolvePart2() => root.Resolve(isPart1: false).ToString( );


		public record Node(int ChildCount, int MetaDataCount)
		{
			public List<Node> Children { get; } = new( );
			public List<int> MetaData { get; set; }

			public bool AddChild() => Children.Count < ChildCount;

			public int Resolve(bool isPart1) => ChildCount == 0
				? MetaData.Sum( )
				: isPart1
				? MetaData.Sum( ) + Children.Sum(c => c.Resolve(isPart1))
				: MetaData.Sum(n => n <= ChildCount ? Children[n - 1].Resolve(isPart1) : 0);
		}
	}
}