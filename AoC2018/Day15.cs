using System.Drawing.Printing;
using System.Formats.Tar;
using System.Numerics;
using Common.Interfaces;
using Common.Renders;

namespace AoC2018;

public class Day15 : Solution
{
	private Grid2d grid;

	public Day15(string file) : base(file) => grid = new (Input, diagonalAllowed:false);



	public override async Task<string> SolvePart1()
	{
		var round = 0;
		var state = new CombatState(1, GetUnits( ).Select(u => new Unit(u.Character, u.Position, 200)).ToList());
		var targetsRemaining = true;
		
		
		while (targetsRemaining)
		{
			round++;
			Console.WriteLine($"Round {round}");
			var units = GetUnits();
			foreach (var unit in units)
			{
				
				//Each unit begins its turn by identifying all possible targets
				var targets = GetUnits( ).Where(c => c.Character != unit.Character).ToList( );
				
				//If there are no targets remaining combat ends
				if (targets.Count == 0)
				{
					targetsRemaining = false;
					break;
				}
				var inRange = new List<Grid2d.Cell>( );

				//the unit identifies all of the open squares (.) that are in range of each target
				foreach (var target in targets)
				{
					var neighbors = grid.GetNeighbors(target);

					//If the unit is already in range of a target, it does not move, but continues its turn with an attack
					if (neighbors.Contains(unit))
					{
						state.Attack(unit.Position, target.Position);
						break;
					}

					inRange.AddRange(neighbors.Where(c => c.Character is '.'));
				}

				var reachable = new List<Grid2d.Cell>( );
				//Determines which open cells next to targets the unit could reach in the fewest steps.
				foreach (var open in inRange)
				{
					var path = await PathFinding.BreadthFirstSearch(unit, open, grid,
						(_, neighbor) => neighbor.Character == '.',
						(current, goal) => current.X == goal.X && current.Y == goal.Y);
					if (path.Contains(open))
						reachable.Add(open);
				}

				//If the unit cannot reach (find an open path to) any of the squares that are in range, it ends its turn.
				if (reachable.Count == 0)
					continue;

				//If multiple squares are in range and tied for being reachable in the fewest steps, the square which is first in reading order is chosen.
				var nearest = reachable
					.GroupBy(c => Maths.GetManhattanDistance(unit.Position, c.Position)).First( )
					.OrderBy(g => g.Y).ThenBy(g => g.X).First( );

				//If multiple steps would put the unit equally closer to its destination, the unit chooses the step which is first in reading order. 
				//i.e. we always try to step on the x-axis first, then on the y-axis
				var step = unit.X == nearest.X
					? (0, unit.Y < nearest.Y ? 1 : -1)
					: (unit.X < nearest.X ? 1 : -1, 0);

				var newPosition = unit.Position.Add(step);

				Console.WriteLine(grid);
				grid[unit.Position].Character = '.';
				state.GetUnit(unit.Position).Position = newPosition;
				grid[newPosition].Character = unit.Character;
				Console.WriteLine(grid);
			}

		}
		

		if (IRenderState.IsActive)
			await IRenderState.Update(new CombatRender(grid));

		return string.Empty;
	}

	public List<Grid2d.Cell> GetUnits() =>
		grid.Where(c => c.Character is 'G' or 'E')
			.OrderBy(c => c.Y)
			.ThenBy(c => c.X).ToList();

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	public record CombatState(int Round, List<Unit> Units)
	{
		public int GetOutcome() => 
			Round * Units.Sum(u => u.HitPoints);

		public Unit GetUnit((int x, int y) position) => 
			Units.First(u => u.Position == position);

		public void Attack((int x, int y) unit, (int x, int y) target)
		{
			var t = GetUnit(target);
			t.TakeDamage(GetUnit(unit).Attack);

			if (t.IsDead)
				Units.Remove(t);
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
