using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.Solutions
{
    public class Day04 : Solution<int>
    {
        private Dictionary<string, Func<string, bool>> requiredFields = new Dictionary<string, Func<string, bool>>
        {   {"byr", checkBirthYear },
            {"iyr", checkIssueYear },
            {"eyr", checkExperirationYear },
            {"hgt", checkHeight },
            {"hcl",checkHairColor},
            {"ecl",checkEyeColor},
            {"pid",checkPassportId},
            {"cid", s => true },
            };

        public static bool checkBirthYear(string arg) => int.Parse(arg) >= 1920 && int.Parse(arg) <= 2002;
        public static bool checkIssueYear(string arg) => int.Parse(arg) >= 2010 && int.Parse(arg) <= 2020;
        public static bool checkExperirationYear(string arg) => int.Parse(arg) >= 2020 && int.Parse(arg) <= 2030;
        public static bool checkHeight(string arg)
        {
            var digit = int.Parse(arg.Where(c => char.IsDigit(c)).ToArray( ));
            var unit = new string(arg.Where(c => char.IsLetter(c)).ToArray( ));

            return unit switch
            {
                "cm" => digit >= 150 && digit <= 193,
                "in" => digit >= 59 && digit <= 76,
                _ => false
            };
        }
        public static bool checkHairColor(string arg)
        {
            if ( !arg.StartsWith('#') ) return false;

            var letters = new string(arg.Where(c => char.IsLetter(c)).ToArray( ));
            var regex = new Regex(@"[g-z]");
            return !regex.Match(letters).Success;
        }
        public static bool checkEyeColor(string arg)
        {
            var colors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return colors.Contains(arg);

        }

        public static bool checkPassportId(string arg)
        {
            if ( arg.Where(c => char.IsLetter(c)).Any( ) ) return false;

            return arg.Length == 9;
        }

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
            passports.Count(d => d.Keys.Count == 8 || ( d.Keys.Count == 7 && !d.ContainsKey("cid")));
        

        public override int SolvePart2( ) =>         
            passports.Where(d => d.Keys.Count == 8 || ( d.Keys.Count == 7 && !d.ContainsKey("cid") ))
                    .Count(p => p.All(kvp => requiredFields[kvp.Key].Invoke(kvp.Value)));                  


    }
}
