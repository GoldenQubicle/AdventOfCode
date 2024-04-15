using Common.Interfaces;
using Common.Renders;

namespace AoC2018;

public class Day15 : Solution
{
	private Grid2d grid;

	public Day15(string file) : base(file) => grid = new (Input);

	public override async Task<string> SolvePart1()
	{
		if (IRenderState.IsActive)
			await IRenderState.Update(new CombatRender(grid));

		return string.Empty;
	}

	public override async Task<string> SolvePart2()
	{
		return string.Empty;
	}

	public record CombatState(int Turn, List<Unit> Units)
	{
		public int GetOutcome() => Turn * Units.Sum(u => u.HitPoints);
	}

	public record Unit(Actor Actor, int HitPoints, int Attack = 3);

	public enum Actor { Elf, Goblin }

}
