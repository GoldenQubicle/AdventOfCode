using System.Collections.Generic;
using Common.Interfaces;

namespace Common.Renders;

public interface ICombatEvent : IRenderState
{
	int UnitId { get; init; }
}

public record UnitData(int Id, (int x, int y) Position, char Type, int HitPoints);

public record Move(int UnitId, (int x, int y) NewPosition) : ICombatEvent;

public record Attack(int UnitId, int TargetId) : ICombatEvent;

public record Death(int UnitId) : ICombatEvent;

public record NewRound(int UnitId) : ICombatEvent;
