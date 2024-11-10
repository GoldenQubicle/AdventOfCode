namespace AoC2016
{
    public class Day01 : Solution
    {
        public enum Direction
        {
            North,
            East,
            South,
            West
        };

        public enum Turn
        {
            Left, Right
        }

        public Day01(string file) : base(file) { }

        public Day01(List<string> input) : base(input) { }

        public override async Task<string> SolvePart1( )
        {
            var current = Direction.North;
            var position = (x: 0, y: 0);

            Input[0].Split(",").ForEach(s =>
            {
                var turn = s.Contains("R") ? Turn.Right : Turn.Left;
                current = GetDirection(current, turn);
                var distance = s.ToInt();
                position = UpdatePosition(position, current, distance);
            });

            return (Math.Abs(position.x) + Math.Abs(position.y)).ToString();
        }

        public override async Task<string> SolvePart2( )
        {
            var current = Direction.North;
            var position = (x: 0, y: 0);
            (int x, int y) intersection = default;
            var visited = new HashSet<(int, int)>();

            foreach(var instruction in Input[0].Split(","))
            {
                var turn = instruction.Contains("R") ? Turn.Right : Turn.Left;
                current = GetDirection(current, turn);
                var distance = instruction.ToInt();

                for(var s = 0 ; s < distance ; s++)
                {
                    var step = UpdatePosition(position, current, s);

                    if(visited.Add(step)) continue;

                    intersection = step;
                    break;
                }

                if(intersection != default)
                    break;

                position = UpdatePosition(position, current, distance);

            }
            return (Math.Abs(intersection.x) + Math.Abs(intersection.y)).ToString();
        }



        private static (int x, int y) UpdatePosition((int x, int y) position, Direction current, int distance)
        {
            position = current switch
            {
                Direction.North => (x: position.x, y: position.y + distance),
                Direction.East => (x: position.x + distance, y: position.y),
                Direction.South => (x: position.x, y: position.y - distance),
                Direction.West => (x: position.x - distance, y: position.y),
                _ => throw new ArgumentOutOfRangeException()
            };
            return position;
        }


        public Direction GetDirection(Direction current, Turn turn) => (current, turn) switch
        {
            (Direction.North, Turn.Right) or (Direction.South, Turn.Left) => Direction.East,
            (Direction.East, Turn.Right) or (Direction.West, Turn.Left) => Direction.South,
            (Direction.South, Turn.Right) or (Direction.North, Turn.Left) => Direction.West,
            (Direction.West, Turn.Right) or (Direction.East, Turn.Left) => Direction.North,
            _ => throw new NotSupportedException()
        };


    }
}