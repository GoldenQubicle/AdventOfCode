using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2018
{
	public class Day06 : Solution
	{
		private readonly Dictionary<int, (int x, int y)> locations;

		public Day06(string file) : base(file) => locations = Input.Select((s, idx) =>
		{
			var parts = s.Split(',');
			return (idx, parts[0].AsInteger( ), parts[1].AsInteger( ));
		}).ToDictionary(l => 65 + l.idx, l => (l.Item2, l.Item3));


		public override string SolvePart1()
		{
			var minx = locations.Values.MinBy(c => c.x).x;
			var maxx = locations.Values.MaxBy(c => c.x).x;
			var miny = locations.Values.MinBy(c => c.y).y;
			var maxy = locations.Values.MaxBy(c => c.y).y;


			var grid = new Grid2d(maxx, maxy);
			
			foreach (var cell in grid)
			{
				var closest = locations.Select(l => (l.Key, d: GetManhattanDistance(l.Value, cell.Position))).GroupBy(l => l.d).MinBy(g => g.Key);
				cell.Character = closest.Count( ) == 1 ? (char)closest.First( ).Key : '.';
			}

			var exclude = grid
				.Where(c => c.Position.x == minx || c.Position.x == maxx || c.Position.y == miny || c.Position.y == maxy)
				.Select(c => c.Character).Distinct( ).ToList( );

			return grid
				.Where(c => !exclude.Contains(c.Character))
				.GroupBy(c => c.Character)
				.MaxBy(g => g.Count( ))!
				.Count( ).ToString( );
		}

		private int GetManhattanDistance((int x, int y) one, (int x, int y) two) =>
			Math.Abs(two.x - one.x) + Math.Abs(two.y - one.y);

		public override string SolvePart2() => null;
	}
}