namespace AoC2015
{
    public class Day16 : Solution
    {
        private List<Dictionary<string, int>> aunts;
        private Dictionary<string, int> compounds = new Dictionary<string, int>
            {
                { "children", 3},
                { "cats", 7},
                { "samoyeds", 2},
                { "pomeranians", 3},
                { "akitas", 0},
                { "vizslas", 0},
                { "goldfish", 5},
                { "trees", 3},
                { "cars", 2},
                { "perfumes", 1},
            };

        public Day16(string file) : base(file, "\n")
        {
            aunts = Input.Select(line =>
             {
                 var matches = Regex.Matches(line,
                     @"([A-Z]\w+ \d{1,})|(children: \d+)|(cats: \d+)|(samoyeds: \d+)|(pomeranians: \d+)|(akitas: \d+)|(vizslas: \d+)|(goldfish: \d+)|(trees: \d+)|(cars: \d+)|(perfumes: \d+)");
                 var aunt = new Dictionary<string, int>( );
                 foreach ( Match match in matches )
                 {
                     if ( string.IsNullOrEmpty(match.Value) ) continue;

                     var parts = match.Value.Contains(":") ? match.Value.Split(":") : match.Value.Split(" ");
                     aunt.Add(parts[0], int.Parse(parts[1]));
                 }
                 return aunt;
             }).ToList( );
        }

        public override async Task<string> SolvePart1( )
        {
            var potential = compounds.ToDictionary(c => c.Key,
                c => aunts.Where(a => a.ContainsKey(c.Key) && a[c.Key] == c.Value).Select(a => a["Sue"]).ToList( ));

            return aunts.Where(a => a.Keys.Skip(1).All(k => potential[k].Contains(a["Sue"]))).First( )["Sue"].ToString( );
        }

        public override async Task<string> SolvePart2( )
        {
            var potential = compounds.ToDictionary(c => c.Key,
                c => aunts.Where(a => a.ContainsKey(c.Key))
                .Where(a => ( c.Key.Equals("cats") || c.Key.Equals("trees") ) ? a[c.Key] >= c.Value :
                            ( c.Key.Equals("pomeranians") || c.Key.Equals("goldfish") ) ? a[c.Key] < c.Value : a[c.Key] == c.Value)
                .Select(a => a["Sue"]).ToList( ));

            return aunts.Where(a => a.Keys.Skip(1).All(k => potential[k].Contains(a["Sue"]))).First( )["Sue"].ToString( );
        }
    }
}