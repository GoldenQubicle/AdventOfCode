using System.Numerics;

namespace AoC2022
{
    public class Day17 : Solution
    {
        private List<Shape> Shapes = new()
        {
            new() { Blocks = new() { new(0,0), new(1,0),  new(2,0),   new (3,0) }             },
            new() { Blocks = new() { new(0,0), new(1,0),  new(2,0),   new (1,1), new(1, -1) } },
            new() { Blocks = new() { new(0,0), new(1,0),  new(2,0),   new(2,1),  new(2,2) }   },
            new() { Blocks = new() { new(0,0), new(0,-1), new(0,-2),  new(0,-3) }             },
            new() { Blocks = new() { new(0,0), new(1,0),  new (0,-1), new (1, -1) }           }
        };

        public Day17(string file) : base(file) { }

        public override string SolvePart1()
        {
            return string.Empty;
        }

        public override string SolvePart2() => null;
    }

    internal enum Direction
    {
        Left, Right, Down
    }

    internal class Shape
    {
        public List<Vector2> Blocks { get; set; }

        public bool Overlaps(Shape s) => Blocks.Any(b => s.Blocks.Contains(b));
        
        public void Spawn(Vector2 pos) => Blocks = Blocks.Select(b => b + pos).ToList();

        public void Move(Direction direction)
        {
            Blocks = direction switch
            {
                Direction.Left => Blocks.Select(b => b - Vector2.UnitX).ToList(),
                Direction.Right => Blocks.Select(b => b + Vector2.UnitX).ToList(),
                Direction.Down => Blocks.Select(b => b - Vector2.UnitY).ToList(),
                _ => Blocks
            };
        }
    }
}