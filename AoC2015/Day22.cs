using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2015
{
	public class Day22 : Solution
	{
		private readonly Dictionary<string, Spell> spells = new( )
		{
			{ nameof(MagicMissile), new MagicMissile() },
			{ nameof(Drain), new Drain() },
			{ nameof(Shield), new Shield() },
			{ nameof(Poison), new Poison() },
			{ nameof(Recharge), new Recharge() },
		};

		public Player TheBoss { get; set; }
		public Player ThePlayer { get; set; }

		private const int NumberOfGames = 15000;

		public Day22(List<string> input) : base(input) { }

		public Day22(string file) : base(file) { }

		public override async Task<string> SolvePart1() => PlayGames(NumberOfGames)
			.Where(r => r.winner.Equals("ThePlayer"))
			.MinBy(r => r.manaSpend).manaSpend.ToString( );

		public override async Task<string> SolvePart2() => PlayGames(NumberOfGames, true)
			.Where(r => r.winner.Equals("ThePlayer"))
			.MinBy(r => r.manaSpend).manaSpend.ToString( );

		private List<(string winner, int manaSpend, int bossHitpoints)> PlayGames(int games, bool isPart2 = false)
		{
			var results = new List<(string winner, int manaSpend, int bossHitpoints)>( );
			for (var i = 0 ;i < games ;i++)
			{
				spells.Values.ForEach(s => s.Reset( ));

				ThePlayer = new Player { Name = nameof(ThePlayer), HitPoints = 50, Mana = 500 };
				TheBoss = new Player { Name = nameof(TheBoss), HitPoints = 51, Damage = 9 };
				results.Add(PlayGame(isPart2));
			}
			return results;
		}

		public (string winner, int manaSpend, int bossHitpoints) PlayGame(bool isPart2 = false, IReadOnlyList<string> scenario = null)
		{
			var playerTurn = 0;
			var manaSpend = 0;

			while (true)
			{
				if (isPart2)
				{
					ThePlayer.HitPoints -= 1;
					if (ThePlayer.HitPoints <= 0)
						return (TheBoss.Name, manaSpend, TheBoss.HitPoints);
				}

				//player turn
				var activeSpells = spells.Values.Where(spell => spell.IsActive).ToList( );
				activeSpells.ForEach(spell => spell.Cast(ThePlayer, TheBoss));

				var pickedSpell = scenario == null ? PickRandomSpell( ) : spells[scenario[playerTurn]];
				pickedSpell.IsActive = true;
				ThePlayer.Mana -= pickedSpell.Cost;
				manaSpend += pickedSpell.Cost;
				playerTurn++;

				//boss turn
				activeSpells = spells.Values.Where(spell => spell.IsActive).ToList( );
				activeSpells.ForEach(spell => spell.Cast(ThePlayer, TheBoss));

				if (TheBoss.HitPoints <= 0)
					return (ThePlayer.Name, manaSpend, TheBoss.HitPoints);

				ThePlayer.HitPoints -= TheBoss.Damage - ThePlayer.Armor <= 0 ? 1 : TheBoss.Damage - ThePlayer.Armor;

				if (ThePlayer.HitPoints <= 0 || ThePlayer.Mana < 53)
					return (TheBoss.Name, manaSpend, TheBoss.HitPoints);
			}
		}

		private Spell PickRandomSpell() => spells.Values.Where(spell => !spell.IsActive).ToList( ).Random( );

		public abstract class Spell
		{
			public bool IsActive { get; set; }
			protected int ActiveRounds { get; set; }
			public abstract int Cost { get; }
			public abstract int Duration { get; }

			public abstract void Cast(Player caster, Player target);

			public void Reset()
			{
				IsActive = false;
				ActiveRounds = 0;
			}

			protected void Tick()
			{
				ActiveRounds += 1;

				if (ActiveRounds < Duration)
					return;

				IsActive = false;
				ActiveRounds = 0;
			}
		}

		public class MagicMissile : Spell
		{
			public override int Cost => 53;
			public override int Duration => 1;
			public override void Cast(Player caster, Player target)
			{
				Tick( );
				target.HitPoints -= 4;
			}
		}

		public class Drain : Spell
		{
			public override int Cost => 73;
			public override int Duration => 1;
			public override void Cast(Player caster, Player target)
			{
				Tick( );
				caster.HitPoints += 2;
				target.HitPoints -= 2;
			}
		}

		public class Shield : Spell
		{
			public override int Cost => 113;
			public override int Duration => 6;
			public override void Cast(Player caster, Player target)
			{
				Tick( );
				if (ActiveRounds == 1)
					caster.Armor += 7;
				if (ActiveRounds == 0)
					caster.Armor -= 7; // tick resets active rounds to 0 when Duration is reached
			}
		}

		public class Poison : Spell
		{
			public override int Cost => 173;
			public override int Duration => 6;
			public override void Cast(Player caster, Player target)
			{
				Tick( );
				target.HitPoints -= 3;
			}
		}

		public class Recharge : Spell
		{
			public override int Cost => 229;
			public override int Duration => 5;
			public override void Cast(Player caster, Player target)
			{
				Tick( );
				caster.Mana += 101;
			}
		}

		public class Player
		{
			public string Name { get; set; }
			public int HitPoints { get; set; }
			public int Damage { get; set; }
			public int Mana { get; set; }
			public int Armor { get; set; }
		}
	}
}