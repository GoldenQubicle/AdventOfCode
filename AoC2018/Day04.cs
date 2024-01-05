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
	public class Day04 : Solution
	{
		private record Record(DateOnly Date, TimeOnly Time, string State)
		{
			public bool IsBegin() => State.Contains("begins");
			public int GetId() => State.AsInteger( );
		}

		private Dictionary<int, List<(int s, int e)>> guards = new( );

		public Day04(string file) : base(file)
		{
			var records = Input
				.Select(s => Regex.Match(s, @"(?<date>\d{4}-\d{2}-\d{2}).(?<time>\d{2}:\d{2})].(?<log>.*)"))
				.Select(m => (date: DateOnly.Parse(m.Groups["date"].Value),
					time: TimeOnly.Parse(m.Groups["time"].Value), log: m.Groups["log"].Value))
				.OrderBy(t => t.date)
				.ThenBy(t => t.time)
				.Aggregate(new Queue<Record>( ), (queue, t) =>
				{
					queue.Enqueue(new Record(t.date, t.time, t.log));
					return queue;
				});

			var currentGuard = 0;

			while (records.Count != 0)
			{
				if (records.Peek( ).IsBegin( ))
				{
					currentGuard = records.Dequeue( ).GetId( );
					
					if (!guards.ContainsKey(currentGuard))
						guards.Add(currentGuard, new List<(int, int)>( ));

					if(!records.Peek().State.Contains("asleep"))
						continue;
						
					guards[currentGuard].Add((records.Dequeue( ).Time.Minute, records.Dequeue( ).Time.Minute));

					continue;
				}

				guards[currentGuard].Add((records.Dequeue( ).Time.Minute, records.Dequeue( ).Time.Minute));
			}
		}


		public override async Task<string> SolvePart1()
		{
			var mostAsleep = guards.MaxBy(g => g.Value.Sum(r => r.e - r.s));
			var minute = mostAsleep.Value
				.Select(r => Enumerable.Range(r.s, r.e - r.s))
				.Aggregate(new Dictionary<int, int>(), (acc, range) =>
				{
					range.ForEach(n =>
					{
						if (!acc.ContainsKey(n)) acc.Add(n, 1);
						else acc[n]++;

					});
					return acc;
				}).MaxBy(kvp => kvp.Value).Key;
				

			return (mostAsleep.Key * minute).ToString();
		}

		public override async Task<string> SolvePart2()
		{
			var guard = guards.Where(kvp => kvp.Value.Count > 0)
					.Select(g => (g.Key, g.Value
					.Select(r => Enumerable.Range(r.s, r.e - r.s))
					.Aggregate(new Dictionary<int, int>(), (acc, range) =>
					{
						range.ForEach(n =>
						{
							if (!acc.ContainsKey(n))
								acc.Add(n, 1);
							else
								acc[n]++;

						});
						return acc;
					}).MaxBy(kvp => kvp.Value)))
				.MaxBy(g => g.Item2.Value);

			return (guard.Key * guard.Item2.Key).ToString();
		} 
	}
}