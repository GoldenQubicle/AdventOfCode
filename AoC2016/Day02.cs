namespace AoC2016
{
    public class Day02 : Solution
    {
        public Day02(string file) : base(file, "\n") { }
        
        public override async Task<string> SolvePart1()
        {
            var code = string.Empty;
            var button = 5;

            Input.ForEach(line =>
            {
                line.ForEach(c => button = c switch
                {
                    'U' when button >= 4 => button - 3,
                    'D' when button <= 6 => button + 3,
                    'L' when button != 1 && button != 4 && button != 7 => button - 1,
                    'R' when button != 3 && button != 6 && button != 9 => button + 1,
                    _ => button
                });
                code += button.ToString();
            });

            return code;
        }

        public override async Task<string> SolvePart2()
        {
            var keypad = new Dictionary<char, Dictionary<char, char>>
            {
                { '5', new Dictionary<char, char> {{'R', '6'}} },
                { '2', new Dictionary<char, char> {{'R', '3'}, {'D', '6'}} },
                { '6', new Dictionary<char, char> {{'R', '7'}, {'D', 'A'}, {'U', '2'}, {'L', '5'}} },
                { 'A', new Dictionary<char, char> {{'R', 'B'}, {'U', '6'}} },
                { '1', new Dictionary<char, char> {{'D', '3'}} },
                { '3', new Dictionary<char, char> {{'R', '4'}, {'D', '7'}, {'L', '2'}, {'U', '1'}} },
                { '7', new Dictionary<char, char> {{'R', '8'}, {'D', 'B'}, {'L', '6'}, {'U', '3'}} },
                { 'B', new Dictionary<char, char> {{'R', 'C'}, {'D', 'D'}, {'L', 'A'}, {'U', '7'}} },
                { 'D', new Dictionary<char, char> {{'U', 'B'}} },
                { '4', new Dictionary<char, char> {{'L', '3'}, {'D', '8'}} },
                { '8', new Dictionary<char, char> {{'R', '9'}, {'D', 'C'}, {'L', '7'}, {'U', '4'}} },
                { 'C', new Dictionary<char, char> {{'L', 'B'}, {'U', '8'}} },
                { '9', new Dictionary<char, char> {{'L', '8'}} },
            };
            var code = string.Empty;
            var button = '5';

            Input.ForEach(line =>
            {
                line.ForEach(c =>
                {
                    button = keypad[button].ContainsKey(c) ? keypad[button][c] : button;
                });
                code += button.ToString();
            });

            return code;
        }
    }
}