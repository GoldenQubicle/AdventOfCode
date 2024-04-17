namespace AoC2018;

public class Day15(string file) : Solution(file)
{
	public override async Task<string> SolvePart1()
	{
		var grid = CreateInitialGrid( );
		var state = CreateInitialState(grid);

		state = await DoCombat(state, grid);

		return state.GetOutcome( ).ToString( );
	}

	public override async Task<string> SolvePart2()
	{
		var elfAttack = 4; // 15 
		var grid = CreateInitialGrid( );
		var state = CreateInitialState(grid, elfAttack);

		while (true)
		{
			state = await DoCombat(state, grid);

			if (state.ElvesSurvived( ))
				break;

			elfAttack++;
			grid = CreateInitialGrid( );
			state = CreateInitialState(grid, elfAttack);
		}

		return state.GetOutcome( ).ToString( );
	}

	public Grid2d CreateInitialGrid() => new(Input, diagonalAllowed: false);

	public static CombatState CreateInitialState(Grid2d grid, int elfAttack = 3)
	{
		var elves = grid.Where(c => c.Character == 'E').Select(c => new Unit(c.Character, c.Position, 200, elfAttack));
		var goblins = grid.Where(c => c.Character == 'G').Select(c => new Unit(c.Character, c.Position, 200, 3));
		return new(0, elves.Concat(goblins).ToList( ));
	}

	private static async Task<CombatState> DoCombat(CombatState state, Grid2d grid)
	{
		var targetsRemaining = true;

		//Console.WriteLine($"Initial state");
		//Console.WriteLine(grid);

		while (targetsRemaining)
		{
			var units = state.GetUnits( );
			foreach (var unit in units)
			{
				if (unit.IsDead)
					continue;

				var targets = state.GetTargets(unit);

				if (!targets.Any())
				{
					targetsRemaining = false;
					break;
				}

				if (TryAttack(unit, state, grid))
					continue;

				var (hasNearest, nearest) = await GetNearest(unit, targets, grid);

				if (!hasNearest)
					continue;

				await MoveUnit(unit, nearest!, grid);

				TryAttack(unit, state, grid);
			}

			state = state with { Round = state.Round + 1 };

			Console.WriteLine($"Round {state.Round}");
			Console.WriteLine(grid);
			//var stats = state.Units.Aggregate(new StringBuilder( ), (builder, unit) =>
			//	builder.AppendLine($"Unit {unit.Type} | Position {unit.Position} | HP: {unit.HitPoints} "));
			//Console.WriteLine(stats.ToString( ));
		}

		return state;
	}

	private static async Task MoveUnit(Unit unit, Grid2d.Cell nearest, Grid2d grid)
	{
		var newPosition = await GetNextStep(unit.Position, nearest.Position, grid);

		grid[unit.Position].Character = '.';
		grid[newPosition].Character = unit.Type;
		unit.Position = newPosition;
	}


	private static async Task<(bool hasNearest, Grid2d.Cell? nearest)> GetNearest(Unit unit, IEnumerable<Unit> targets, Grid2d grid)
	{
		//Identifies all of the open squares (.) that are in range of each target and
		//determines which open cells next to targets the unit could reach in the fewest steps.
		var inRange = GetCellsInRange(targets, grid);
		var reachable = new Dictionary<Grid2d.Cell, int>( );

		foreach (var open in inRange)
		{
			var path = await PathFinding.BreadthFirstSearch(grid[unit.Position], open, grid,
				(_, neighbor) => neighbor.Character == '.',
				(current, goal) => current.X == goal.X && current.Y == goal.Y);

			if (path.Contains(open))
				reachable.Add(open, path.Count( ));
		}

		//If the unit cannot reach (find an open path to) any of the squares that are in range, it ends its turn.
		if (reachable.Count == 0)
			return (false, null);

		//If multiple squares are in range and tied for being reachable in the fewest steps, the square which is first in reading order is chosen.
		var nearest = reachable
			.GroupBy(kvp => kvp.Value).OrderBy(g => g.Key).First( )
			.OrderBy(kvp => kvp.Key.Y).ThenBy(kvp => kvp.Key.X).First( ).Key;

		return (true, nearest);
	}

