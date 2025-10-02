namespace AoC2023;

public class Day12 : Solution
{
	private readonly List<(string row, List<int> records)> _springs;
	
	public Day12(string file) : base(file)
	{
		_springs = Input.Select(l =>
		{
			var parts = l.Split( );
			return (row: parts[0], records: parts[1].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList( ));
		}).ToList( );
	}

	public override async Task<string> SolvePart1() =>
		_springs.Select(s => RecurseArrangement(s.row, s.records, 0, 0, [ ])).Sum( ).ToString( );


	public override async Task<string> SolvePart2() =>
		_springs.Select(s => RecurseArrangement(s.row, s.records, 0, 0, [ ], isPart2: true)).Sum( ).ToString( );


	public static long RecurseArrangement(string row, List<int> groups, int rowIdx, int groupIdx, Dictionary<(int, int), long> cache, bool isPart2 = false)
	{
		var rIdx = rowIdx % (row.Length + 1); // take the additional ? into account for part 2
		var gIdx = groupIdx % groups.Count;

		var unfoldedLength = isPart2 ? (row.Length * 5) + 4 : row.Length;
		var unfoldedGroupsCount = isPart2 ? groups.Count * 5 : groups.Count;

		if (rowIdx >= unfoldedLength) //no more springs left to check
			return groupIdx == unfoldedGroupsCount ? 1 : 0; //check if used up all groups

		var key = (rowIdx, groupIdx);

		if (cache.TryGetValue(key, out var result))
			return result;

		var toCheck = rowIdx <= (row.Length * 4) + 3 && isPart2
			? row + '?' + row
			: row;

		//no more groups left to fit. Check for any remaining damaged spring. If present, overfitted groups and thus invalid arrangement. 
		if (groupIdx >= unfoldedGroupsCount)
			return toCheck[rIdx..].Contains('#') ? 0 : 1; 

		var arrangements = 0L;

		if (toCheck[rIdx] is '.' or '?')
			arrangements += RecurseArrangement(row, groups, rowIdx + 1, groupIdx, cache, isPart2);

		var groupSize = groups[gIdx];
		var unfoldedEnd = rowIdx + groupSize; 
		var rowEnd = rIdx + groupSize;

		//group too big for remaining row OR operational spring in the way OR doesn't have at least one operational spring after group
		if (unfoldedEnd > unfoldedLength || toCheck[rIdx..rowEnd].Contains('.') || unfoldedEnd < unfoldedLength && toCheck[rowEnd] == '#')
			arrangements += 0;
		else
			arrangements += RecurseArrangement(row, groups, unfoldedEnd + 1, groupIdx + 1, cache, isPart2);

		cache[key] = arrangements;

		return arrangements;
	}
}
