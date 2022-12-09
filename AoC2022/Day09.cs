namespace AoC2022
{
    public class Day09 : Solution
    {
        private readonly List<(string direction, int steps)> motions;

        public Day09(string file) : base(file) => motions = Input
            .Select(l => l.Split(" "))
            .Select(p => (direction: p[0], steps: int.Parse(p[1]))).ToList();

        public override string SolvePart1() => SimulateRope(GetRope(2));

        public override string SolvePart2() => SimulateRope(GetRope(10));

        private string SimulateRope(List<(int x, int y)> rope) =>
            motions.Aggregate(new HashSet<(int, int)> { rope.Last() }, (visited, motion) =>
            {
                for (var s = 0; s < motion.steps; s++)
                {
                    rope[0] = motion.direction switch
                    {
                        "R" => rope[0].Add(1, 0),
                        "L" => rope[0].Add(-1, 0),
                        "U" => rope[0].Add(0, 1),
                        "D" => rope[0].Add(0, -1),
                    };

                    for (var k = 1; k < rope.Count; k++)
                        rope[k] = UpdateKnot(rope[k - 1], rope[k]);

                    visited.Add(rope.Last());
                }
                return visited;
            }).Count.ToString();

        private static (int x, int y) UpdateKnot((int x, int y) head, (int x, int y) tail) =>
            Math.Abs(head.x - tail.x) > 1 || Math.Abs(head.y - tail.y) > 1
                ? tail.Add(Math.Sign(head.x - tail.x), Math.Sign(head.y - tail.y))
                : tail;

        private List<(int x, int y)> GetRope(int length) =>
            Enumerable.Range(0, length).Select(n => (x: 0, y: 0)).ToList();

    }
}