	private static HashSet<Grid2d.Cell> GetCellsInRange(IEnumerable<Unit> targets, Grid2d grid) => targets
			.SelectMany(t => grid.GetNeighbors(grid[t.Position], n => n.Character == '.'))
			.Aggregate(new HashSet<Grid2d.Cell>( ), (set, node) =>
			{
				set.Add(node.Cast<Grid2d.Cell>( ));
				return set;
			});

	private static bool TryAttack(Unit unit, CombatState state, Grid2d grid)
	{
		var (inIsRange, target) = state.IsTargetInRange(unit);

		if (!inIsRange)
			return false;

		target!.TakeDamage(unit.Attack);

		if (target.IsDead)
			grid[target.Position].Character = '.';

		return true;
	}

	public static async Task<(int x, int y)> GetNextStep((int x, int y) unitPosition, (int x, int y) nearest, Grid2d grid)
	{
		var steps = new Dictionary<Grid2d.Cell, int>( );

		foreach (var neighbor in grid.GetNeighbors(grid[unitPosition], n => n.Character == '.'))
		{
			var path = await PathFinding.BreadthFirstSearch(neighbor, grid[nearest], grid,
				(_, n) => n.Character == '.',
				(current, goal) => current.X == goal.X && current.Y == goal.Y);

			//check if path actually contains the target we're looking for
			if (path.Contains(grid[nearest]))
			{
				//paths are returned in reversed order, i.e. start point is last
				steps.Add(path.Last( ).Cast<Grid2d.Cell>( ), path.Count( ));
			}
		}

		//If multiple steps would put the unit equally closer to its destination, consider the first step of each such path.
		return steps
			.GroupBy(kvp => kvp.Value).OrderBy(g => g.Key).First( )
			.OrderBy(kvp => kvp.Key.Y).ThenBy(kvp => kvp.Key.X).First( ).Key.Position;
	}


	public record CombatState(int Round, List<Unit> Units)
	{
		public int GetOutcome() =>
			(Round - 1) * Units.Where(u => !u.IsDead).Sum(u => u.HitPoints);

		public IEnumerable<Unit> GetTargets(Unit unit) =>
			Units.Where(u => u.Type != unit.Type && !u.IsDead);

		public IEnumerable<Unit> GetUnits() => Units
			.Where(u => !u.IsDead)
			.OrderBy(u => u.Position.Y)
			.ThenBy(u => u.Position.X);

		public bool ElvesSurvived() =>
			!Units.Any(u => u is { Type: 'E', IsDead: true });

		public (bool result, Unit? target) IsTargetInRange(Unit unit)
		{
			var offsets = new List<(int x, int y)> { (1, 0), (0, -1), (-1, 0), (0, 1) }
				.Select(o => unit.Position.Add(o));

			var targetsInRange = Units.Where(u => offsets.Contains(u.Position) && !u.IsDead && u.Type != unit.Type).ToList( );

			if (targetsInRange.Count == 0)
				return (false, null);

			//When multiple targets are in range, the ones with the lowest hit points are selected
			//Of those, the first in reading order is selected.
			var target = targetsInRange
				.GroupBy(t => t.HitPoints)
				.OrderBy(g => g.Key).First( )
				.OrderBy(t => t.Position.Y)
				.ThenBy(t => t.Position.X).First( );

			return (true, target);
		}
	}

	public record Unit(char Type, (int X, int Y) Position, int HitPoints, int Attack)
	{
		public int HitPoints { get; private set; } = HitPoints;
		public (int X, int Y) Position { get; set; } = Position;
		public void TakeDamage(int attack) => HitPoints -= attack;
		public bool IsDead => HitPoints <= 0;

	}
}
