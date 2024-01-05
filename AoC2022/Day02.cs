namespace AoC2022
{
    public class Day02 : Solution
    {
        public Day02(string file) : base(file) { }
        
        public Day02(List<string> input) : base(input) { }

        public override async Task<string> SolvePart1() => Input
            .Select(round => round switch
            {
                "A X" => 1 + 3,
                "A Y" => 2 + 6,
                "A Z" => 3 + 0,
                "B X" => 1 + 0,
                "B Y" => 2 + 3,
                "B Z" => 3 + 6,
                "C X" => 1 + 6,
                "C Y" => 2 + 0,
                "C Z" => 3 + 3,
            }).Sum().ToString();



        public override async Task<string> SolvePart2() => Input
            .Select(round => round switch
            {
                "A X" => 0 + 3,
                "A Y" => 3 + 1,
                "A Z" => 6 + 2,
                "B X" => 0 + 1,
                "B Y" => 3 + 2,
                "B Z" => 6 + 3,
                "C X" => 0 + 2,
                "C Y" => 3 + 3,
                "C Z" => 6 + 1,
            }).Sum().ToString();
    }
}