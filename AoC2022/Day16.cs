namespace AoC2022
{
    public class Day16 : Solution
    {
        private readonly Dictionary<string, (int rate, List<string> tunnels)> valves;

        public Day16(string file) : base(file) => valves = Input.Select(line => line.Split(";"))
            .ToDictionary(p => p[0][6..8], p => (rate: p[0].AsInteger(), tunnels: p[1][23..].Split(",").Select(s => s.Trim()).ToList()));

        public override async Task<string> SolvePart1()
        {
            //SETUP
            //precompute travel times between all nodes
            var nodes = valves.Keys.ToList();
            var graph = valves.ToDictionary(v => v.Key, v => v.Value.tunnels);
            var paths = Enumerable.Range(0, valves.Count - 1)
	            .SelectMany(i => Enumerable.Range(i, valves.Count - i - 1)
		            .Select(k => (start: nodes[i], end: nodes[k], GetPath(nodes[i], nodes[k], graph))))
	            .GroupBy(t => t.start)
	            .ToDictionary(g => g.Key, g => g.ToDictionary(t => t.end, t => t.Item3));

            var path = GetPath("AA", "HH", graph);

            var isOpen = valves.Where(v => v.Value.rate > 0).ToDictionary(v => v.Key, _ => false);
            //FLOW
            //given the current and elapsed time, which next valve to open will give the largest flow in the time remaining?
            //i.e. taking into account travel time and 1 minute, max (elapsed-timeSpend) * flowRate
            //update timeSpend
            //update totalFlow



	        return string.Empty;
        }

        private List<string> GetPath(string start, string end, Dictionary<string, List<string>> graph)
        {
	        var queue = new Queue<(string node, List<string> visited)>();
	        queue.Enqueue((start, new List<string>()));

            while (queue.Count > 0)
            {
	            var current = queue.Dequeue();
	            if (current.node == end)
		            return current.visited;

                queue.EnqueueAll(graph[current.node].Select(n => (n, current.visited.Expand(current.node))));

            }

            return new List<string>();
        }

        public override async Task<string> SolvePart2() => null;
    }
}