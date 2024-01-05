using Common;
using Common.Extensions;

namespace AoC2018
{
    public class Day05 : Solution
    {
        public Day05(string file) : base(file) { }
        
        public Day05(List<string> input) : base(input) { }

        
        private const int Match = 32;
        
        private bool DoesReact(char one, char two) => Math.Abs(one - two) == Match;

        
        public override async Task<string> SolvePart1() => FullYReactPolymer(Input[0]).ToString();

        
        public override async Task<string> SolvePart2() => Enumerable.Range(0, 26)
	        .Select(n =>
			{
			    var current = 65 + n;
			    var polymer = Input[0].Where(c => c != current).Where(c => c != current + Match).AsString( );
			    return FullYReactPolymer(polymer);
			}).Min( ).ToString( );


		private int FullYReactPolymer(string polymer)
        {
	        var destroyed = new HashSet<int>();
	        var i1 = 0;
	        var i2 = 1;

	        while (i2 < polymer.Length)
	        {
		        if (DoesReact(polymer[i1], polymer[i2]))
		        {
			        destroyed.Add(i1);
			        destroyed.Add(i2);

			        while (destroyed.Contains(i1) && i1 - 1 >= 0)
				        i1--;

			        i2++;
			        continue;
		        }
				
		        if (i1 + 1 != i2)
		        {
			        i1 = i2;
			        i2++;
		        }
		        else
		        {
			        i1++;
			        i2++;
		        }
	        }
	        return polymer.Length - destroyed.Count;
        }
    }
}