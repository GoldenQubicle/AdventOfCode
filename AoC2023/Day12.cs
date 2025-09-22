using System.Collections.Concurrent;
using System.Drawing;

namespace AoC2023;

public class Day12 : Solution
{
	private List<(string row, List<int> records)> springs;
	private static readonly Dictionary<(string, string), long> _cache = new( );
	public Day12(string file) : base(file)
	{
		springs = Input.Select(l =>
		{
			var parts = l.Split( );
			return (row: parts[0], records: parts[1].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList( ));
		}).ToList( );
	}

	public override async Task<string> SolvePart1() => 
		springs.Select(s => RecurseArrangement(s.row, s.records, 0, 0)).Sum( ).ToString( );


	public override async Task<string> SolvePart2()
	{
		//still taking forever, should obviously take modulo of indices and just wrap around the rows 5 times
		

		return springs.Select(s => RecurseArrangement(s.row, s.records, 0, 0)).Sum( ).ToString( );
	}
	
	public static long RecurseArrangement(string row, List<int> groups, int rowIdx, int groupIdx, bool isPart2 = false)
	{
		//make rowIdx & groupIdx local variables based on modulo 
		var rIdx = rowIdx % row.Length;
		var gIdx = groupIdx % groups.Count;
		var rowLength = isPart2 ? (row.Length * 5) - 1 : row.Length;
		var groupsCount= isPart2 ? groups.Count * 5 : groups.Count;

		if (rowIdx >= rowLength) //no more springs left to check
			return groupIdx == groups.Count ? 1 : 0; //check if used up all groups

		var key = (row[rowIdx..], string.Join('-', groups.Skip(groupIdx)));

		if (_cache.TryGetValue(key, out var result))
			return result;

		//no more groups left to fit
		if (groupIdx >= groupsCount)
			return row[rowIdx..].Contains('#') ? 0 : 1; // check for any remaining damaged spring

		var arrangements = 0L;

		if (row[rowIdx] is '.' or '?')
			arrangements += RecurseArrangement(row, groups, rowIdx + 1, groupIdx);

		//if (row[rowIdx] is '#' or '?')

		var groupSize = groups[gIdx];
		var end = rowIdx + groupSize;

		//group too big for remaining row OR operational spring in the way OR doesn't have at least one operation spring after group
		if (rowIdx + groupSize > rowLength || row[rowIdx..end].Contains('.') || end < rowLength && row[end] == '#')
			arrangements += 0;
		else 
			arrangements += RecurseArrangement(row, groups, end + 1, groupIdx + 1);

		_cache[key] = arrangements;

		return arrangements;
	}

	private static long TryPlaceGroup(string row, List<int> records, int rowIdx, int recordIdx)
	{
		var size = records[recordIdx];

		//group too big for remaining row
		if (rowIdx + size > row.Length)
			return 0;

		var end = rowIdx + size;
		var check = row[rowIdx..end];

		if (row[rowIdx..end].Contains('.')) //operation spring in the way
			return 0;

		if (end < row.Length && row[end] == '#')
			return 0;//must have at least one operation spring after group

		return RecurseArrangement(row, records, end + 1, recordIdx + 1);

	}





}
