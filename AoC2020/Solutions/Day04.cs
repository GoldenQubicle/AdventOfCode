using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.Solutions
{
    public class Day04 : Solution<int>
    {
        private readonly Dictionary<string, Func<string, bool>> fieldValidation = new( )
        {
            { "byr", CheckBirthYear },
            { "iyr", CheckIssueYear },
            { "eyr", CheckExperirationYear },
            { "hgt", CheckHeight },
            { "hcl", CheckHairColor },
            { "ecl", CheckEyeColor },
            { "pid", CheckPassportId },
            { "cid", s => true },
        };

        private List<Dictionary<string, string>> passports;

        public Day04(string file) : base(file, "\r\n\r\n")
        {
            passports = Input
                .Select(i => i.Split("\r\n"))
                .Select(i => i.Select(i => i.Split(" ")))
                .Select(i =>
                {
                    var dict = new Dictionary<string, string>( );
                    var kvp = i.SelectMany(f => f.Select(e => e.Split(" ")[0])).ToList( );
                    kvp.ForEach(e => dict.Add(e.Split(":")[0], e.Split(":")[1]));
                    return dict;
                }).ToList( );
        }

        public override int SolvePart1( ) =>
          passports.Count(d => d.Keys.Count == 8 || ( d.Keys.Count == 7 && !d.ContainsKey("cid") ));

        public override int SolvePart2( ) =>
            passports.Where(d => d.Keys.Count == 8 || ( d.Keys.Count == 7 && !d.ContainsKey("cid") ))
                     .Count(p => p.All(kvp => fieldValidation[kvp.Key].Invoke(kvp.Value)));

        public static bool CheckBirthYear(string arg) => CheckRange(int.Parse(arg), 1920, 2002);

        public static bool CheckIssueYear(string arg) => CheckRange(int.Parse(arg), 2010, 2020);

        public static bool CheckExperirationYear(string arg) => CheckRange(int.Parse(arg), 2020, 2030);

        public static bool CheckHeight(string arg) =>
            new string(arg.Where(char.IsLetter).ToArray( )) switch
            {
                "cm" => CheckRange(int.Parse(arg.Where(char.IsDigit).ToArray( )), 150, 193),
                "in" => CheckRange(int.Parse(arg.Where(char.IsDigit).ToArray( )), 59, 76),
                _ => false
            };

        public static bool CheckHairColor(string arg) => !arg.StartsWith('#') ? false :
            !new Regex(@"[g-z]").Match(new string(arg.Where(char.IsLetter).ToArray( ))).Success;

        public static bool CheckEyeColor(string arg) =>
            new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(arg);

        public static bool CheckPassportId(string arg) => arg.Any(char.IsLetter) ? false : arg.Length == 9;

        private static bool CheckRange(int i, int min, int max) => i >= min && i <= max;

    }
}
