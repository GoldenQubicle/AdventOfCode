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
				var group = m.GetGroup("rules").Split(',', StringSplitOptions.TrimEntries).ToList();
				var rules= group.SkipLast(1).Select(l => (c: l[0], op: l[1], v: l.AsInteger(), r: l.Split(':').Last())).ToList();

				rules.Add(('n', '_', 0,  group.Last() ));
				return new WorkFlow(m.GetGroup("tag"), rules);
			}).ToDictionary(wf => wf.Tag, wf => wf);

		parts = Input
			.SkipWhile(l => !l.StartsWith('{'))
			.Select(l => Regex.Match(l, "(?<={)(.*)(?=})").Value)
			.Select(l =>
			{
				var parts = l.Split(',', StringSplitOptions.TrimEntries);
				return new Part(new Dictionary<char, int>
				{
					{ 'x', parts[0].AsInteger() },
					{ 'm', parts[1].AsInteger() },
					{ 'a', parts[2].AsInteger() },
					{ 's', parts[3].AsInteger() },
				});
			}).ToList();
	}
	

	public override string SolvePart1()
	{
		var accepted = new List<Part>();
		foreach (var part in parts)
		{
			while (part.Next is not ("A" or "R"))
			{
				workflows[part.Next].Process(part);
			}

			if(part.Next == "A")
				accepted.Add(part);
		}

		return accepted.Sum(p => p.Categories.Values.Sum()).ToString();
	}

	public override string SolvePart2()
	{
		return string.Empty;
	}

	public record WorkFlow(string Tag, List<(char c, char op, int value, string result)> Rules)
	{
		public void Process(Part p)
		{
			foreach (var (c, op, value, result) in Rules.SkipLast(1))
			{
				if (!Accepted(p.Categories[c], op, value))
					continue;

				p.Next = result;
				return;
			}

			p.Next = Rules.Last().result;
		}

		private static bool Accepted(int p, char op, int v) => op == '>' ? p > v : p < v;
	}

	public record Part(Dictionary<char, int> Categories)
	{
		public string Next { get; set; } = "in";
	};
}
