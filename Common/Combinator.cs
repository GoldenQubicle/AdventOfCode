using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class Combinator
    {
        /// <summary>
        /// Given a list of elements, returns every combination. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="isFullSet">
        /// False by default, indicates whether to return subsets. For example given elements {'a', 'b', 'c'}
        /// the full set will also include combinations {'aa', 'ab', 'ac', .. }  as well as {'aaa', 'aab', 'aac', ...}.</param>
        /// <returns>A list of lists with every combination of the elements provided</returns>
        public static List<List<T>> Generate<T>(List<T> elements, bool isFullSet = false) => !isFullSet ?
            GetCombinations(elements, elements.Count).Where(r => r.Count == elements.Count).ToList( ) :
            GetCombinations(elements, elements.Count);

        /// <summary>
        /// Given a list of elements, returns every combination. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="cLength">
        /// The length of the combinations to be generated. For example given elements {'a', 'b'} and a <paramref name="cLength"/> of
        /// 4, will return combinations {'aaaa', 'aaab', 'aaba', 'aabb', ....}</param>
        /// <param name="isFullSet">
        /// False by default, indicates whether to return subsets. For example given elements {'a', 'b', 'c'}
        /// the full set will also include combinations {'aa', 'ab', 'ac', .. }  as well as {'aaa', 'aab', 'aac', ...}.</param>
        /// <returns>A list of lists with every combination of the elements provided</returns>
        public static List<List<T>> Generate<T>(List<T> elements, int cLength, bool isFullSet = false) => !isFullSet ?
            GetCombinations(elements, cLength).Where(r => r.Count == cLength).ToList( ) :
            GetCombinations(elements, cLength);

        private static List<List<T>> GetCombinations<T>(List<T> elements, int cLength)
        {
            var result = new List<List<T>>( );

            for ( var i = 0 ; i < cLength ; i++ )
            {
                for ( var j = 0 ; j < elements.Count ; j++ )
                {
                    if ( i == 0 )
                    {
                        result.Add(new List<T> { elements[j] });
                    }
                    else
                    {
                        var prev = result.Where(r => r.Count == i).ToList( );
                        prev.ForEach(r => result.Add(r.Expand(elements[j])));
                    }
                }
            }
            return result.Where(r => r.Count != 1).ToList( );
        }
    }
}
