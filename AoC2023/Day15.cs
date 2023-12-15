using System.Text.RegularExpressions;
using Common;
using Common.Extensions;

namespace AoC2023;

public class Day15 : Solution
{
	private readonly List<string> sequence;

	public Day15(string file) : base(file) => 
		sequence = Input[0].Split(',').ToList();
    

    public override string SolvePart1( ) => sequence
	    .Select(s => s.Aggregate(0, (v, c) => CalculateHash(c, v))).Sum( ).ToString( );


	public override string SolvePart2( )
	{
		var boxes = Enumerable.Range(0, 256).ToDictionary(n => n, _ => new List<string>());
		var labels2Boxes = new Dictionary<string, int>();
		var labels2Focus = new Dictionary<string, int>();

		sequence
			.Select(s => Regex.Match(s, @"(?<label>[a-z]*)(?<op>-|=\d+)"))
			.Select(m => (l: m.AsString("label"), o: m.AsString("op")))
			.ForEach(t =>
			{
				labels2Boxes.TryAdd(t.l, t.l.Aggregate(0, (v, c) => CalculateHash(c, v)));

				if (t.o.Contains('='))
				{
					if (!labels2Focus.TryAdd(t.l, t.o.AsInteger()))
						labels2Focus[t.l] = t.o.AsInteger();

					if(!boxes[labels2Boxes[t.l]].Contains(t.l))
						boxes[labels2Boxes[t.l]].Add(t.l);
				}

				if (t.o.Contains('-'))
				{
					boxes[labels2Boxes[t.l]].Remove(t.l);
				}
			});


		return boxes
			.Sum(kvp => kvp.Value.Select((l, i) => (kvp.Key + 1) * (i + 1) * labels2Focus[l]).Sum())
			.ToString();
	}


	public static int CalculateHash(char c, int v)
    {
	    v += c;
	    v *= 17;
	    v %= 256;
	    return v;
    }
}
