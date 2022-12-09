namespace AoC2022
{
    public class Day09 : Solution
    {
        public Day09(string file) : base(file) { }
        
        public Day09(List<string> input) : base(input) { }

        public override string SolvePart1()
        {
            var head = (x: 0, y: 0);
            var tail = (x: 0, y: 0);
            var visited = new HashSet<(int, int)> { tail };

            foreach (var step in Input)
            {
                var parts = step.Split(" ");
                var current = parts[0];

                for (var i = 0; i < int.Parse(parts[1]); i++)
                {
                    head = current switch
                    {
                        "R" => head.Add(1, 0),
                        "L" => head.Add(-1, 0),
                        "U" => head.Add(0, 1),
                        "D" => head.Add(0, -1),
                    };

                    if(OverLaps(head, tail)) continue;

                    tail = current switch
                    {
                        "R" when head.y < tail.y => tail.Add(1, -1),
                        "R" when head.y > tail.y => tail.Add(1, 1),
                        "R" => tail.Add(1, 0),
                        "L" when head.y < tail.y => tail.Add(-1, -1),
                        "L" when head.y > tail.y => tail.Add(-1, 1),
                        "L" => tail.Add(-1, 0),
                        "U" when head.x < tail.x => tail.Add(-1, 1),
                        "U" when head.x > tail.x => tail.Add(1, 1),
                        "U"  => tail.Add(0, 1),
                        "D" when head.x < tail.x => tail.Add(-1, -1),
                        "D" when head.x > tail.x => tail.Add(1, -1),
                        "D"  => tail.Add(0, -1),
                    };

                    visited.Add(tail);
                }

                
            }
            return visited.Count.ToString();
        }

        public static bool OverLaps((int x, int y) head, (int x, int y) tail) => 
            tail.x >= head.x - 1 && tail.x <= head.x + 1 &&
            tail.y >= head.y - 1 && tail.y <= head.y + 1;


        public override string SolvePart2( ) => null;
    }
}