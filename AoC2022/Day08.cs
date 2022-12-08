using static Common.Grid2d;

namespace AoC2022
{
    public class Day08 : Solution
    {
        private readonly Grid2d grid;

        public Day08(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: false);

        public override string SolvePart1()
        {
            var totalVisible = grid.Count;

            foreach (var cell in grid.Where(FilterEdge))
            {
                var hiddenLeft = IsHidden(c => c.X < cell.X && c.Y == cell.Y, cell.Value);
                var hiddenRight = IsHidden(c => c.X > cell.X && c.Y == cell.Y, cell.Value);
                var hiddenUp = IsHidden(c => c.X == cell.X && c.Y < cell.Y, cell.Value);
                var hiddenDown = IsHidden(c => c.X == cell.X && c.Y > cell.Y, cell.Value);

                if (hiddenLeft && hiddenRight && hiddenDown && hiddenUp)
                    totalVisible--;
            }

            return totalVisible.ToString();
        }

        public override string SolvePart2()
        {
            var scores = new Dictionary<Cell, int>();

            foreach (var cell in grid.Where(FilterEdge))
            {
                var scoreLeft = WalkLine(cell, i => (cell.X - i, cell.Y));
                var scoreRight = WalkLine(cell, i => (cell.X + i, cell.Y));
                var scoreUp = WalkLine(cell, i => (cell.X, cell.Y - i));
                var scoreDown = WalkLine(cell, i => (cell.X, cell.Y + i));

                scores.Add(cell, scoreLeft * scoreRight * scoreUp * scoreDown);
            }

            return scores.Values.Max().ToString();
        }

        private bool IsHidden(Func<Cell, bool> direction, long value) => 
            grid.GetCells(direction).Any(c => c.Value >= value);

        private bool FilterEdge(Cell cell) =>
            cell.X >= 1 && cell.X < grid.Dimensions.x &&
            cell.Y >= 1 && cell.Y < grid.Dimensions.y;

        private int WalkLine(Cell cell, Func<int, (int x, int y)> getNextPosition)
        {
            var isBlocked = false;
            var i = 1;

            while (!isBlocked)
            {
                var (x, y) = getNextPosition(i);
                if (x < 0 || x >= grid.Dimensions.x ||
                    y < 0 || y >= grid.Dimensions.y)
                    return i - 1;

                if (grid[x, y].Value < cell.Value) i++;
                else isBlocked = true;
            }

            return i;
        }
    }
}