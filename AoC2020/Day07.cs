namespace AoC2020
{
    public class Day07 : Solution
    {
        private readonly Dictionary<string, List<(string bag, int count)>> bagRules = new( );
        public Day07(string file) : base(file, ".")
        {
            Input.Select(i => i.Split("contain")).ToList( )                
                .ForEach(rule =>
                {                    
                    var currentBag = rule[0].Trim( );
                    bagRules.Add(currentBag, new List<(string, int)>( ));

                    rule[1].Split(",").ToList( ).ForEach(content =>
                    {
                        var bag = content.Trim( ).Substring(1, content.Trim( ).Length - 1).Trim( );
                        var count = int.Parse(content.Trim( ).Substring(0, 1));
                        if ( count == 1 ) bag = bag + "s"; // add s to singular bags
                        bagRules[currentBag].Add((bag, count));
                    });
                });
        }
        public override async Task<string> SolvePart1( )
        {
            var endPoints = new List<string>( );
            var queue = new Queue<KeyValuePair<string, List<(string, int)>>>( );
            queue.AddAll(bagRules.Where(b => b.Value.Contains("shiny gold bags")));

            while ( queue.Count > 0 )
            {
                var current = queue.Dequeue( ).Key;
                endPoints.Add(current);

                if ( bagRules.Any(b => b.Value.Contains(current)) )
                    queue.AddAll(bagRules.Where(b => b.Value.Contains(current)));
            }
            return endPoints.Distinct( ).Count( ).ToString( );
        }

        public override async Task<string> SolvePart2( )
        {
            var queue = new Queue<(string bag, int count)>( );
            var count = new List<int>( );
            queue.Enqueue(("shiny gold bags", 1));           

            while ( queue.Count > 0 )
            {
                var current = queue.Dequeue( );
                if ( bagRules.ContainsKey(current.bag) )
                {
                    count.Add(bagRules[current.bag].Sum(bag => bag.count) * current.count);
                    bagRules[current.bag].ForEach(rule => 
                        queue.Enqueue((rule.bag, rule.count * current.count)));
                }
            }
            return count.Sum( ).ToString( );
        }
    }

    public static class Extensions
    {
        public static bool Contains(this List<(string, int)> bags, string bag) => bags.Any(b => b.Item1.Equals(bag));

        public static void AddAll(this Queue<KeyValuePair<string, List<(string, int)>>> queue
            , IEnumerable<KeyValuePair<string, List<(string, int)>>> newEntries) => newEntries.ToList( ).ForEach(e => queue.Enqueue(e));
    }
}
