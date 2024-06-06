namespace AoC2019;

public class Day11 : Solution
{
	public Day11(string file) : base(file) { }

	private enum Direction { Left, Right, Up, Down }

	private record Robot((int x, int y) Position, Direction Direction = Direction.Up)
	{
		public (int x, int y) Position { get; private set; } = Position;
		public Direction Direction { get; private set; } = Direction;

		public void Turn(Direction dir) => Direction = (Direction, dir) switch
		{
			(Direction.Up, Direction.Left) or (Direction.Down, Direction.Right) => Direction.Left,
			(Direction.Up, Direction.Right) or (Direction.Down, Direction.Left) => Direction.Right,
			(Direction.Left, Direction.Left) or (Direction.Right, Direction.Right) => Direction.Down,
			(Direction.Left, Direction.Right) or (Direction.Right, Direction.Left) => Direction.Up
		};

		public void Move() => Position = Direction switch
		{
			Direction.Left => Position.Add(-1, 0),
			Direction.Right => Position.Add(1, 0),
			Direction.Up => Position.Add(0, 1),
			Direction.Down => Position.Add(0, -1),
			_ => throw new ArgumentOutOfRangeException( )
		};
	}

	public override async Task<string> SolvePart1() => PaintHull(0).Count.ToString( );

	public override async Task<string> SolvePart2() => PaintHull(1).Pad( ).ToString( );

	private Grid2d PaintHull(int input)
	{
		var icc = new IntCodeComputer(Input)
		{
			BreakOnOutput = true,
			Inputs = new( ) { input }
		};

		var robot = new Robot((0, 0));
		var doPaint = true;
		var panels = new Grid2d( );

		while (icc.Execute( ))
		{
			if (icc.IsFinished)
				break;

			if (doPaint)
			{
				panels.AddOrUpdate(new Grid2d.Cell(robot.Position, icc.Output == 0 ? '.' : '#'));
				doPaint = false;
				continue;
			}

			//2nd break turn, apply to robot, move forward
			var dir = (Direction)icc.Output;
			robot.Turn(dir);
			robot.Move( );

			icc.Inputs.Add(panels.TryGetCell(robot.Position, out var panel) ? panel.Character == '.' ? 0 : 1 : 0);

			doPaint = true;
		}

		return panels;
	}


}
