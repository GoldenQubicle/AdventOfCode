using Common;

namespace AoC2018
{
    public class Day01 : Solution
    {
        public Day01(string file) : base(file) { }
        
        public Day01(List<string> input) : base(input) { }

        public override async Task<string> SolvePart1( ) => Input.Select(int.Parse).Aggregate(0, (i, i1) => i + i1).ToString();

        public override async Task<string> SolvePart2()
        {
	        var seen = new HashSet<int>();
	        var freq = 0;
	        seen.Add(freq);

			while (true)
	        {
				foreach (var i in Input.Select(int.Parse))
		        {
			        freq += i;

					if (seen.Contains(freq))
					{
						return freq.ToString();
					}
					seen.Add(freq);
				}
	        }
		}
    }
}