using System.Text;
using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2018
{
	public class Day07 : Solution
	{
		public static int Offset { get; set; } = 4;
		public static int NumberOfWorkers { get; set; } = 5;

		private readonly Dictionary<string, Node> nodes;
		
		public Day07(string file) : base(file)
		{
			nodes = Input
				.Select(l => Regex.Match(l, @"Step (?<step1>[A-Z]) must be finished before step (?<step2>[A-Z]) can begin."))
				.Aggregate(new Dictionary<string, Node>( ), (nodes, match) =>
				{
					var one = match.Groups["step1"].Value;
					var two = match.Groups["step2"].Value;

					if (!nodes.ContainsKey(one))
						nodes.Add(one, new Node(one));
					if (!nodes.ContainsKey(two))
						nodes.Add(two, new Node(two));

					nodes[one].After.Add(nodes[two]);
					nodes[two].Before.Add(nodes[one]);

					return nodes;
				});
		}

		public override async Task<string> SolvePart1()
		{
			var order = new HashSet<string>( );
			var queue = new PriorityQueue<Node, string>( );

			nodes.Values
				.Where(n => n.IsReady( ))
				.OrderBy(n => n.Id)
				.ForEach(n => queue.Enqueue(n, n.Id));

			while (queue.Count != 0)
			{
				var node = queue.Dequeue( );

				if (!node.IsReady( ))
					continue;

				order.Add(node.Id);
				node.After.ForEach(n =>
				{
					n.Before.Remove(node);
					queue.Enqueue(n, n.Id);
				});
			}

			return order.Aggregate(new StringBuilder( ), (builder, s) => builder.Append(s)).ToString( );
		}

		public override async Task<string> SolvePart2()
		{
			var timer = 0;
			var queue = new PriorityQueue<Node, string>( );
			var workers = new Worker[NumberOfWorkers];
			Enumerable.Range(0, NumberOfWorkers).ForEach(n => workers[n] = new Worker( ));

			nodes.Values
				.Where(n => n.IsReady( ))
				.Select((n, idx) => (n, idx))
				.ForEach(t => workers[t.idx].Begin(t.n));

			while (workers.Any(w => w.IsBusy( )))
			{
				timer++;
				workers.ForEach(w => w.Tick( ));

				//process the nodes which are completed this tick
				workers
					.Where(w => w.IsCompleted( ))
					.Select(w => w.Completed)
					.ForEach(node => node.After.ForEach(n => n.Before.Remove(node)));

				// queue up all the nodes which are ready to be worked on
				nodes.Values
					.Where(n => n.ReadyForWorker( ))
					.ForEach(n => queue.Enqueue(n, n.Id));

				// distribute nodes over available workers
				workers
					.Where(w => !w.IsBusy( ))
					.ForEach(w =>
					{
						if (queue.Count > 0)
							w.Begin(queue.Dequeue( ));
					});
			}

			return timer.ToString( );
		}

		private record Worker
		{
			public Node Completed => current;

			private Node current = new("~");

			public void Begin(Node n)
			{
				current = n;
				current.Start( );
			}

			public void Tick() => current.Tick( );
			public bool IsCompleted() => current.IsCompleted( ) && current.Id != "~";
			public bool IsBusy() => !IsCompleted( ) && current.Id != "~";
		}


		private record Node
		{
			private int timer;
			private bool hasStarted;
			public string Id { get; }
			public List<Node> Before { get; } = new( );
			public List<Node> After { get; } = new( );

			public Node(string id)
			{
				timer = id.First( ) - Offset;
				Id = id;
			}

			public bool IsReady() => Before.Count == 0;
			public void Tick() => timer--;
			public bool IsCompleted() => timer <= 0;
			public bool ReadyForWorker() => IsReady( ) && !IsCompleted( ) && !hasStarted;
			public void Start() => hasStarted = true;
		}
	}
}