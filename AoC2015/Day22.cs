using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

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
        public List<string> Scenario { get; set; }
        public Day22(string file) : base(file)
        {
            
        }
        
        public Day22(List<string> input) : base(input) { }

        public override string SolvePart1()
        {
            var playerTurn = 0;

            while(true)
            {
                //player turn
                var activeSpells = spells.Values.Where(spell => spell.IsActive).ToList();
                activeSpells.ForEach(spell => spell.Cast(ThePlayer, TheBoss));

                var pickedSpell = Scenario.Any() ? spells[Scenario[playerTurn]] : PickRandomSpell(ThePlayer);
                pickedSpell.IsActive = true;
                ThePlayer.Mana -= pickedSpell.Cost;
                playerTurn++;

                //boss turn
                activeSpells = spells.Values.Where(spell => spell.IsActive).ToList();
                activeSpells.ForEach(spell => spell.Cast(ThePlayer, TheBoss));

                ThePlayer.HitPoints -= TheBoss.Damage - ThePlayer.Armor <= 0 ? 1 : TheBoss.Damage - ThePlayer.Armor;

                if(TheBoss.HitPoints <= 0) return ThePlayer.Name;

                if(ThePlayer.HitPoints <= 0 || ThePlayer.Mana < 53) return TheBoss.Name;

            }
        }

        private Spell PickRandomSpell(Player player) => 
            spells.Values.Where(spell => !spell.IsActive && spell.Cost <= player.Mana).ToList().Random();

        public override string SolvePart2( ) => null;

        public abstract class Spell
        {
            public bool IsActive { get; set; }
            public abstract int Cost { get;  }
            public abstract int Duration { get; }
            protected int ActiveRounds;
            public abstract void Cast(Player caster, Player target);

            protected void Tick()
            {
                ActiveRounds += 1;

                if (ActiveRounds < Duration) return;

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
                Tick();
                target.HitPoints -= 4;
            }
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
        }

        public class Shield : Spell
        {
            public override int Cost => 113;
            public override int Duration => 6;
            public override void Cast(Player caster, Player target)
            {
                Tick();
                if (ActiveRounds == 1) caster.Armor += 7;
                if (ActiveRounds == 0) caster.Armor -= 7; // tick resets active rounds to 0 when Duration is reached
            }
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