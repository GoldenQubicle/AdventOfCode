using Common.Interfaces;

namespace Common.Renders;

public class CombatRender(Grid2d grid) : IRenderState
{
	public Grid2d Grid { get; } = grid;
}