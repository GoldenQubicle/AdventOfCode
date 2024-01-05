using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2023;

public class Day12 : Solution
{
	private readonly List<(string row, List<int> records)> springs;

	public Day12(string file) : base(file)
	{
		springs = Input.Select(l =>
		{
			var parts = l.Split( );
			return (row: parts[0], records: parts[1].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList( ));
		}).ToList( );
	}

	public override async Task<string> SolvePart1()
	{
		var t = springs.Select(s => GetArrangements(s.row, s.records));

		return t.Sum( ).ToString( );
	}

	public static int GetArrangements(string row, List<int> records)
	{
		var toCheck = new Queue<(string row, int count, List<int> groups)>( );

		toCheck.EnqueueAll(GetNext(row, 0, records));

		var count = 0;
		while (toCheck.Count > 0)
		{
			var distinct = toCheck.Distinct( ).ToList( );
			toCheck.Clear( );
			toCheck.EnqueueAll(distinct);
			var current = toCheck.Dequeue( );

			
			if (current.row.Length < current.groups.Sum( ) + (current.groups.Count > 0 ? current.groups.Count - 1 : 0) - current.count)
				continue;

			if (current.row != string.Empty)
			{
				Console.WriteLine($"{current.row} with count {current.count} and groups {current.groups.Count}");

				if (current.row.First( ) == '#')
				{
					if (current.groups.Count > 0 && current.count + 1 <= current.groups[0])
						toCheck.EnqueueAll(GetNext(current.row[1..], current.count + 1, current.groups));
				}

				if (current.row.First( ) == '.')
				{
					var closeGroup = current.groups.Count > 0 && current.count == current.groups.First( );
					toCheck.EnqueueAll(closeGroup
						? GetNext(current.row[1..], 0, current.groups[1..])
						: GetNext(current.row[1..], 0, current.groups));
				}
			}
			else
			{
				Console.WriteLine($"{current.row} with count {current.count} and groups {current.groups.Count} and group {(current.groups.Count > 0 ? current.groups.First( ) : 0)}");
				var isValid = current.groups.Count != 0 && current.count == current.groups.First( ) || current.groups.Count == 0 && current.count == 0;

				Console.WriteLine(isValid);
				count += isValid ? 1 : 0;
			}
		}

		return count;

	}

	private static List<(string r, int gc, List<int> g)> GetNext(string row, int count, List<int> records) =>
		row.StartsWith('?')
			? new( ) { (row.ReplaceAt(0, '.'), count, records), (row.ReplaceAt(0, '#'), count, records) }
			: new( ) { (row, count, records) };


	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}
}
