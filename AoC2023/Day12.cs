namespace AoC2023;

public class Day12 : Solution
{
	private readonly List<(string row, List<int> records)> _springs;
	//private static readonly Dictionary<(string, string), long> _cache = new( );
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


	public static long RecurseArrangement(string row, List<int> groups, int rowIdx, int groupIdx, Dictionary<(string, string), long> cache, bool isPart2 = false)
	{
		//make rowIdx & groupIdx local variables based on modulo 
		//a couple of things; for part2 we want to fit groups on row + '?' + row 
		//so the modulo needs to be based with the additional ? in mind

		var rIdx = rowIdx % (row.Length + 1); // take the additional ? into account for part 2
		var gIdx = groupIdx % groups.Count;

		var unfoldedLength = isPart2 ? (row.Length * 5) + 4 : row.Length;
		var unfoldedGroupsCount = isPart2 ? groups.Count * 5 : groups.Count;

		if (rowIdx >= unfoldedLength) //no more springs left to check
			return groupIdx == unfoldedGroupsCount ? 1 : 0; //check if used up all groups

		var toCheck = rowIdx <= (row.Length * 4) + 3 && isPart2
			? row + '?' + row
			: row;

		var key = (toCheck[rIdx..], string.Join('-', groups.Skip(gIdx)));

		if (cache.TryGetValue(key, out var result))
			return result;

		//no more groups left to fit
		if (groupIdx >= unfoldedGroupsCount)
			return toCheck[rIdx..].Contains('#') ? 0 : 1; // check for any remaining damaged spring

		var arrangements = 0L;

		if (toCheck[rIdx] is '.' or '?')
			arrangements += RecurseArrangement(row, groups, rowIdx + 1, groupIdx, cache, isPart2);

		var groupSize = groups[gIdx];
		var unfoldedEnd = rowIdx + groupSize; 
		var rowEnd = rIdx + groupSize;

		//group too big for remaining row OR operational spring in the way OR doesn't have at least one operation spring after group
		if (unfoldedEnd > unfoldedLength || toCheck[rIdx..rowEnd].Contains('.') || unfoldedEnd < unfoldedLength && toCheck[rowEnd] == '#')
			arrangements += 0;
		else
			arrangements += RecurseArrangement(row, groups, unfoldedEnd + 1, groupIdx + 1, cache, isPart2);

		cache[key] = arrangements;

		return arrangements;
	}

	//private static long TryPlaceGroup(string row, List<int> records, int rowIdx, int recordIdx)
	//{
	//	var size = records[recordIdx];

	//	//group too big for remaining row
	//	if (rowIdx + size > row.Length)
	//		return 0;

	//	var end = rowIdx + size;
	//	var check = row[rowIdx..end];

	//	if (row[rowIdx..end].Contains('.')) //operation spring in the way
	//		return 0;

	//	if (end < row.Length && row[end] == '#')
	//		return 0;//must have at least one operation spring after group

	//	return RecurseArrangement(row, records, end + 1, recordIdx + 1);

	//}
}
