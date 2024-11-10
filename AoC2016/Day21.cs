namespace AoC2016
{
    public class Day21 : Solution
    {
        public string PassWord { get; set; } = "abcdefgh";
        public string Scrambled { get; set; } = "fbgdceah";

        public Day21(string file) : base(file, "\n")
        {
            
        }

        public override async Task<string> SolvePart1()
        {
            foreach(var line in Input)
            {
                var parts = line.Split(' ');
                switch((parts[0], parts[1]))
                {
                    case ("swap", "position"):
                        PassWord = SwapPosition(PassWord, int.Parse(parts[2]), int.Parse(parts.Last()));
                        break;
                    case ("swap", "letter"):
                        PassWord = SwapLetter(PassWord, parts[2], parts.Last());
                        break;
                    case ("rotate", "left"):
                        PassWord = Rotate(PassWord, int.Parse(parts[2]), false);
                        break;
                    case ("rotate", "right"):
                        PassWord = Rotate(PassWord, int.Parse(parts[2]));
                        break;
                    case ("rotate", "based"):
                        PassWord = Rotate(PassWord, parts.Last());
                        break;
                    case ("reverse", "positions"):
                        PassWord = Reverse(PassWord, int.Parse(parts[2]), int.Parse(parts.Last()));
                        break;
                    case ("move", "position"):
                        PassWord = Move(PassWord, int.Parse(parts[2]), int.Parse(parts.Last()));
                        break;
                }
            }
            return PassWord;
        }

        public override async Task<string> SolvePart2()
        {
            Input.Reverse();
            
            //manually figured out mapping for reverse rotation based on letter index
            var mapping = new Dictionary<int, Func<string, string>>
            {
                {0, input => Rotate(input, 1, false) },
                {1, input => Rotate(input, 1, false) },
                {2, input => Rotate(input, 2) },
                {3, input => Rotate(input, 2, false) },
                {4, input => Rotate(input, 1) },
                {5, input => Rotate(input, 3, false) },
                {6, input => input },
                {7, input => Rotate(input, 4, false) },
            };
            

            foreach(var line in Input)
            {
                var parts = line.Split(' ');
                switch((parts[0], parts[1]))
                {
                    case ("swap", "position"):
                        Scrambled = SwapPosition(Scrambled, int.Parse(parts.Last()), int.Parse(parts[2]));
                        break;
                    case ("swap", "letter"):
                        Scrambled = SwapLetter(Scrambled, parts.Last(), parts[2]);
                        break;
                    case ("rotate", "left"):
                        Scrambled = Rotate(Scrambled, int.Parse(parts[2]));
                        break;
                    case ("rotate", "right"):
                        Scrambled = Rotate(Scrambled, int.Parse(parts[2]), false);
                        break;
                    case ("rotate", "based"):
                        Scrambled = mapping[Scrambled.IndexOf(parts.Last())](Scrambled);
                        break;
                    case ("reverse", "positions"):
                        Scrambled = Reverse(Scrambled, int.Parse(parts[2]), int.Parse(parts.Last()));
                        break;
                    case ("move", "position"):
                        Scrambled = Move(Scrambled, int.Parse(parts.Last()), int.Parse(parts[2]));
                        break;
                }
            }
            return Scrambled;
        }

        public string SwapPosition(string input, int d1, int d2)
        {
            var swap = input[d1];
            var result = input.ReplaceAt(d1, input[d2]);
            result = result.ReplaceAt(d2, swap);
            return result;
        }

        public string SwapLetter(string input, string c1, string c2)
        {
            var d1 = input.IndexOf(c1);
            var d2 = input.IndexOf(c2);
            return SwapPosition(input, d1, d2);
        }

        public string Rotate(string input, int steps, bool isRight = true)
        {
            var start = isRight ? input[^steps..] : input[^(input.Length-steps)..];
            var end = new string(input.Except(start).ToArray());
            return start + end;
        }

        public string Rotate(string input, string letter)
        {
            var idx = input.IndexOf(letter);
            var steps = idx >= 4 ? idx + 2 : idx + 1;

            if (steps > input.Length)
                steps %= input.Length;
            
            return Rotate(input, steps);
        }

        public string Reverse(string input, int d1, int d2)
        {
            var reversed = new string(input[d1..(d2 + 1)].Reverse().ToArray());
            var start = input[..d1];
            var end = input[(d2+1)..];
            return start + reversed + end;
        }

        public string Move(string input, int d1, int d2)
        {
            var move = input[d1];
            input = input.Remove(d1, 1);
            input = input.Insert(d2, move.ToString());
            return input;
        }

 
    }
}