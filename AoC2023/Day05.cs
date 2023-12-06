using System.Collections;
using Common;
using Common.Extensions;

namespace AoC2023
{
	public class Day05 : Solution
	{
		private readonly List<long> seeds;

		public class Mappings : IEnumerable<string>
		{
			public const string Seed2Soil = "seed-to-soil map:";
			public const string Soil2Fertilizer = "soil-to-fertilizer map:";
			public const string Fertilizer2Water = "fertilizer-to-water map:";
			public const string Water2Light = "water-to-light map:";
			public const string Light2Temperature = "light-to-temperature map:";
			public const string Temperature2Humidity = "temperature-to-humidity map:";
			public const string Humidity2Location = "humidity-to-location map:";

			private Dictionary<string, List<(long dest, long start, long end)>> maps = new( )
			 {
			  { Seed2Soil, new List<(long dest, long start, long end)>() },
			  { Soil2Fertilizer, new List<(long dest, long start, long end)>() },
			  { Fertilizer2Water, new List<(long dest, long start, long end)>() },
			  { Water2Light, new List<(long dest, long start, long end)>() },
			  { Light2Temperature , new List<(long dest, long start, long end)>() },
			  { Temperature2Humidity, new List<(long dest, long start, long end)>() },
			  { Humidity2Location, new List<(long dest, long start, long end)>() },
			 };

			public IEnumerator<string> GetEnumerator()
			{
				yield return Seed2Soil;
				yield return Soil2Fertilizer;
				yield return Fertilizer2Water;
				yield return Water2Light;
				yield return Light2Temperature;
				yield return Temperature2Humidity;
				yield return Humidity2Location;
			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator( );

			public List<(long dest, long start, long end)> this[string m]
			{
				get => maps[m];
				set => maps[m] = value;
			}

			public long GetDestination(string map, long value)
			{
				var inRange = maps[map].Where(v => value >= v.start && value <= v.end).ToList( );
				if (inRange.Count == 0)
					return value;

				return inRange[0].dest + (value - inRange[0].start);
			}

			public List<(long start, long end)> GetDestinationRanges(string map, (long s, long e) range)
			{
				var result = new List<(long start, long end)>( );
				var toCheck = new Queue<(long start, long end)>( );
				toCheck.Enqueue(range);

				while (toCheck.Count != 0)
				{
					var current = toCheck.Dequeue( );
					var inRange = maps[map].Where(m => !(m.start > current.end || current.start > m.end)).ToList( );

					if (inRange.Count == 0)
					{
						result.Add(range);
						continue;
					}
						
					foreach (var ranges in inRange.Select(m => GetRange((m.start, m.end), current, m.dest - m.start)))
					{
						ranges.Where(r => !r.isMapped).ForEach(r => toCheck.Enqueue((r.s, r.e)));

						if (ranges.Any(r => r.isMapped))
							result.Add(ranges.Where(r => r.isMapped).Select(r => (r.s, r.e)).First( ));
					}
				}
				return result;
			}

			public static List<(long s, long e, bool isMapped)> GetRange((long s, long e) source, (long s, long e) range, long offset) => (source, range) switch
			{
				var (s, r) when s.s > r.e || r.s > s.e => new( ) { (range.s, range.e, false) },
				var (s, r) when r.s >= s.s && r.s <= s.e && r.e > s.e => new( ) { (range.s + offset, source.e + offset, true), (source.e + 1, range.e, false) },
				var (s, r) when r.s < s.s && r.e >= s.s && r.e <= s.e => new( ) { (range.s, source.s - 1, false), (source.s + offset, range.e + offset, true) },
				var (s, r) when r.s >= s.s && r.e <= s.e => new( ) { (range.s + offset, range.e + offset, true) },
				var (s, r) when r.s < s.s && r.e > s.e => new( ) { (range.s, source.s - 1, false), (source.s + offset, source.e + offset, true), (source.e + 1, range.e, false) }
			};
		}

		public Mappings Maps = new( );

		public Day05(string file) : base(file, doRemoveEmptyLines: false)
		{
			seeds = Input[0].Split(':')[1]
				.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
				.Select(long.Parse).ToList( );

			foreach (var map in Maps)
			{
				Maps[map] = Input.Skip(Input.IndexOf(map) + 1).TakeWhile(l => l.Length != 0).Select(l =>
				{
					var parts = l.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
					return (long.Parse(parts[0]), long.Parse(parts[1]), (long.Parse(parts[1]) + long.Parse(parts[2])));
				}).ToList( );
			}
		}

		public override string SolvePart1()
		{
			var toBeMapped = seeds;

			foreach (var map in Maps)
			{
				toBeMapped = toBeMapped.Select(v => Maps.GetDestination(map, v)).ToList( );
			}

			return toBeMapped.Min( ).ToString( );
		}

		public override string SolvePart2()
		{
			var toBeMapped = seeds.Chunk(2).Select(c => (start: c[0], end: (c[0] + c[1]))).ToList( );

			foreach (var map in Maps)
			{
				toBeMapped = toBeMapped.SelectMany(v => Maps.GetDestinationRanges(map, v)).Distinct( ).ToList( );
			}

			return toBeMapped.Min( ).start.ToString( );
		}
	}
}