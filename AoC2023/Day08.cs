using System.Linq;
using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2023;

public class Day08 : Solution
{
	private readonly Dictionary<string, (string l, string r)> nodes;
	private readonly string instructions;

	public Day08(string file) : base(file)
	{
		instructions = Input[0];
		nodes = Input.Skip(1)
			.Select(l => Regex.Match(l, @"(?<n>[A-Z]{3}).*(?<l>[A-Z]{3}). (?<r>[A-Z]{3})"))
			.ToDictionary(m => m.Groups["n"].Value, m => (m.Groups["l"].Value, m.Groups["r"].Value));

	}

	public override string SolvePart1()
	{
		var current = "AAA";
		var idx = 0;
		var steps = 0;
		while (current != "ZZZ")
		{
			current = instructions[idx] == 'L' ? nodes[current].l : nodes[current].r;
			idx = (idx + 1) % instructions.Length;
			steps++;
		}

		return steps.ToString( );
	}

	public override string SolvePart2()
	{
		var current = nodes.Keys.Where(n => n.EndsWith('A')).ToList( );
		var paths = current.Select((n, i) => (n, i)).ToDictionary(t => t.i, _ => (found: false, steps: 0));
		var idx = 0;
		var steps = 0;

		while (paths.Values.Any(p => !p.found))
		{
			steps++;

			current = current.Select(n => instructions[idx] == 'L' ? nodes[n].l : nodes[n].r).ToList( );

			current.Select((n, i) => (isEnd: n.EndsWith('Z'), i))
				.Where(t => t.isEnd)
				.ForEach(t => paths[t.i] = (true, steps));

			idx = (idx + 1) % instructions.Length;
			
		}
		//TODO figure out how to calculate lcm, for now just put the path results in online calculator :)
		return string.Empty;
	}
}
