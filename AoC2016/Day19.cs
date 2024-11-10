namespace AoC2016
{
    public class Day19 : Solution
    {
        private List<int> elves;
        public Day19(string file) : base(file, "\n") => elves = Enumerable.Range(1, int.Parse(Input[0])).ToList();
        public Day19(List<string> input) : base(input) => elves = Enumerable.Range(1, int.Parse(Input[0])).ToList();

        public override async Task<string> SolvePart1()
        {
            while (elves.Count > 1)
            {
                var remove = new List<int>();
                for (var i = 0; i < elves.Count; i += 2)
                {
                    remove.Add(i + 1 >= elves.Count ? elves[0] : elves[i + 1]);
                }
                elves = elves.Except(remove).ToList();
            }
            return elves.First().ToString();
        }

        public override async Task<string> SolvePart2()
        {
            //tbh not quite sure why this works but it does.. 
            //spend way too long with while loop, linked list and then back to
            //getting the for loop working so I am just going to call it a day here
            var current = 1;
            
            for(var i = 1 ; i < elves.Count ; i++)
            {
                current = current % i + 1;
                
                if(current > (i + 1) / 2)
                {
                    current++;
                }
            }
            return current.ToString();

            //var ll = new LinkedList<int>();
            //var current = new LinkedListNode<int>(elves.First());
            //ll.AddFirst(current);

            //for (var i = 1; i < elves.Count-1; i++)
            //{
            //    var next = new LinkedListNode<int>(elves[i]);
            //    ll.AddAfter(current, next);
            //    current = next;
            //}
            //ll.AddLast(new LinkedListNode<int>(elves.Last()));

            //var currentValue = 1;

            //while (ll.Count > 1)
            //{
            //    if (currentValue > ll.Count)
            //        currentValue = ll.First();

            //    var node = ll.Find(currentValue);

            //    while (node == null)
            //    {
            //        currentValue++;
            //        node = ll.Find(currentValue);
            //    }

            //    var steps = ll.Count / 2;
            //    for (var s = 0; s < steps; s++)
            //    {
            //        node = node.Next ?? ll.Find(ll.First());
            //    }

            //    ll.Remove(node);
            //    currentValue++;
            //}
            //return ll.First().ToString();
        }
    }
}