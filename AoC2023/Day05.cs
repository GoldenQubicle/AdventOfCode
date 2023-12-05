using System.Collections;
using System.Diagnostics;
using Common;
using Common.Extensions;

namespace AoC2023
{
    public class Day05 : Solution
    {
	    private List<long> seeds;
	
	    public class Mappings : IEnumerable<string>
	    {
		    public const string Seed2Soil = "seed-to-soil map:";
		    public const string Soil2Fertilizer = "soil-to-fertilizer map:";
		    public const string Fertilizer2Water = "fertilizer-to-water map:";
		    public const string Water2Light = "water-to-light map:";
		    public const string Light2Temperature = "light-to-temperature map:";
		    public const string Temperature2Humidity = "temperature-to-humidity map:";
		    public const string Humidity2Location = "humidity-to-location map:";

			private Dictionary<string, List<(long dest, long source, long count)>> maps = new( )
			 {
			  { Seed2Soil, new List<(long dest, long source, long count)>() },
			  { Soil2Fertilizer, new List<(long dest, long source, long count)>() },
			  { Fertilizer2Water, new List<(long dest, long source, long count)>() },
			  { Water2Light, new List<(long dest, long source, long count)>() },
			  { Light2Temperature , new List<(long dest, long source, long count)>() },
			  { Temperature2Humidity, new List<(long dest, long source, long count)>() },
			  { Humidity2Location, new List<(long dest, long source, long count)>() },
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

			public List<(long dest, long source, long count)> this[string m]
			{
				get => maps[m];
				set => maps[m] = value;
			}

			public long GetDestination(string map, long value)
			{
				var inRange = maps[map].Where(v => value >= v.source && value < v.source + v.count).ToList();
				if (inRange.Count() == 0) return value;

				Debug.Assert(inRange.Count( ) == 1);

				return inRange[0].dest + (value - inRange[0].source);
			} 
		}

	    public Mappings Maps = new();

	    public Day05(string file) : base(file, doRemoveEmptyLines: false)
	    {
		    seeds = Input[0].Split(':')[1]
			    .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
			    .Select(long.Parse).ToList();

		    foreach (var map in Maps)
		    {
			    Maps[map] = Input.Skip(Input.IndexOf(map) + 1).TakeWhile(l => l.Length != 0).Select(l =>
				{
					var parts = l.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
					return (long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
				}).ToList( );
			}
	    }
        

        public override string SolvePart1( )
        {
	        var toBeMapped = seeds;
	        foreach (var map in Maps)
	        {
		        toBeMapped = toBeMapped.Select(v => Maps.GetDestination(map, v)).ToList();
	        }

	        return toBeMapped.Min().ToString();
        }

        public override string SolvePart2( )
        {
	        return string.Empty;
        }
    }
}