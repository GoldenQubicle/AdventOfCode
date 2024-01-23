namespace AoC2022
{
    public class Day14 : Solution
    {
        public Day14(string file) : base(file) { }

        Grid2d GetGrid() => Input.Select(line => line.Split(" -> "))
            .Aggregate(new Grid2d(), (grid2d, line) =>
            {
                for (var i = 0; i < line.Length - 1; i++)
                {
                    var c1 = line[i].Split(",").Select(int.Parse).ToArray();
                    var c2 = line[i + 1].Split(",").Select(int.Parse).ToArray();

                    for (var x = 0; x <= Math.Abs(c1[0] - c2[0]); x++)
                        for (var y = 0; y <= Math.Abs(c1[1] - c2[1]); y++)
                            grid2d.AddOrUpdate(new Cell((
                                c1[0] < c2[0] ? c1[0] + x : c1[0] - x,
                                c1[1] < c2[1] ? c1[1] + y : c1[1] - y), '#'));
                }
                return grid2d;

            });

        public override async Task<string> SolvePart1()
        {
            var grid = GetGrid();
            grid.Fill('.');

            var sand = new Cell((x: 500, y: 0));
            var minx = grid.Min(c => c.X);
            var maxx = grid.Max(c => c.X);
            var maxy = grid.Max(c => c.Y);

            while (true)
            {
                var options = grid.GetNeighbors(sand, n => n.Y > sand.Y && n.Character == '.')
                    .OrderBy(n => n.X == sand.X)
                    .ThenBy(n => n.X < sand.X)
                    .ThenBy(n => n.X > sand.X).ToList();

                if (options.Any())
                {
                    sand = options.Last().Cast<Cell>();
                    continue;
                }

                // we cant move, either at rest or falling of the abyss, check x
                if (sand.X <= minx || sand.Y >= maxx || sand.Y >= maxy)
                    break;

                grid.AddOrUpdate(new(sand.Position, 'S'));
                sand = new((500, 0));
                Debug.WriteLine(grid.ToString());

            }

            return grid.Count(c => c.Character == 'S').ToString();
        }

        public override async Task<string> SolvePart2()
        {
            var grid = GetGrid();

            var minx = grid.Min(c => c.X);
            var maxx = grid.Max(c => c.X);
            var maxy = grid.Max(c => c.Y) + 2;
            //yep this takes forever now...
            for (var x = minx - (maxy/2) - 200; x <= maxx + (maxy/2) + 200; x++)
                grid.AddOrUpdate(new Cell((x, maxy), '#'));

            grid.Fill('.');

            Debug.Write(grid.ToString());

			var sand = new Cell((x: 500, y: 0));
			while (true)
            {
                var options = grid.GetNeighbors(sand, n => n.Y > sand.Y && n.Character == '.')
                    .OrderBy(n => n.X == sand.X)
                    .ThenBy(n => n.X < sand.X)
                    .ThenBy(n => n.X > sand.X).ToList();

                if (options.Any())
                {
                    sand = options.Last().Cast<Cell>();
                    continue;
                }

                if (sand.X == 500 && sand.Y == 0)
                    break;

                grid.AddOrUpdate(new(sand.Position, 'S'));
                sand = new((500, 0));
                Debug.WriteLine(grid.ToString());

            }

            return (grid.Count(c => c.Character == 'S') + 1).ToString();
        }
    }
}