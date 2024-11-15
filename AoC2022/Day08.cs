namespace AoC2022
{
    public class Day08 : Solution
    {
        private readonly Grid2d grid;

        public Day08(string file) : base(file) => grid = new Grid2d(Input, diagonalAllowed: false);

        public override async Task<string> SolvePart1() => grid.Where(FilterEdge)
            .Aggregate(grid.Count, (visible, cell) =>
            {
                var hiddenLeft = IsHidden(c => c.X < cell.X && c.Y == cell.Y, cell.Value);
                var hiddenRight = IsHidden(c => c.X > cell.X && c.Y == cell.Y, cell.Value);
                var hiddenUp = IsHidden(c => c.X == cell.X && c.Y < cell.Y, cell.Value);
                var hiddenDown = IsHidden(c => c.X == cell.X && c.Y > cell.Y, cell.Value);

                return (hiddenLeft && hiddenRight && hiddenUp && hiddenDown) ? --visible : visible;
            }).ToString();

        public override async Task<string> SolvePart2() => grid.Where(FilterEdge)
            .Select(cell =>
            {
                var scoreLeft = WalkLine(step => (cell.X - step, cell.Y), cell.Value);
                var scoreRight = WalkLine(step => (cell.X + step, cell.Y), cell.Value);
                var scoreUp = WalkLine(step => (cell.X, cell.Y - step), cell.Value);
                var scoreDown = WalkLine(step => (cell.X, cell.Y + step), cell.Value);

                return scoreLeft * scoreRight * scoreUp * scoreDown;
            }).Max().ToString();


        private bool IsHidden(Func<Cell, bool> direction, long value) =>
            grid.GetCells(direction).Any(c => c.Value >= value);

        private bool FilterEdge(Cell cell) =>
            cell.X >= 1 && cell.X < grid.Width &&
            cell.Y >= 1 && cell.Y < grid.Height;

        private int WalkLine(Func<int, (int x, int y)> getNextPosition, long value)
        {
            var isBlocked = false;
            var step = 1;

            while (!isBlocked)
            {
                var pos = getNextPosition(step);
                if (pos.x < 0 || pos.x >= grid.Width ||
                    pos.y < 0 || pos.y >= grid.Height)
                    return step - 1;

                if (grid[pos].Value < value) step++;
                else isBlocked = true;
            }

            return step;
        }
    }
}