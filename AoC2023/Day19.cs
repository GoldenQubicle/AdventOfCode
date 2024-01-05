using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2023;

public class Day19 : Solution
{
	private readonly Dictionary<string, WorkFlow> workflows;
	private readonly List<Part> parts;

	public Day19(string file) : base(file, doRemoveEmptyLines: false)
	{
		workflows = Input
			.TakeWhile(l => l != string.Empty)
			.Select(l => Regex.Matches(l, "(?<tag>[a-z]*).(?<={)(?<rules>.*)(?=})"))
			.Select(m =>
			{
				var group = m.GetGroup("rules").Split(',', StringSplitOptions.TrimEntries).ToList( );
				var rules = group.SkipLast(1).Select(l => (c: l[0], op: l[1], v: l.AsInteger( ), r: l.Split(':').Last( ))).ToList( );

				rules.Add(('_', '_', 0, group.Last( )));
				return new WorkFlow(m.GetGroup("tag"), rules);
			}).ToDictionary(wf => wf.Tag, wf => wf);

		parts = Input
			.SkipWhile(l => !l.StartsWith('{'))
			.Select(l => Regex.Match(l, "(?<={)(.*)(?=})").Value)
			.Select(l =>
			{
				var parts = l.Split(',', StringSplitOptions.TrimEntries);
				return new Part(new Dictionary<char, (int ,int)>
				{
					{ 'x', (1, parts[0].AsInteger()) },
					{ 'm', (1, parts[1].AsInteger()) },
					{ 'a', (1, parts[2].AsInteger()) },
					{ 's', (1, parts[3].AsInteger()) },
				});
			}).ToList( );
	}


	public override async Task<string> SolvePart1()
	{
		var accepted = new List<Part>( );

		foreach (var part in parts)
		{
			while (part.Next is not ("A" or "R"))
			{
				workflows[part.Next].ProcessPart1(part);
			}

			if (part.Next == "A")
				accepted.Add(part);
		}

		return accepted.Sum(p => p.Categories.Values.Sum(r => r.e )).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		//starts with xmas ranges 1-4k at in:
		//per workflow if rule applies, adjust range accordingly
		//crucially, per applied rule I want to queue a new state with adjusted ranges
		//intersect xmas ranges for all states which end up in A
		var queue = new Queue<Part>( );

		while (queue.TryDequeue(out var current))
		{

		}

		return string.Empty;
	}


	public record WorkFlow(string Tag, List<(char c, char op, int value, string result)> Rules)
	{
		//public void ProcessPart2(State state)
		//{
		//	foreach (var (c, op, value, result) in Rules.SkipLast(1))
		//	{
		//		//if rules applies, generate 2 new 
		//		return;
		//	}
		//}

	
		public void ProcessPart1(Part p)
		{
			foreach (var (c, op, value, result) in Rules.SkipLast(1))
			{
				if (!Accepted(p.Categories[c], op, value))
					continue;

				p.Next = result;
				return;
			}

			p.Next = Rules.Last( ).result;
		}

		private static bool Accepted((int s, int e) r, char op, int v) => op == '>' ? r.e> v : r.e < v;
	}

	public record Part(Dictionary<char, (int s, int e)> Categories)
	{
		public string Next { get; set; } = "in";
	};
}
