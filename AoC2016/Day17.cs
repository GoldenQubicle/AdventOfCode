namespace AoC2016
{
    public class Day17 : Solution
    {
        private readonly List<(int x, int y, int p, string dir)> offsets = new()
        {
            (0, -1, 0, "U"),
            (0, 1, 1, "D"),
            (-1, 0, 2, "L"),
            (1, 0, 3, "R"),
        };

        public string PassCode { get; set; }

        public Day17(string file) : base(file) => PassCode = Input[0];

        public Day17(List<string> input) : base(input) => PassCode = Input[0];

        public override async Task<string> SolvePart1( )
        {
            var current = (room: (x: 0, y: 0), path: string.Empty);
            var queue = new Queue<((int x, int y) room, string path)>();
            queue.Enqueue(current);

            while(queue.Any())
            {
                current = queue.Dequeue();
                if(current.room.x == 3 && current.room.y == 3) break;

                GetMoves(current).ForEach(move => queue.Enqueue(move));
            }
            return current.path;
        }

        public override async Task<string> SolvePart2( )
        {
            var current = (room: (x: 0, y: 0), path: string.Empty);
            var queue = new Queue<((int x, int y) room, string path)>();
            var paths = new List<int>();

            queue.Enqueue(current);

            while(queue.Any())
            {
                current = queue.Dequeue();
                if(current.room.x == 3 && current.room.y == 3)
                {
                    paths.Add(current.path.Length);
                    continue;
                }

                GetMoves(current).ForEach(move => queue.Enqueue(move));
            }
            return paths.Max().ToString();
        }

        private List<((int x, int y) room, string path)> GetMoves(((int x, int y) room, string path) current)
        {
            var hash = Maths.HashToHexadecimal($"{PassCode}{current.path}").ToLowerInvariant();
            return offsets
                .Where(o => isOpen(hash[o.p]) && isValid(current.room, o))
                .Select(o => (room: (current.room.x + o.x, current.room.y + o.y), path: $"{current.path}{o.dir}")).ToList();
        }

        private bool isValid((int x, int y) currentRoom, (int x, int y, int p, string dir) offset) =>
            currentRoom.x + offset.x >= 0 && currentRoom.x + offset.x < 4 &&
            currentRoom.y + offset.y >= 0 && currentRoom.y + offset.y < 4;

        private bool isOpen(char c) => c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f';
    }
}