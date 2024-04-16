using System;
using System.Drawing;
using Common.Interfaces;
using Common.Renders;

namespace AoC2018;

public class Day15 : Solution
{
	private Grid2d grid;

	public Day15(string file) : base(file) => grid = new(Input, diagonalAllowed: false);


	public CombatState CreateInitialState() =>
		new(0, grid
			.Where(c => c.Character is 'G' or 'E')
			.Select(c => new Unit(c.Character, c.Position, 200))
			.ToList( ));

	public override async Task<string> SolvePart1()
	{
		var state = CreateInitialState( );
		var targetsRemaining = true;

		Console.WriteLine($"Initial state");
		Console.WriteLine(grid);

		while (targetsRemaining)
		{
			var units = state.GetUnits( );
			foreach (var unit in units)
			{
				if (unit.IsDead)
					continue;

				//Each unit begins its turn by identifying all possible targets
				var targets = state.GetTargets(unit);

				//If there are no targets remaining combat ends
				if (targets.Count == 0)
				{
					targetsRemaining = false;
					break;
				}

				//If the unit is already in range of a target, it does not move, but continues its turn with an attack
				var targetsInRange = state.GetTargetsInRange(unit);

				if (targetsInRange.Count( ) > 0)
				{
					//When multiple targets are in range, the ones with the lowest hit points are selected
					//Of those, the first in reading order is selected.
					var target = targetsInRange
						.GroupBy(t => t.HitPoints)
						.OrderBy(g => g.Key).First( )
						.OrderBy(t => t.Position.Y)
						.ThenBy(t => t.Position.X).First( );

					target.TakeDamage(unit.Attack);
					if (target.IsDead)
					{
						grid[target.Position].Character = '.';
					}

					continue;
				}


				//the unit identifies all of the open squares (.) that are in range of each target
				var inRange = new HashSet<Grid2d.Cell>( );
				foreach (var target in targets)
				{
					grid.GetNeighbors(grid[target.Position], n => n.Character == '.').Cast<Grid2d.Cell>( )
						.ForEach(n => inRange.Add(n));
				}


				//Determines which open cells next to targets the unit could reach in the fewest steps.
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
					continue;

				//If multiple squares are in range and tied for being reachable in the fewest steps, the square which is first in reading order is chosen.
				var nearest = reachable
					.GroupBy(kvp => kvp.Value).OrderBy(g => g.Key).First( )
					.OrderBy(kvp => kvp.Key.Y).ThenBy(kvp => kvp.Key.X).First( ).Key;

				//If multiple steps would put the unit equally closer to its destination, the unit chooses the step which is first in reading order. 
				var newPosition = await GetNextStep(unit.Position, nearest.Position);

				//actually move
				grid[unit.Position].Character = '.';
				unit.Position = newPosition;
				grid[newPosition].Character = unit.Type;

				//After moving (or if the unit began its turn in range of a target), the unit attacks.
				targetsInRange = state.GetTargetsInRange(unit);

				if (targetsInRange.Count( ) > 0)
				{
					//When multiple targets are in range, the ones with the lowest hit points are selected
					//Of those, the first in reading order is selected.
					var target = targetsInRange
						.GroupBy(t => t.HitPoints)
						.OrderBy(g => g.Key).First( )
						.OrderBy(t => t.Position.Y)
						.ThenBy(t => t.Position.X).First( );

					target.TakeDamage(unit.Attack);
					if (target.IsDead)
					{
						grid[target.Position].Character = '.';
					}

					continue;
				}
			}

			state = state with { Round = state.Round + 1 };

			Console.WriteLine($"Round {state.Round}");
			Console.WriteLine(grid);
			var stats = state.Units.Aggregate(new StringBuilder(), (builder, unit) =>
				builder.AppendLine($"Unit {unit.Type} | Position {unit.Position} | HP: {unit.HitPoints} "));
			Console.WriteLine(stats.ToString());
		}


		if (IRenderState.IsActive)
			await IRenderState.Update(new CombatRender(grid));

		return state.GetOutcome( ).ToString( );
	}

	public async Task<(int x, int y)> GetNextStep((int x, int y) unitPosition, (int x, int y) nearest)
	{
		//If multiple steps would put the unit equally closer to its destination, consider the first step of each such path.
		var steps = new Dictionary<Grid2d.Cell, int>( );
		var neighbors = grid.GetNeighbors(grid[unitPosition], n => n.Character == '.');
		foreach (var neighbor in neighbors)
		{
			var path = await PathFinding.BreadthFirstSearch(neighbor, grid[nearest], grid,
				(_, n) => n.Character == '.',
				(current, goal) => current.X == goal.X && current.Y == goal.Y);

			if (path.Contains(grid[nearest]))
			{
				steps.Add(path.Last( ).Cast<Grid2d.Cell>( ), path.Count( ));
			}
		}

		return steps.GroupBy(kvp => kvp.Value).OrderBy(g => g.Key).First( )
			.OrderBy(kvp => kvp.Key.Y).ThenBy(kvp => kvp.Key.X).First( ).Key.Position;
	}



	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	public record CombatState(int Round, List<Unit> Units)
	{
		public int GetOutcome() =>
			(Round -1) * Units.Where(u => !u.IsDead).Sum(u => u.HitPoints);

		public List<Unit> GetTargets(Unit unit) =>
			Units.Where(u => u.Type != unit.Type && !u.IsDead).ToList( );

		public Unit GetUnit((int x, int y) pos) =>
			Units.First(u => u.Position == pos);

		public IEnumerable<Unit> GetUnits() => Units
			.Where(u => !u.IsDead)
			.OrderBy(u => u.Position.Y)
			.ThenBy(u => u.Position.X);

		public List<Unit> GetTargetsInRange(Unit unit)
		{
			var offsets = new List<(int x, int y)> { (1, 0), (0, -1), (-1, 0), (0, 1) }
				.Select(o => unit.Position.Add(o));
			var targets = Units.Where(u => offsets.Contains(u.Position) && !u.IsDead && u.Type != unit.Type);
			return targets.ToList( );
		}
	}

	public record Unit(char Type, (int X, int Y) Position, int HitPoints, int Attack = 3)
	{
		public int HitPoints { get; private set; } = HitPoints;
		public (int X, int Y) Position { get; set; } = Position;
		public void TakeDamage(int attack) => HitPoints -= attack;
		public bool IsDead => HitPoints <= 0;

	}
}
