using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day07 : Solution<int>
    {
        private readonly Dictionary<string, List<(string bag, int no)>> bagRules = new( );
        public Day07(string file) : base(file, ".")
        {
            Input.Select(i => i.Split("contain")).ToList( )
                .ForEach(i =>
                {
                    var contains = i[1].Split(",").ToList( );
                    var current = i[0].Replace(" ", "");
                    current = current.Remove(current.Length - 1);
                    bagRules.Add(current, new List<(string, int)>( ));
                    contains.ForEach(b =>
                    {
                        var bag = new string(b.Where(char.IsLetter).ToArray( ));
                        var no = int.Parse(b.Where(char.IsDigit).ToArray( ));
                        if ( bag.Last( ) == 's' )
                            bag = bag.Remove(bag.Length - 1);
                        bagRules[current].Add((bag, no));
                    });
                });
            bagRules.Add("nootherbag",new List<(string bag, int no)>());
        }
        public override int SolvePart1( ) => TraceBag("shinygoldbag");

        public override int SolvePart2( ) => CountBags("shinygoldbag").Sum( );     

        public List<int> CountBags(string bag)
        {
            var queue = new Queue<(string bag, int prevCount)>( );
            var count = new List<int>( );
            
            queue.Enqueue((bag: bag, prevCount: 1));

            while(queue.Count > 0 )
            {               
                var current = queue.Dequeue( );
                count.Add(bagRules[current.bag].Sum(b => b.no) * current.prevCount);

                var next = bagRules[current.bag];
                
                foreach(var b in next )
                {
                    queue.Enqueue((b.bag, b.no * current.prevCount));
                }               
            }

            return count;
        }

        public int TraceBag(string bag)
        {
            var endPoints = new List<string>( );
            var queue = new Queue<KeyValuePair<string, List<(string, int)>>>( );
            queue.AddAll(bagRules.Where(b => b.Value.Contains(bag)));

            while ( queue.Count > 0 )
            {
                var current = queue.Dequeue( ).Key;
                endPoints.Add(current);

                if ( bagRules.Any(b => b.Value.Contains(current)) )
                    queue.AddAll(bagRules.Where(b => b.Value.Contains(current)));
            }
            return endPoints.Distinct( ).Count( );
        }

    }

    public static class Extensions
    {
        public static bool Contains(this List<(string, int)> bags, string bag) => bags.Any(b => b.Item1.Equals(bag));

        public static void AddAll(this Queue<KeyValuePair<string, List<(string, int)>>> queue
            , IEnumerable<KeyValuePair<string, List<(string, int)>>> newEntries) => newEntries.ToList( ).ForEach(e => queue.Enqueue(e));
    }
}
