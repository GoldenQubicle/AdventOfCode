namespace AoC2015
{
    public class Day05 : Solution
    {
        public Day05(string file) : base(file, "\n") { }
        
        public Day05(List<string> input) : base(input) { }

        public override async Task<string> SolvePart1( ) => Input.Select(IsValidStringPart1).Count(b => b ).ToString();

        public override async Task<string> SolvePart2( ) => Input.Select(IsValidStringPart2).Count(b => b).ToString( );
        public bool IsValidStringPart1(string input)
        {
            var vowels = new Regex("[aeiou]");
            var twice = new Regex("(\\w)\\1");
            var groups = new Regex("(ab)|(cd)|(pq)|(xy)");

            return vowels.Matches(input).Count >= 3 &&
                   twice.Matches(input).Count >= 1 &&
                   groups.Matches(input).Count == 0;
        }

        public bool IsValidStringPart2(string input)
        {
            var twice = new Regex("(\\w{2}).*\\1");
            var repeat = new Regex("(?<=(\\w)).(?=\\1)");

            return  twice.Matches(input).Count >= 1 &&
                    repeat.Matches(input).Count >= 1;
        }
    }
}