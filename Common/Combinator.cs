namespace Common;

[Obsolete("Use Combinatorics nuget pacakge")]
public static class Combinator
{
	public static CombinatorResult<T> Generate<T>(List<T> elements, CombinatorOptions options)
	{
		// figure out what is the combinations' length? If not specified, or each element can only occur once
		// per combination, we use the elements count. Otherwise as specified per the options. 
		var cLength = options.Length == 0 || options.IsElementUnique ? elements.Count : options.Length;

		// get the combinations. This returns a full set by default
		var combos = options.IsFullSet ? GetCombinationsFullSet(elements, cLength) : GetCombinationsSparseSet(elements, cLength);

		//// if not a full set, only return the combinations which are of the specified length
		//if(!options.IsFullSet)
		//    combos = combos.Where(r => r.Count == cLength).ToList();

		// filter out combinations which include duplicate elements
		if(options.IsElementUnique)
			combos = combos.Where(r => r.GroupBy(e => e).All(g => g.Count() == 1)).ToList();

		// filter out combinations which contain the same elements, e.g. 'ab' is the same as 'ba'
		// note; identifying similar combinations by sum of hashcodes only works for strings. 
		// not ideal given the method is generic, would like to fix this.. maybe hashsets..?
		if(!options.IsOrdered)
			combos = combos.Select(l => (l: l, h: l.Sum(e => (long) e.GetHashCode())))
				.GroupBy(t => t.h)
				.Select(g => g.First().l).ToList();

		return new CombinatorResult<T> { Result = combos };
	}

	private static List<List<T>> GetCombinationsSparseSet<T>(List<T> elements, int cLength)
	{
		var result = new List<List<T>>();

		for(var i = 0 ; i < cLength ; i++)
		{
			var temp = new List<List<T>>();

			for(var j = 0 ; j < elements.Count ; j++)
			{
				if(i == 0)
				{
					temp.Add(new List<T> { elements[j] });
				}
				else
				{
					for(int k = 0; k < result.Count; k++)
						temp.Add(result[k].Expand(elements[j]));
				}
			}
			result = temp;
		}

		return result;
	}

	private static List<List<T>> GetCombinationsFullSet<T>(List<T> elements, int cLength)
	{
		var result = new List<List<T>>();

		for(var i = 0 ; i < cLength ; i++)
		{
			for(var j = 0 ; j < elements.Count ; j++)
			{
				if(i == 0)
				{
					result.Add(new List<T> { elements[j] });
				}
				else
				{
					var prev = result.Where(r => r.Count == i).ToList();
					prev.ForEach(r => result.Add(r.Expand(elements[j])));
				}
			}
		}
		return result.Where(r => r.Count != 1).ToList();
	}
}


public record CombinatorOptions
{
	/// <summary>
	/// How long the combination to make is. Does not apply when a combination must consist of unique elements. 
	/// </summary>
	public int Length { get; init; }
	/// <summary>
	/// Indicates whether to return subsets. Given elements {a, b, c} a full set will 
	/// also include combinations {aa, ab, ac, .. } as well as {aaa, aab, aac, ...}. False by default.
	/// </summary>
	public bool IsFullSet { get; init; }
	/// <summary>
	/// Indicates elements can only occur once per combination. Given elements {a, b} this will exclude {aa, bb}. 
	/// False by default. 
	/// </summary>
	public bool IsElementUnique { get; init; }
	/// <summary>
	/// Indicates whether the order of elements within a combination constitute a unique combination. 
	/// Given elements {a, b} the combinations ab & ba are considered unique when true, which is the default.  
	/// </summary>
	public bool IsOrdered { get; init; } = true;
}

public class CombinatorResult<T> : IEnumerable<List<T>>
{
	public List<List<T>> Result { get; init; }
	public IEnumerator<List<T>> GetEnumerator( )
	{
		foreach(var r in Result)
		{
			yield return r;
		}
	}
	IEnumerator IEnumerable.GetEnumerator( ) => GetEnumerator();
}