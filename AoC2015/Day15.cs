using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2015
{
    public class Day15 : Solution
    {
        private Dictionary<string, Dictionary<string, int>> ingredients;
        private const string Capacity = nameof(Capacity);
        private const string Durability = nameof(Durability);
        private const string Flavor = nameof(Flavor);
        private const string Texture = nameof(Texture);
        private const string Calories = nameof(Calories);

        public Day15(string file) : base(file, "\n")
        {
            ingredients = Input.Select(line =>
              {
                  var matches = Regex.Matches(line, @"([A-Z]\w+)|(capacity -?\d)|(durability -?\d)|(flavor -?\d)|(texture -?\d)|(calories -?\d)");
                  return (name: matches[0].Value,
                  cap: GetValue(matches[1].Value),
                  dur: GetValue(matches[2].Value),
                  flr: GetValue(matches[3].Value),
                  txr: GetValue(matches[4].Value),
                  cal: GetValue(matches[5].Value));
              }).ToDictionary(i => i.name, i => new Dictionary<string, int>
              {
                 { Capacity, i.cap },
                 { Durability, i.dur },
                 { Flavor, i.flr },
                 { Texture, i.txr },
                 { Calories, i.cal },
              });

            int GetValue(string v) => v.Contains("-") ?
                int.Parse(v.Where(char.IsDigit).ToArray( )) * -1 :
                int.Parse(v.Where(char.IsDigit).ToArray( ));
        }

        public override async Task<string> SolvePart1( ) => MakeCookies( )
                .Select(cookie => cookie[Capacity] * cookie[Durability] * cookie[Flavor] * cookie[Texture])
                .Max( ).ToString( );

        public override async Task<string> SolvePart2( ) => MakeCookies( )
                .Where(cookie => cookie[Calories] == 500)
                .Select(cookie => cookie[Capacity] * cookie[Durability] * cookie[Flavor] * cookie[Texture])
                .Max( ).ToString( );

        private IEnumerable<Dictionary<string, int>> MakeCookies( )
        {
            var permutations = ingredients.Count == 4 ? PermFourIngredients( ) : PermTwoIngredients( );

            var cookies = permutations.Select(p =>
            {
                var cookie = new Dictionary<string, int>
                 {
                       { Capacity, 0 },
                       { Durability, 0 },
                       { Flavor, 0 },
                       { Texture, 0 },
                       { Calories, 0 },
                 };

                p.ForEach(ingredient =>
                {
                    cookie[Capacity] += ingredients[ingredient.Key][Capacity] * ingredient.Value;
                    cookie[Durability] += ingredients[ingredient.Key][Durability] * ingredient.Value;
                    cookie[Flavor] += ingredients[ingredient.Key][Flavor] * ingredient.Value;
                    cookie[Texture] += ingredients[ingredient.Key][Texture] * ingredient.Value;
                    cookie[Calories] += ingredients[ingredient.Key][Calories] * ingredient.Value;
                });
                return cookie;
            });

            return cookies.Where(c => c[Capacity] > 0 && c[Durability] > 0 && c[Flavor] > 0 && c[Texture] > 0);
        }

        private List<Dictionary<string, int>> PermTwoIngredients( )
        {
            var permutations = new List<Dictionary<string, int>>( );
            for ( int i = 0 ; i < 100 ; i++ )
            {
                var j = 100 - i;
                permutations.Add(new Dictionary<string, int>
                {
                    {"Butterscotch", i },
                    {"Cinnamon", j }
                });
            }
            return permutations;
        }

        private List<Dictionary<string, int>> PermFourIngredients( )
        {
            var permutations = new List<Dictionary<string, int>>( );
            for ( int i = 0 ; i < 100 ; i++ )
            {
                for ( int j = 0 ; j < 100 - i ; j++ )
                {
                    for ( int k = 0 ; k < 100 - i - j ; k++ )
                    {
                        var l = 100 - k - j - i;
                        permutations.Add(new Dictionary<string, int>
                        {
                            {"Sugar", i },
                            {"Sprinkles", j },
                            {"Candy", k },
                            {"Chocolate", l }
                        }); ;
                    }
                }
            }
            return permutations;
        }
    }
}