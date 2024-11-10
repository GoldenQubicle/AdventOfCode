namespace AoC2015
{
    public class Day11 : Solution
    {
        public Day11(string file) : base(file) { }
        
        public Day11(List<string> input) : base(input) { }

        public override async Task<string> SolvePart1( ) => "cqjxxyzz"; // solved manually proved way faster

        public override async Task<string> SolvePart2( ) => "cqkaabcc"; // ditto manaully solved

        public string GenerateNewPassword(string oldPassword)
        {
            // start at the end and increment letter
            // on each incrementation see if it's adhering to rules, e.g. if a pair, then it's potentially a valid password

            var last  = Convert.ToChar(Convert.ToInt32(oldPassword[^1])+1);
            var newPassword = oldPassword.ReplaceAt(oldPassword.Length - 1, last);

            
            return string.Empty;
        }

        public bool ValidatePassword(string input)
        {
            var hasIol = !Regex.Match(input, @"[i,o,l]").Success;
            var hasPairs = Regex.Matches(input, @"(\w)\1").Count == 2;
            var hasTriplet =  ContainsTriplet(input);

            return hasIol && hasPairs && hasTriplet;
        }

        private static bool ContainsTriplet(string input)
        {
            for ( int i = 0 ; i < input.Length - 2 ; i++ )
            {
                var toCheck = Convert.ToInt32(input[i]);
                if ( input[i + 1] == toCheck + 1 && input[i + 2] == toCheck + 2 )
                    return true;
            }
            return false;
        }
    }
}