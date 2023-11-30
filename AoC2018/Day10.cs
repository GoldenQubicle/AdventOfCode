using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2018
{
	public record Point((int x, int y) Position, (int x, int y) Velocity)
	{
		public (int x, int y) Position { get; private set; } = Position;

		public void Update() => Position = Position.Add(Velocity);

	}

	public class Day10 : Solution
	{
		private readonly IEnumerable<Point> points;

		public Day10(string file) : base(file) => points = Input
			.Select(l => Regex.Match(l, @"position=<(?<p>.\d+, .\d+)> velocity=<(?<v>.\d+, .\d+)"))
			.Select(m => new Point(m.AsIntTuple("p", ","), m.AsIntTuple("v", ","))).ToList( );


		public override string SolvePart1()
		{
			for (var i = 0 ;i <= 10942 ;i++)
			{
				var minx = points.MinBy(p => p.Position.x).Position.x;
				var maxx = points.MaxBy(p => p.Position.x).Position.x;
				var miny = points.MinBy(p => p.Position.y).Position.y;
				var maxy = points.MaxBy(p => p.Position.y).Position.y;
				var current = points.Select(p => p.Position);

				if (i == 10942)
				{
					var sb = new StringBuilder( );

					for (var y = miny ;y <= maxy ;y++)
					{
						sb.AppendLine( );
						for (var x = minx ;x <= maxx ;x++)
						{
							sb.Append(current.Contains((x, y)) ? '#' : '.');
						}
					}

					Console.Write(sb.ToString( ));
					Console.WriteLine( );
					Console.WriteLine($"It took {i} seconds");
					Console.WriteLine( );
				}
				
				points.ForEach(p => p.Update( ));
				//var k = Console.ReadKey( );
			}



			return string.Empty;
		}

		public override string SolvePart2()
		{
			return string.Empty;
		}
	}
}