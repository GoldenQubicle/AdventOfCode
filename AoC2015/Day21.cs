namespace AoC2015
{
    public class Day21 : Solution
    {
        public Player Boss { get; }

        private List<Item> weapons = new()
        {
            new Item { Name = "Dagger", Cost = 8, Damage = 4, Armor = 0 },
            new Item { Name = "Shortsword", Cost = 10, Damage = 5, Armor = 0 },
            new Item { Name = "Warhammer", Cost = 25, Damage = 6, Armor = 0 },
            new Item { Name = "Longsword", Cost = 40, Damage = 7, Armor = 0 },
            new Item { Name = "Greataxe", Cost = 74, Damage = 8, Armor = 0 },
        };

        private List<Item> armor = new()
        {
            new Item { Name = "Leather", Cost = 13, Damage = 0, Armor = 1 },
            new Item { Name = "Chainmail", Cost = 31, Damage = 0, Armor = 2 },
            new Item { Name = "Splintmail", Cost = 53, Damage = 0, Armor = 3 },
            new Item { Name = "Bandedmail", Cost = 75, Damage = 0, Armor = 4 },
            new Item { Name = "Platemail", Cost = 102, Damage = 0, Armor = 5 },
        };

        private List<Item> ringsDamage = new()
        {
            new Item { Name = "Damage +1", Cost = 25, Damage = 1, Armor = 0 },
            new Item { Name = "Damage +2", Cost = 50, Damage = 2, Armor = 0 },
            new Item { Name = "Damage +3", Cost = 100, Damage = 3, Armor = 0 },
        };

        private List<Item> ringsDefense = new()
        {
            new Item { Name = "Defense +1", Cost = 20, Damage = 0, Armor = 1 },
            new Item { Name = "Defense +2", Cost = 40, Damage = 0, Armor = 2 },
            new Item { Name = "Defense +3", Cost = 80, Damage = 0, Armor = 3 },
        };

        public Day21(string file) : base(file, "\n")
        {
            var stats = Input.Select(line => line.Split(": ")).ToList();
            Boss = new Player
            {
                Name = "The Boss",
                HitPoints = int.Parse(stats[0][1]),
                Damage = int.Parse(stats[1][1]),
                Armor = int.Parse(stats[2][1]),
            };
        }

        public override async Task<string> SolvePart1( )
        {
            var combinations = GetAllItemCombinations();

            var rounds = combinations.Select(combo =>
                {
                    var player = new Player
                    {
                        Name = "Me",
                        HitPoints = 100,
                        Armor = combo.Armor,
                        Damage = combo.Damage
                    };
                    var winner = PlayGame(player);
                    return (winner: winner.Name, cost: combo.Cost);
                })
                .Where(r => r.winner.Equals("Me"))
                .OrderBy(r => r.cost)
                .First();
            return rounds.cost.ToString();
        }

        public override async Task<string> SolvePart2()
        {
            var combinations = GetAllItemCombinations();

            var rounds = combinations.Select(combo =>
                {
                    var player = new Player
                    {
                        Name = "Me",
                        HitPoints = 100,
                        Armor = combo.Armor,
                        Damage = combo.Damage
                    };
                    var winner = PlayGame(player);
                    return (winner: winner.Name, cost: combo.Cost);
                })
                .Where(r => r.winner.Equals("The Boss"))
                .OrderByDescending(r => r.cost)
                .First();
            return rounds.cost.ToString();
        }


        private List<Item> GetAllItemCombinations()
        {
            var weaponsArmor = weapons.SelectMany(w =>
                armor.Select(a => new Item
                {
                    Name = w.Name + a.Name,
                    Cost = w.Cost + a.Cost,
                    Damage = w.Damage + a.Damage,
                    Armor = w.Armor + a.Armor
                })).ToList();
            var weaponsRingDefense = weapons.SelectMany(w =>
                ringsDefense.Select(rd => new Item
                {
                    Name = w.Name + rd.Name,
                    Cost = w.Cost + rd.Cost,
                    Damage = w.Damage + rd.Damage,
                    Armor = w.Armor + rd.Armor,
                })).ToList();
            var weaponsRingDamage = weapons.SelectMany(w =>
                ringsDamage.Select(rd => new Item
                {
                    Name = w.Name + rd.Name,
                    Cost = w.Cost + rd.Cost,
                    Damage = w.Damage + rd.Damage,
                    Armor = w.Armor + rd.Armor,
                })).ToList();
            var weaponsArmorRingDefense = weaponsArmor.SelectMany(wa =>
                ringsDefense.Select(rd => new Item
                {
                    Name = wa.Name + rd.Name,
                    Cost = wa.Cost + rd.Cost,
                    Damage = wa.Damage + rd.Damage,
                    Armor = wa.Armor + rd.Armor,
                })).ToList();

            var weaponsArmorRingDamage = weaponsArmor.SelectMany(wa =>
                ringsDamage.Select(rd => new Item
                {
                    Name = wa.Name + rd.Name,
                    Cost = wa.Cost + rd.Cost,
                    Damage = wa.Damage + rd.Damage,
                    Armor = wa.Armor + rd.Armor,
                })).ToList();
            var weaponsBothRings = weapons.SelectMany(w =>
                ringsDefense.SelectMany(rdf => ringsDamage.Select(rda =>
                    new Item
                    {
                        Name = w.Name + rdf.Name + rda.Name,
                        Cost = w.Cost + rdf.Cost + rda.Cost,
                        Damage = w.Damage + rdf.Damage + rda.Damage,
                        Armor = w.Armor + rdf.Armor + rda.Armor
                    }))).ToList();

            var weaponsArmorBothRings = weaponsBothRings.SelectMany(w =>
                armor.Select(a => new Item
                {
                    Name = w.Name + a.Name,
                    Cost = w.Cost + a.Cost,
                    Damage = w.Damage + a.Damage,
                    Armor = w.Armor + a.Armor,
                })).ToList();

            var combinations = weapons
                .Concat(weaponsArmor)
                .Concat(weaponsRingDamage)
                .Concat(weaponsRingDefense)
                .Concat(weaponsBothRings)
                .Concat(weaponsArmorRingDamage)
                .Concat(weaponsArmorRingDefense)
                .Concat(weaponsArmorBothRings).ToList();
            return combinations;
        }


        public Player PlayGame(Player player)
        {
            var bossGoesDownIn = player.Attack(Boss);
            var playerGoesDownIn = Boss.Attack(player);
            return bossGoesDownIn > playerGoesDownIn ? Boss : player;
        }

        public record Item
        {
            public string Name { get; init; }
            public int Cost { get; init; }
            public int Damage { get; init; }
            public int Armor { get; init; }
        }

        public class Player
        {
            public string Name { get; set; }
            public int HitPoints { get; set; }
            public int Damage { get; set; }
            public int Armor { get; set; }
            private double AttackStrength { get; set; }

            public int Attack(Player other)
            {
                AttackStrength = Damage > other.Armor ? Damage - other.Armor : 1;
                return (int) Math.Ceiling(other.HitPoints / AttackStrength);
            }
        }
    }

    
}