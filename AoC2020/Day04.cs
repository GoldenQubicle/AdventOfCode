using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace AoC2020
{
    public class Day04 : Solution<int>
    {
        private readonly List<Dictionary<string, string>> passports;

        public Day04(string file) : base(file, "\r\n\r\n") => passports = Input
                .Select(i => i.Split("\r\n"))
                .Select(i => i.Select(i => i.Split(" ")))
                .Select(i => i.SelectMany(f => f.Select(e => e.Split(" ")[0]))
                .ToDictionary(kvp => kvp.Split(":")[0], kvp => kvp.Split(":")[1])).ToList( );

        public override int SolvePart1( ) => passports.Count(IsValidPassport);

        public override int SolvePart2( ) => passports.Where(IsValidPassport).Count(p => p.All(IsValidField));

        private bool IsValidPassport(Dictionary<string, string> p) =>
            p.Keys.Count == 8 || ( p.Keys.Count == 7 && !p.ContainsKey("cid") );

        private bool IsValidField(KeyValuePair<string, string> kvp) => kvp.Key switch
        {
            "byr" => CheckBirthYear(kvp.Value),
            "iyr" => CheckIssueYear(kvp.Value),
            "eyr" => CheckExperirationYear(kvp.Value),
            "hgt" => CheckHeight(kvp.Value),
            "hcl" => CheckHairColor(kvp.Value),
            "ecl" => CheckEyeColor(kvp.Value),
            "pid" => CheckPassportId(kvp.Value),
            "cid" => true,
        };

        public bool CheckBirthYear(string arg) => CheckRange(int.Parse(arg), 1920, 2002);

        public bool CheckIssueYear(string arg) => CheckRange(int.Parse(arg), 2010, 2020);

        public bool CheckExperirationYear(string arg) => CheckRange(int.Parse(arg), 2020, 2030);

        public bool CheckHeight(string arg) =>
            new string(arg.Where(char.IsLetter).ToArray( )) switch
            {
                "cm" => CheckRange(int.Parse(arg.Where(char.IsDigit).ToArray( )), 150, 193),
                "in" => CheckRange(int.Parse(arg.Where(char.IsDigit).ToArray( )), 59, 76),
                _ => false
            };

        public bool CheckHairColor(string arg) => !arg.StartsWith('#') ? false :
            !new Regex(@"[g-z]").Match(new string(arg.Where(char.IsLetter).ToArray( ))).Success;

        public bool CheckEyeColor(string arg) =>
            new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(arg);

        public bool CheckPassportId(string arg) => arg.Any(char.IsLetter) ? false : arg.Length == 9;

        private bool CheckRange(int i, int min, int max) => i >= min && i <= max;

    }
}
