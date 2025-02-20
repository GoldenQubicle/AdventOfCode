using System.Collections.Concurrent;
using System.Drawing;

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
		return springs.Select(s => GetArrangements(s.row, s.records, 0, new ())).Sum( ).ToString( );
		return string.Empty;
	}


	private static string Replace(string row, int idx, char c)
	{
		var t = row.Remove(idx, 1);
		return t.Insert(idx, c.ToString( ));
	}

	public static int TryFit(string springs, List<int> record, Dictionary<string, int> cache)
	{
		if (record.IsEmpty() && (string.IsNullOrEmpty(springs) || springs.All(c => c is '.')))
			return 1;

		if (cache.TryGetValue(springs, out var count))
			return count;

		//work from the back
		//do stuff

		return -1;
	}

	public static int GetArrangements(string row, List<int> record, int count, Dictionary<string, int> cache)
	{
		if ((string.IsNullOrEmpty(row) || row.Length < 2) && record.Count > 0) return 0;

		//no more groups left to fit...
		if (record.Count == 0)
		{
			//...but did we actually meet the criteria for a valid arrangement?
			if (row.Contains('#'))
				return count;

			return 1;
		}

		if (cache.TryGetValue(row, out var value))
			return value;

		//do not care, skip
		if (row.StartsWith('.'))
			return GetArrangements(row[1..], record, count, cache);

		//replace and recurse
		if (row.StartsWith('?'))
		{
			return (GetArrangements(row[1..], record, count, cache) + // might as well skip here immediately instead of replacing  
					GetArrangements('#' + row[1..], record, count, cache));
		}

		//row must start with # when we get here
		var n = record.First( );
		var possible = row.TakeWhile(c => c != '.'); 
		var atEnd = row.Length == n;
		
		if (possible.Count( ) >= n && (atEnd || row[n] != '#'))
		{
			var next = atEnd ? string.Empty : row[(n + 1)..];
			var result = GetArrangements(next, record.Skip(1).ToList( ), count, cache);
			cache.Add(row, result);
		}
		else
		{
			cache.Add(row, 0);
		}


		

		return cache[row];
	}

	//private int FitPossibleGroups(string group, int length)
	//{
	//	var result = new List<string>( );
	//	var queue = new Queue<string>( );
	//	queue.Enqueue(row);

	//	while (queue.TryDequeue(out var current))
	//	{
	//		var idx = current.IndexOf('?');
	//		if (idx == -1)
	//		{
	//			result.Add(current);
	//			continue;
	//		}

	//		queue.Enqueue(Replace(current, idx, '.'));
	//		queue.Enqueue(Replace(current, idx, '#'));
	//	}

	//	var count = result
	//		.Select(r => Regex.Matches(r, "(#+)"))
	//		.Count(m => m.Count == record.Count && record.WithIndex( ).All(r => m[r.idx].Length == r.Value));
	//	return 0;
	//}

	public override async Task<string> SolvePart2()
	{
		var result = new ConcurrentBag <int>();
		Parallel.ForEach(springs, s =>
		{
			var row = string.Join('?', Enumerable.Repeat(s.row, 5));
			var record = Enumerable.Repeat(s.records, 5).SelectMany(n => n).ToList( );
			result.Add(GetArrangements(row, record, 0, new ()));
		});

		//return springs.Select(s =>
		//{
		//	cache = new( );
		//	var row = string.Join('?', Enumerable.Repeat(s.row, 5));
		//	var record = Enumerable.Repeat(s.records, 5).SelectMany(n => n).ToList();
		//	return GetArrangements(row, record, 0);

		//}).Sum().ToString();

		return result.Sum().ToString();
	}
}
