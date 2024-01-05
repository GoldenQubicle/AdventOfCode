using System.Linq;
using System.Threading.Tasks;
using Common;

namespace AoC2016
{
    public class Day06 : Solution
    {
        public Day06(string file) : base(file, "\n") { }

        public override async Task<string> SolvePart1()
        {
            var mssg = string.Empty;
            
            for (var s = 0; s < Input[0].Length; s++)
            {
                var key = Input.Select(i => i[s])
                    .GroupBy(c => c)
                    .OrderBy(g => g.Count())
                    .Last().Key;
                mssg += key.ToString();
            }

            return mssg;
        }

        public override async Task<string> SolvePart2()
        {
            var mssg = string.Empty;

            for(var s = 0 ; s < Input[0].Length ; s++)
            {
                var key = Input.Select(i => i[s])
                    .GroupBy(c => c)
                    .OrderBy(g => g.Count())
                    .First().Key;
                mssg += key.ToString();
            }

            return mssg;
        }
    }
}