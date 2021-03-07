using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;
using NUnit.Framework;

namespace AoC2015
{
    public class Day22 : Solution
    {
        private readonly Dictionary<string, Spell> spells = new()
        {
            { nameof(MagicMissile), new MagicMissile() },
            { nameof(Drain), new Drain() },
            { nameof(Shield), new Shield() },
            { nameof(Poison), new Poison() },
            { nameof(Recharge), new Recharge() },
        };
        public Player TheBoss { get; set; }
        public Player ThePlayer { get; set; }
        public Day22(string file) : base(file) { }

        public Day22(List<string> input) : base(input) { }

        public override string SolvePart1( )
        {


            var combos = new Queue<(List<string> scenario, List<Spell> spells)>();

            combos.Enqueue((new List<string>(), new List<Spell> { new MagicMissile(), new Drain(), new Shield(), new Poison(), new Recharge() }));
            var turn = 0;

            while(turn < 10)
            {
                var current = combos.Dequeue();

                turn = current.scenario.Count > turn ? current.scenario.Count : turn;

                var possibleSpells = current.spells.Where(spell => !spell.IsActive);

                foreach(var spell in possibleSpells)
                {
                    spell.IsActive = true;
                    var scenario = current.scenario.Expand(spell.GetType().Name);
                    var spellState = current.spells.Select(s => s.Copy()).ToList();
                    spell.IsActive = false;

                    spellState.Where(s => s.IsActive).ForEach(s =>
                    {
                        s.Tick();
                        s.Tick();
                    });

                    combos.Enqueue((scenario, spellState));
                }
            }

            var games = combos.Select(c => PlayGame(c.scenario)).ToList();
                //.Where(g => g.winner.Equals("me"))
                //.OrderBy(g => g.manaSpend).ToList();

            return PlayGame(new List<string>()).winner;
        }

        public (string winner, int manaSpend) PlayGame(IReadOnlyList<string> scenario)
        {
            var playerTurn = 0;
            var manaSpend = 0;
            spells.Values.ForEach(s => { s.IsActive = false; s.ActiveRounds = 0; });
            ThePlayer = new Player { Name = nameof(ThePlayer), HitPoints = 50, Mana = 500 };
            TheBoss = new Player { Name = nameof(TheBoss), HitPoints = 51, Damage = 9 };
            
            while(true)
            {
                //stupid guard due to stupid combination generation process
                if(playerTurn == 10) return ("", 0);

                //player turn
                var activeSpells = spells.Values.Where(spell => spell.IsActive).ToList();
                activeSpells.ForEach(spell => spell.Cast(ThePlayer, TheBoss));
                
                var pickedSpell = spells[scenario[playerTurn]];

                //stupid guard due to stupid combination generation process
                if(pickedSpell.IsActive) return ("", 0);
                
                Assert.IsFalse(pickedSpell.IsActive);
                Assert.AreEqual(0, pickedSpell.ActiveRounds);

                pickedSpell.IsActive = true;
                ThePlayer.Mana -= pickedSpell.Cost;
                manaSpend += pickedSpell.Cost;
                playerTurn++;

                //boss turn
                activeSpells = spells.Values.Where(spell => spell.IsActive).ToList();
                activeSpells.ForEach(spell => spell.Cast(ThePlayer, TheBoss));

                ThePlayer.HitPoints -= TheBoss.Damage - ThePlayer.Armor <= 0 ? 1 : TheBoss.Damage - ThePlayer.Armor;

                if(TheBoss.HitPoints <= 0)
                    return (ThePlayer.Name, manaSpend);

                if(ThePlayer.HitPoints <= 0 || ThePlayer.Mana < 53)
                    return (TheBoss.Name, manaSpend);

            }
        }

        public override string SolvePart2( ) => null;

        public abstract class Spell
        {
            public bool IsActive { get; set; }
            public abstract int Cost { get; }
            public abstract int Duration { get; }
            public int ActiveRounds;
            public abstract void Cast(Player caster, Player target);

            public void Tick( )
            {
                ActiveRounds += 1;

                if(ActiveRounds < Duration) return;

                IsActive = false;
                ActiveRounds = 0;
            }

            public abstract Spell Copy( );
        }

        public class MagicMissile : Spell
        {
            public override int Cost => 53;
            public override int Duration => 1;
            public override void Cast(Player caster, Player target)
            {
                Tick();
                target.HitPoints -= 4;
            }

            public override Spell Copy( ) => new MagicMissile { IsActive = IsActive, ActiveRounds = ActiveRounds };

        }

        public class Drain : Spell
        {
            public override int Cost => 73;
            public override int Duration => 1;
            public override void Cast(Player caster, Player target)
            {
                Tick();
                caster.HitPoints += 2;
                target.HitPoints -= 2;
            }
            public override Spell Copy( ) => new Drain { IsActive = IsActive, ActiveRounds = ActiveRounds };

        }

        public class Shield : Spell
        {
            public override int Cost => 113;
            public override int Duration => 6;
            public override void Cast(Player caster, Player target)
            {
                Tick();
                if(ActiveRounds == 1) caster.Armor += 7;
                if(ActiveRounds == 0) caster.Armor -= 7; // tick resets active rounds to 0 when Duration is reached
            }
            public override Spell Copy( ) => new Shield { IsActive = IsActive, ActiveRounds = ActiveRounds };

        }

        public class Poison : Spell
        {
            public override int Cost => 173;
            public override int Duration => 6;
            public override void Cast(Player caster, Player target)
            {
                Tick();
                target.HitPoints -= 3;
            }

            public override Spell Copy( ) => new Poison { IsActive = IsActive, ActiveRounds = ActiveRounds };

        }

        public class Recharge : Spell
        {
            public override int Cost => 229;
            public override int Duration => 5;
            public override void Cast(Player caster, Player target)
            {
                Tick();
                caster.Mana += 101;
            }

            public override Spell Copy( ) => new Recharge { IsActive = IsActive, ActiveRounds = ActiveRounds };

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