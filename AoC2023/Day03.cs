using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;
using Microsoft.VisualBasic;

namespace AoC2023
{
	public class Day03 : Solution
	{
		private readonly Grid2d grid;
		public Day03(string file) : base(file) => grid = new Grid2d(Input);


		public override string SolvePart1()
		{
			var parts = new List<int>( );
			var sb = new StringBuilder( );
			var neighbors = new List<Grid2d.Cell>( );

			foreach (var cell in grid)
			{
				if (char.IsDigit(cell.Character))
				{
					sb.Append(cell.Character);
					neighbors.AddRange(grid.GetNeighbors(cell).Where(c => !char.IsDigit(c.Character)));
					continue;
				}

				if (neighbors.Count != 0)
				{
					if (neighbors.Any(n => n.Character != '.'))
					{
						parts.Add(int.Parse(sb.ToString( )));
					}

					neighbors.Clear( );
					sb.Clear( );
				}
			}

			return parts.Sum( ).ToString( );
		}

		public override string SolvePart2()
		{
			var ratios = new List<long>( );
			foreach (var cell in grid)
			{
				if (cell.Character == '*')
				{
					var neighbors = grid.GetNeighbors(cell).Where(n => char.IsDigit(n.Character)).ToList();

					if(neighbors.Count < 2) continue;

					var rangeString = new StringBuilder();
					var range = grid.GetRange(cell.Position.Add(-3, -1), cell.Position.Add(3, 1)).GroupBy(c => c.Position.y);
					foreach (var group in range)
					{
						foreach (var cell1 in group)
						{
							rangeString.Append(cell1.Character);
						}
						rangeString.AppendLine();
					}
					Console.WriteLine(rangeString);


					var digits = neighbors
						.Select(d =>
							 grid.TryGetCell(d.Position.Add(-1, 0), out var m1) && m1.Character is '.' or '*' &&
							 grid.TryGetCell(d.Position.Add(1, 0), out var m2) && m2.Character is '.' or '*'
							? new List<Grid2d.Cell> { d }
							: grid.TryGetCell(d.Position.Add(-1, 0), out var cl1) && cl1.Character is '.' or '*'
							? grid.GetRange(d.Position, d.Position.Add(2, 0))
							: grid.TryGetCell(d.Position.Add(1, 0), out var cr1) && cr1.Character is '.' or '*'
							? grid.GetRange(d.Position.Add(-2, 0), d.Position)
							: grid.GetRange(d.Position.Add(-1, 0), d.Position.Add(1, 0)))

						.Select(d => int.Parse(new string(d.Where(c => char.IsDigit(c.Character)).Select(c => c.Character).ToArray())))
						.Distinct().ToList();
					
					//bug! if 2 of the same digit appear left/right & up/down from the gear we do NOT find them because of the distinct!
					// after inspecting ALL the gear ranges it turns out to be the case for 273
					//if(digits.Count < 2) continue;

					if (digits.Count == 1 && neighbors.Select(n => cell.Position.y).Distinct( ).Count( ) == 2)
					{
						digits.Add(digits.First( ));
					}
				

					Console.WriteLine($"Found: {digits[0]} and {digits[1]}");
					Console.WriteLine();
					ratios.Add(digits.Skip(1).Aggregate(digits[0], (sum, d) => sum * d));
				}
			}

			return (ratios.Sum( )).ToString( );
		}
	}
}