using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class Combinator
    {
        public static List<List<T>> Generate<T>(List<T> elements, int cLength)
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
                        var prev = result.Where(r => r.Count == i).ToList();
                        prev.ForEach(r => result.Add(r.Expand(elements[j])));
                    }
                }
                result = result.Where(r => r.Count == i + 1).ToList( );
            }

            return result;
        }

    }
}
