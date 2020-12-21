using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Solutions
{
    public class Day21 : Solution<string>
    {
        Dictionary<string, HashSet<string>> allergensIngredients = new( );
        List<(List<string> ingredients, List<string> allergens)> foods = new( );
        public Day21(string file) : base(file)
        {
            var list = Input.Select(i =>
            {
                i = i.Replace('(', ' ');
                i = i.Replace(')', ' ');
                return i;
            });

            foods = list.Select(i => i.Split("contains"))
                .Select(i => (i[0].Split(' ').Select(s => s.Trim( )).Where(s => !string.IsNullOrEmpty(s)).ToList( ),
                              i[1].Split(' ', ',').Select(s => s.Trim( )).Where(s => !string.IsNullOrEmpty(s)).ToList( ))).ToList( );

            foods.ForEach(food =>
            {
                food.allergens.ForEach(allergen =>
                {
                    if ( allergensIngredients.ContainsKey(allergen) )
                        allergensIngredients[allergen].IntersectWith(food.ingredients);
                    else
                        allergensIngredients.Add(allergen, food.ingredients.ToHashSet( ));
                });
            });
        }

        public override string SolvePart1( )
        {
            var bad = allergensIngredients.SelectMany(kvp => kvp.Value.Select(i => i)).Distinct( );

            return foods.Sum(f => f.ingredients.Except(bad).Count( )).ToString( );
        }

        public override string SolvePart2( )
        {
            var knownAllergens = new Dictionary<string, string>( );
            while ( allergensIngredients.Any(kvp => kvp.Value.Count( ) > 0) )
            {
                var newKnown = allergensIngredients
                    .Where(kvp => kvp.Value.Count == 1)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.First());

                knownAllergens.AddRange(newKnown);

                foreach ( var (allergen, ingredients) in allergensIngredients )
                {
                    allergensIngredients[allergen] = ingredients.Except(newKnown.Values.ToList()).ToHashSet( );
                }
            }

            var sorted = new SortedDictionary<string, string>(knownAllergens);
            return sorted.Values.Aggregate(string.Empty, (s, a) => s + "," + a)[1..]; // get rid of first ","
        }
    }

    public static class DictionaryExtension
    {
        public static Dictionary<string, string> AddRange(this Dictionary<string, string> dic,  Dictionary<string, string> other )
        {
            foreach(var kvp in other )
            {
                dic.Add(kvp.Key, kvp.Value);
            }
            return dic;
        }            
    }
}
