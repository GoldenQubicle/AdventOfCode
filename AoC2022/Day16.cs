namespace AoC2022
{
    public class Day16 : Solution
    {
        private readonly Dictionary<string, (int rate, List<string> tunnels)> valves;

        public Day16(string file) : base(file) => valves = Input.Select(line => line.Split(";"))
            .ToDictionary(p => p[0][6..8], p => (rate: p[0].AsInteger(), tunnels: p[1][23..].Split(",").Select(s => s.Trim()).ToList()));

        public override string SolvePart1()
        {
            var result = new List<int>();
            var queue = new Queue<(string valve, int relieved, int step)>();

            queue.Enqueue(("AA", 0, 0));

            
            
            while (queue.Count > 0)
            {
                // state consist of; dictionary valves open / closed, ticks elapsed, pressure relieved, current valve
                // get all closed valves with flow rate > 0 for current
                // foreach option
                // - compute the shortest path to it (i.e. travel time 1 * n nodes)
                // - compute the amount of pressure relieved during

                var current = queue.Dequeue();

                if (current.step == 15)
                    result.Add(current.relieved);
                else
                    valves[current.valve].tunnels.ForEach(t =>
                        queue.Enqueue((t, current.relieved * 2 + valves[current.valve].rate, current.step++)));
            }


            return result.Max().ToString();
        }

        public override string SolvePart2() => null;
    }
}