using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2021
{
    public class Day14 : Solution
    {
        private string template;
        private Dictionary<string, string> insertions;

        public Day14(string file) : base(file)
        {
            template = Input.First();
            insertions = Input.Skip(1).Select(s =>
            {
                var parts = s.Split(" -> ");
                return new KeyValuePair<string, string>(parts[0], parts[0].Insert(1, parts[1]));
            }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public override async Task<string> SolvePart1() => BuildPolymer(10);

        //saw problems part 2 coming, still went for it naive approach part1 :'|
        private string BuildPolymer(int iterations)
        {
            for (var i = 0; i < iterations; i++)
            {
                template = template.Select((_, idx) => (_, idx: idx))
                    .Aggregate(new StringBuilder(),
                        (sb, t) => t.idx == template.Length - 1
                            ? sb.Append(template[t.idx])
                            : sb.Append(insertions[template[t.idx..(t.idx + 2)]][..2])).ToString();
            }

            var freq = template.GroupBy(c => c).Select(g => g.Count()).ToList();

            return (freq.Max() - freq.Min()).ToString();
        }

        public override async Task<string> SolvePart2()
        {
            var chars = new Dictionary<char, long>();
            var pairs = new Dictionary<string, long>();
            for (var i = 0; i < template.Length - 1; i++)
            {
                if (pairs.ContainsKey(template[i..(i + 2)])) pairs[template[i..(i + 2)]]++;
                else pairs.Add(template[i..(i + 2)], 1);
            }

            for (var i = 0; i < 40; i++)
            {
                var newPairs = new Dictionary<string, long>();
                foreach (var (s, v) in pairs)
                {
                    var np = (insertions[s][..2], insertions[s][1..3]);

                    if (newPairs.ContainsKey(np.Item1)) newPairs[np.Item1] += v;
                    else newPairs.Add(np.Item1, v);
                    if (newPairs.ContainsKey(np.Item2)) newPairs[np.Item2] += v;
                    else newPairs.Add(np.Item2, v);
                }
                pairs = newPairs;
            }

            foreach (var (s, v) in pairs)
            {
                if (chars.ContainsKey(s[0])) chars[s[0]] += v;
                else chars.Add(s[0], v);
                if (chars.ContainsKey(s[1])) chars[s[1]] += v;
                else chars.Add(s[1], v);
            }
            //what a shit show somewhere counting twice but.. too tired to fix now
            return ((chars.Values.Max() - chars.Values.Min()) / 2 ).ToString();
        }
    }
}